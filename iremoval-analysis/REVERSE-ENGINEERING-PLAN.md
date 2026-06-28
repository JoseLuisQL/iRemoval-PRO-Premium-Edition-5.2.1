# Plan de Ingeniería Inversa — iRemoval-PRO-Premium-Edition-5.2.1

**Objetivo:** descompilar y reconstruir el código fuente completo del paquete `iRemoval-PRO-Premium-Edition-5.2.1`, con fidelidad suficiente para recompilar y producir un binario funcionalmente equivalente.

**Activos bajo análisis:**
- `iRemoval PRO.exe` (2.7 MB) — launcher .NET Framework WPF, ofuscado (patrón ConfuserEx)
- `iremovalpro.dll` (33 MB) — motor nativo x64, .NET NativeAOT + runtime statically linked
- `ref/toolkits/` — dependencias open-source (libimobiledevice/libplist/libusbmuxd/OpenSSL), no se revierten

**Principio rector del plan:** *dump dinámico primero, estático después.* El engine está compilado a nativo y ofuscado; la única forma de recuperar IL es dumpearlo en runtime, ya descifrado/deshidratado en memoria. Cualquier intento de revertir estático puro del engine desde el binario en disco va a perder código o a tardar semanas.

---

## Fase 0 — Preparación del entorno

**Objetivo:** montar un lab aislado, reproducible, snapshotable, donde se pueda ejecutar el binario con instrumentation sin riesgo de fuga.

### 0.1 Hardware
- Host físico: x86_64, ≥16 GB RAM, SSD ≥100 GB libres, **con puerto USB físico accesible** (no hub — directo al host, para pasar-through a la VM).
- iPhone de pruebas: modelo A7–A11 (Checkra1n-compatible si vamos a probar el bypass real), **no el iPhone de uso diario**. Recomendado: iPhone 7/8 de segunda mano, restaurado a iOS 15.x donde sea vulnerable.
- USB-A → Lightning (mejor que USB-C para evitar negociaciones PD raras con usbmuxd).

### 0.2 VM host
- **VMware Workstation Pro 17** (mejor soporte de USB passthrough que VirtualBox) o **Hyper-V Gen2** si querés nested.
- SO guest: **Windows 10 22H2 x64** (no Win11 — algunos anti-debug de Themida/packers explotan TPM/secure boot de Win11).
- Config:
  - CPU: 4 vCPU, VT-x/EPT habilitado, **deshabilitar side-channel mitigations** en VMware (acelera y reduce ruido).
  - RAM: 8 GB.
  - Disco: 80 GB thin-provisioned.
  - USB: **Automatically connect new USB devices** ON.
  - Display: **Disable 3D acceleration** (algunos packers lo usan como anti-VM).
  - Network: **NAT** o host-only con `mitmproxy` upstream. **Nunca bridged a LAN de producción.**

### 0.3 Snapshot base
Instalar y configurar todo, luego **snapshot "clean-rev-base"** antes de tocar el binario. Cada corrida del binario sale de este snapshot, así no hay residuo entre sesiones.

### 0.4 Toolchain estática (Linux o WSL en la VM)
- `binutils` (`objdump`, `readelf`, `nm`, `strings`)
- `file`, `xxd`
- `yara` + reglas de [YARA-Signator](https://github.com/avast/yarules) y [ capa-rules](https://github.com/mandiant/capa-rules)
- `radare2` / `rizin` + `cutter`
- `Ghidra 11.x` con extensiones: `.NET Analyzer`, `Microsoft.SymbolUploader`
- `IDA Free 8.4+` con `Hex-Rays microcode` (si tenés licencia)
- `python3` + paquetes: `pefile`, `dnfile`, `capstone`, `unicorn`, `keystone-engine`

### 0.5 Toolchain .NET reversing
- **dnSpyEx** (fork mantenido de dnSpy): <https://github.com/dnSpyEx/dnSpy>
- **de4dot** (deobfuscator): <https://github.com/de4dot/de4dot>
- **ILSpy** + plugin `ILSpy.Backports` (cruce de versiones de BCL)
- **AsmResolver** (lib .NET para manipular PE/IL desde código): <https://github.com/Washi1337/AsmResolver>
- **dotPeek** (JetBrains) como alternativa a dnSpy
- **GreyHatHacker .NET deobfuscation suite** si ConfuserEx tiene módulos custom
- **ConfuserEx-Unpacker** (varios forks en GitHub)
- `monodis`, `ikdasm` (alternativas CLI para .NET Framework)

### 0.6 Toolchain dinámica (Windows)
- **x64dbg** + plugins:
  - `ScyllaHide` (anti-anti-debug)
  - `Scylla` (import reconstruction)
  - `xAnalyzer`
  - `TitanHide` (kernel-level anti-anti-debug, opcional)
- **Process Hacker 2** / **System Informer** (inspect memory, handles, sockets en runtime)
- **API Monitor v2** (ropeado de `rohitab.com`) — log de todas las API calls
- **Wireshark** + **USBPcap** (captura de tráfico USB al iPhone)
- **Fiddler Classic** o **mitmproxy** para HTTPS al server de licencia
- **Frida 16.x** + scripts .NET (`frida-il2cpp-bridge` no aplica pero `frida-dotnet` sí)
- **DebugView** (DbgPrint)
- **Procmon** (Sysinternals)
- **PE-bear**, **CFF Explorer**, **Detect It Easy (DIE)**

### 0.7 .NET runtime de debugging
- **.NET Framework 4.8 Developer Pack** (para `cordbg` y símbolos)
- **.NET 8 SDK** (por si el engine es NativeAOT de .NET 8 — vamos a confirmar en Fase 1)
- **windbg** + `sos.dll` + `clrmd` (ClrMD para inspección de heap CLR desde fuera)

### 0.8 Aislamiento
- VM **sin credenciales** guardadas, sin cuenta Microsoft vinculada, sin historial de nada.
- Snapshot limpio antes de cualquier ejecución del binario.
- **Nunca conectar el iPhone de pruebas a otra máquina** después de usarlo con la VM (por si el bypass lo modifica de forma persistente).
- Host sin acceso a LAN corporativa durante las sesiones.

### 0.9 Criterio de salida
Checklist:
- [ ] VM Windows 10 snapshotada limpia
- [ ] Toolchain estática instalada en Linux/WSL
- [ ] Toolchain dinámica instalada en Windows guest
- [ ] iPhone de pruebas identificado y dedicado
- [ ] `mitmproxy`/Fiddler con CA confiada en el trust store del guest
- [ ] USB passthrough verificado (el guest ve el iPhone via iTunes tethering test)

---

## Fase 1 — Triaje y fingerprinting completo

**Objetivo:** caracterizar los binarios en disco con detalle suficiente para decidir tooling y estrategia. Salida: `triage.json` con todos los metadatos.

### 1.1 Hashes y firma
- `certutil -hashfile "iRemoval PRO.exe" SHA256` y lo mismo para `iremovalpro.dll`.
- `Get-AuthenticodeSignature` (PowerShell) — esperar **NotSigned**.
- `Get-AppLockerFileInformation` para ver reglas efectivas.

### 1.2 PE completo con pefile
```python
import pefile, json, hashlib
for p in ["iRemoval PRO.exe", "iremovalpro.dll", "ref/toolkits/idevicepair.exe"]:
    pe = pefile.PE(p, fast_load=False)
    info = {
        "path": p,
        "sha256": hashlib.sha256(open(p,'rb').read()).hexdigest(),
        "machine": hex(pe.FILE_HEADER.Machine),
        "timestamp": pe.FILE_HEADER.TimeDateStamp,
        "sections": [{"name": s.Name.rstrip(b'\x00').decode('latin1'),
                       "vsize": s.Misc_VirtualSize,
                       "rsize": s.SizeOfRawData,
                       "vaddr": hex(s.VirtualAddress),
                       "entropy": s.get_entropy()} for s in pe.sections],
        "imports": [dll.dll.decode() for dll in pe.DIRECTORY_ENTRY_IMPORT] if hasattr(pe,'DIRECTORY_ENTRY_IMPORT') else [],
        "exports": [e.name.decode() if e.name else f"ord_{e.ordinal}" for e in pe.DIRECTORY_ENTRY_EXPORT.symbols] if hasattr(pe,'DIRECTORY_ENTRY_EXPORT') else [],
        "rich_header": bool(pe.RICH_HEADER),
        "debug_entries": [(d.struct.Type, d.struct.SizeOfData) for d in pe.DIRECTORY_ENTRY_DEBUG] if hasattr(pe,'DIRECTORY_ENTRY_DEBUG') else [],
    }
    json.dump(info, open(f"{p}.triage.json","w"), indent=2)
```

**Outputs esperados:**
- EXE: machine=0x14c (i386), 5 secciones, imports solo `mscoree.dll`, sin exports, sin Rich (fue construido con `csc`/`msbuild` no con VC++).
- DLL: machine=0x8664 (x64), 11 secciones (`.text`, `.managed`, `hydrated`, `.rdata`, `.data`, `.pdata`, `.RZ`, `.CFY`, `.CJz`, `.rsrc`, `.reloc`), exports posiblemente ocultos o ninguno, **alta entropía** en `.RZ`/`.CFY`/`.CJz` (>7.0) — sugiere datos comprimidos/cifrados.
- Rich header del EXE = False (coincide con .NET Framework csc).

### 1.3 Detector de packer/protector
- **DIE (Detect It Easy)** con todos los scripts activados. Anotar:
  - Protector detectado (si lo hay)
  - Compiler detectado (.NET Framework / NativeAOT)
  - Entropy por sección
- **YARA** con reglas:
  - `Themida_WinLicense.yar` (aunque ya vimos que no hay strings claros, los patterns de código sí pueden estar)
  - `VMProtect.yar`
  - `Eziriz_Reactor.yar`
  - `ConfuserEx.yar` (sobre el EXE)
  - `NativeAOT.yar` (custom — buscar `DOTNET_GCRegionRange`, `RehydrateData`)
- **PE-bear** visualizar secciones y buscar superposición / TLS callbacks / weird entry points.

### 1.4 Strings dirigidos
Para cada binario, extraer y categorizar:
- URLs y hosts (`grep -E 'https?://|api\.|\.php|\.aspx'`)
- Identificadores .NET legibles (los que no matcheen el patrón `A[0-9A-F]{7}`)
- Referencias a tipos BCL (`System.Net.Http.*`, `System.Security.Cryptography.*`)
- Strings de debug / asserts (pistas de versiones de .NET, rutas de build)
- Plists, XML, JSON embebidos (muchos — ya vimos el HTML basura)
- Nombres de archivos y rutas (`C:\`, `\src\`, `\obj\`, `\bin\`)

**Output esperado para el engine:** confirmación de versión del runtime (.NET 6/7/8), pistas de rutas de build (`\iremovalpro\src\…`), posibles nombres de tipo legibles que sobrevivieron a la compilación AOT.

### 1.5 Imports del engine — análisis fino
`objdump -p iremovalpro.dll` → lista completa. Mapear cada DLL importada a comportamiento esperado:
- `winhttp.dll` → HTTP/HTTPS al server de licencia
- `WS2_32.dll` → sockets raw (¿USB network tethering para el iPhone?)
- `IPHLPAPI.DLL` → fingerprinting de red (anti-VM por MAC)
- `bcrypt.dll` + `ncrypt.dll` + `CRYPT32.dll` → TLS y firma con cert cliente
- `sspicli.dll` → auth SSPI (¿negociación con server de licencia?)
- `FirewallAPI.dll` (saw it in strings) → manipulación de firewall del host

### 1.6 Recursos del EXE (BAML/XAML)
El launcher WPF guarda su UI en `iRemovalProWPF.g.resources` como **BAML**. Extraerlo con `dnSpy` → `iRemovalProWPF` → `Properties/Resources` → export. O con `ildasm` + `resview`. La UI en XAML te dice **exactamente qué controles y handlers hay** — una mina de oro para reconstruir la lógica de presentación.

### 1.7 Criterio de salida
- [ ] `triage.json` por binario con hashes/PE/sections/imports
- [ ] DIE report por binario
- [ ] YARA hits documentados
- [ ] Lista de strings categorizados
- [ ] BAML del launcher extraído y descompilado a XAML
- [ ] Decisión: ¿engine es NativeAOT de .NET 6/7/8? (lo confirma el string `RepositoryUrlBhttps://github.com/dotnet/runtime` y la ausencia de BSJB)

---

## Fase 2 — Análisis dinámico preliminar

**Objetivo:** observar el comportamiento del binario en runtime **sin tocarlo todavía** — capturar tráfico, APIs, IOCs, antes de meter debugger. Esto guía el resto del reversing: qué código se ejecuta primero, qué se descifra, a dónde llama.

### 2.1 Snapshot y boot limpio
Revertir a `clean-rev-base`. Verificar que no hay procesos sospechosos en background.

### 2.2 Instrumentación pasiva (sin tocar el binario)
Antes de ejecutar, dejar corriendo:
- **Procmon** con filtro `ProcessName is "iRemoval PRO.exe"` — captura filesystem/registry/process/thread.
- **API Monitor** hooked a `iRemoval PRO.exe` con profile `All-StdAPIs` + `All-Com` + `All-Networking` (puede ser pesado, ver Fase 2.5).
- **Wireshark** en la interfaz NAT del guest + **USBPcap** en la interfaz USB.
- **Frida** con script stub:
  ```js
  Process.setExceptionHandler(function(details) {
    console.log("[EXC]", details.type, details.address, details.context.pc);
    return false;
  });
  Module.load("iremovalpro.dll");
  ```
- **DebugView** capturando `OutputDebugString`.
- **mitmproxy** en modo transparent proxy sobre el tráfico del guest (CA ya confiada en Fase 0.7).

### 2.3 Primera ejecución — sin iPhone conectado
- Lanzar `iRemoval PRO.exe` desde `Explorer` (no desde debugger).
- Observar:
  - ¿Aparece GUI WPF? ¿Qué texto muestra?
  - ¿Pide registro/UDID/serial?
  - ¿Hace HTTP/HTTPS? Capturar URLs en mitmproxy.
  - ¿Crea archivos? ¿Dónde? (`%AppData%\iRemovalPro\`, `%LocalAppData%\`, `%Temp%\`).
  - ¿Escribe registry? (`HKCU\Software\iRemovalPro`, `HKLM\...`).
  - ¿Carga `iremovalpro.dll`? Ver en Process Hacker → módulos cargados.
  - ¿Genera procesos hijos? (`cmd.exe`, `idevicepair.exe`, `powershell.exe`).
  - ¿Detecta debugger/VM y muere? (anti-debug probe).
- **Detener** el binario después de 60s sin interacción.
- Guardar todos los logs en `run-01-no-iphone/`.

### 2.4 Segunda ejecución — con iPhone conectado
- iPhone en modo normal (encendido, desbloqueado, en pantalla de inicio).
- Asegurar `usbmuxd` corriendo en el guest (instalar **Apple Mobile Device Support** o iTunes COM para tener el servicio).
- Pasar el iPhone al guest vía USB passthrough.
- Lanzar `iRemoval PRO.exe`.
- Observar además:
  - ¿Detecta el iPhone? ¿Muestra UDID, modelo, iOS version?
  - ¿Qué comandos envía por USB? Capturar paquetes usbmuxd.
  - ¿Pide pair? Si sí, ¿qué flujos usa?
- Guardar logs en `run-02-iphone-normal/`.

### 2.5 Tercera ejecución — con iPhone en modo DFU/recovery
- Poner iPhone en DFU.
- Lanzar `iRemoval PRO.exe`.
- Observar:
  - ¿Reconoce modo DFU?
  - ¿Llama a `checkm8` exploit? (Strings: `checkm8`, `usb_exploit`, `heap_spray`, `usb_req_stall`).
  - ¿Carga el iBEC/iBSS personalizado? ¿De dónde?
- Guardar logs en `run-03-dfu/`.

### 2.6 Análisis del tráfico capturado
- **mitmproxy**: ¿a qué dominios llamó? Si falla TLS (cert pinning), anotar y pasar a Fase 6.
- **Wireshark usbmuxd**: decodificar paquetes con `imobiledevice-net` o `pymobiledevice3` en Linux para entender qué pares clave-valor del plist envía el engine.

### 2.7 Criterio de salida
- [ ] 3 runs con logs completos
- [ ] Lista de dominios/IPs contactadas
- [ ] Lista de archivos/registry creados
- [ ] Lista de módulos cargados por proceso
- [ ] Confirmación de si hay anti-debug
- [ ] Confirmación de si usa `idevicepair.exe` o su propio stack .NET

---

## Fase 3 — Unpacking del launcher (`iRemoval PRO.exe`)

**Objetivo:** desofuscar y descompilar el launcher a C# legible y recompilable. El launcher es la parte más accesible del proyecto.

### 3.1 Confirmar el obfuscator
```bash
de4dot "iRemoval PRO.exe" -d
```
`de4dot -d` detecta el obfuscator automáticamente. Esperado:
```
Detected Obfuscator: ConfuserEx (or compatible)
```

### 3.2 Desofuscación automática
```bash
de4dot "iRemoval PRO.exe" -o "iRemoval PRO.cleaned.dll"
```
de4dot:
- Renombra identificadores obfuscados (`A007D23E` → nombres significativos cuando puede inferirlos del tipo)
- Desencripta strings (ConfuserEx usa un módulo de string encryption que llama a un decrypter embebido — de4dot lo reimplementa)
- Remueve anti-tamper/anti-debugger de ConfuserEx (si los hay)
- Reconstruye el metadata stream

**Output:** `iRemoval PRO.cleaned.dll` — un ensamblado .NET Framework estándar, decompilable con dnSpy.

### 3.3 Desofuscación manual de residuos
de4dot no siempre limpia todo. Abrir en dnSpy y revisar:
- Métodos con `<PrivateImplementationDetails>` → eran lambdas/closures de ConfuserEx.
- Strings que todavía aparecen como base64+IV → eran strings de segundo nivel de ConfuserEx, hay que desencriptar a mano o con scripts de `ConfuserEx-Unpacker` (forks en GitHub).
- Tipos con nombre `Axxxxxxx` que de4dot no renombró → renombrar manualmente según contexto.

### 3.4 Decompilación a proyecto C#
En dnSpyEx:
- File → Open Assembly → `iRemoval PRO.cleaned.dll`
- Project → Export to Project
- Elegir `.NET Framework 4.8`, lenguaje `C# 7.3` (compatible con VS 2019+).
- Output: carpeta con `.csproj`, `.cs`, `.xaml`, `App.config`, `packages.config`/`AssemblyReferences`.

**Output esperado:** `launcher-src/` con estructura tipo:
```
launcher-src/
├── iRemovalProWPF.csproj
├── App.xaml / App.xaml.cs
├── MainWindow.xaml / MainWindow.xaml.cs
├── Properties/
│   ├── AssemblyInfo.cs
│   ├── Resources.resx / Resources.Designer.cs
│   └── Settings.settings
└── obj/   (generated)
```

### 3.5 Verificación de build
```bash
cd launcher-src/
msbuild /t:Restore
msbuild /p:Configuration=Debug /p:Platform=AnyCPU
```
Si compila → el launcher está reconstruido. Si no, resolver:
- Referencias faltantes (typical: `PresentationCore`, `WindowsBase`, `System.Xaml`).
- Recursos XAML rotos (paths relativos mal).
- Código generado por de4dot con nombres raros que el compilador rechaza → renombrar.

### 3.6 Mapeo de la lógica del launcher
Lo que vamos a buscar en el código descompilado:
- **Punto de entrada** (`App.OnStartup`) → qué hace al arrancar.
- **Carga del engine**: ¿cómo carga `iremovalpro.dll`? P/Invoke (`[DllImport]`)? Reflection? ¿Cuál es el punto de entrada nativo que llama?
- **Validación de licencia**: ¿hay checkeo local antes de llamar al server? (UDID hash, hardware fingerprint).
- **Handlers de UI**: cada botón → método → lógica.
- **Anti-debug/anti-VM** embebido (ConfuserEx a veces inyecta checks).

### 3.7 Criterio de salida
- [ ] `de4dot -d` confirmó ConfuserEx
- [ ] `iRemoval PRO.cleaned.dll` decompila limpio en dnSpy
- [ ] Proyecto `launcher-src/` compila con `msbuild`
- [ ] Identificado: entry point, carga del engine, validación de licencia, handlers de UI
- [ ] Mapeo de métodos → funciones del engine (tabla de P/Invoke o tabla de exports de `iremovalpro.dll`)

---

## Fase 4 — Unpacking del engine (`iremovalpro.dll`)

**Objetivo:** recuperar el IL (intermediate language) original del motor desde el binario nativo. **Esta es la fase más difícil y la que decide si el proyecto entero es viable.**

### 4.1 Confirmar el modelo de publicación
Antes de dumpear, confirmar qué tipo de binario es. Strings + estructura sugieren **.NET NativeAOT** (no ReadyToRun, no Crossgen2 a secas).

Verificar:
- `strings iremovalpro.dll | grep -i 'NativeAOT\|crossgen\|ReadyToRun\|Composite'`
- Buscar la tabla `Methodtable` de .NET AOT — los RTT (ReadyToRun) headers viven en `.managed` o `hydrated`.
- Si NativeAOT puro: **no hay metadata IL en disco en ningún momento**. El código IL fue compilado a native en build-time. La única forma de recuperar fuente es descompilar el native x64 con Ghidra/IDA (no ideal) o dumpear post-rehydrate.

### 4.2 Dumping dinámico — estrategia
NativeAOT in-place hace *hydration* al arrancar: las secciones `.managed` / `hydrated` se relocalizan y se preparan para ejecución. Ese es el momento de dumpear.

**Estrategia A — proceso detenido tras hydration:**
1. VM revertida a snapshot limpio.
2. Abrir `x64dbg`, attach a `iRemoval PRO.exe` (o lanzar el EXE bajo debugger con `CREATE_SUSPENDED`).
3. Poner breakpoint en `iremovalpro.dll!NativeAOT_Runtime_Initialize` (o equivalente — buscar export, si no hay, breakpoint en entry del módulo).
4. Dejar correr hasta que `hydrated` esté poblada (event: primer UI del launcher o primer log de red).
5. Usar **Scylla** o **Process Hacker → Memory → Save** para dumpear:
   - El módulo completo (`iremovalpro.dll` relocalizado).
   - El heap managed (donde vive el `MethodTable` reconstruido).
6. Salvar a `engine-dump/`.

**Estrategia B — Volatility style desde fuera:**
1. Usar **ClrMD** (`Microsoft.Diagnostics.Runtime`) para attach al proceso vivo:
   ```csharp
   using var target = DataTarget.AttachToProcess(pid, suspend: true);
   var runtime = target.ClrVersions[0].CreateRuntime();
   foreach (var module in runtime.EnumerateModules())
       Console.WriteLine(module.Name);
   ```
   Si el runtime está cargado (NativeAOT embebe CoreCLR-like data structures), ClrMD debería poder enumerar tipos y métodos.
2. Si ClrMD funciona: iterar todos los `ClrType` y dumpear:
   - Nombres de tipo y método.
   - Firmas de método (parámetros, retorno).
   - **Campos** (nombres y tipos).
   - Strings del heap.
3. Para cuerpos de método: NativeAOT **no guarda IL** — los cuerpos están compilados a native x64. Hay que descompilarlos con Ghidra aplicando el signature de la calling convention de CoreCLR.
4. Dump completo a `engine-dump/clrmd-output.json`.

**Estrategia C — Hooking con Frida:**
1. Cargar `iremovalpro.dll` en el proceso del launcher.
2. Frida hook en cada método de la vtable managed (cuando sepamos direcciones):
   ```js
   const symbols = Module.enumerateExports("iremovalpro.dll");
   symbols.forEach(s => {
     Interceptor.attach(s.address, {
       onEnter(args) { console.log("[CALL]", s.name); },
       onLeave(retval) { console.log("  ret:", retval); }
     });
   });
   ```
3. Esto no da fuente pero da **traza completa de ejecución** — qué se llama, en qué orden, con qué args.

### 4.3 Reconstrucción de metadata
Con el dump de ClrMD (Fase 4.2 Estrategia B), generar:
- `types.json` — lista de todos los tipos con namespaces inferred.
- `methods.json` — lista de todos los métodos con firma.
- `strings.json` — todos los strings del heap managed.

A partir de eso, **construir un ensamblado .NET sintético** con `AsmResolver` que tenga los mismos tipos y métodos (con cuerpos vacíos o stubs que llamen a un `throw new NotImplementedException()`). Esto te da un DLL que dnSpy puede abrir y mostrar como árbol navegable.

```csharp
// Pseudocódigo con AsmResolver
var assembly = new AssemblyDefinition("iremovalpro", new Version(1,0,0,0));
var module = new ModuleDefinition("iremovalpro.dll");
assembly.Modules.Add(module);
foreach (var typeInfo in types) {
    var td = new TypeDefinition(typeInfo.Namespace, typeInfo.Name, typeAttributes);
    module.TopLevelTypes.Add(td);
    foreach (var m in typeInfo.Methods) {
        var md = new MethodDefinition(m.Name, methodAttributes,
            module.CorLibTypeFactory.Void);
        // signature, parameters...
        td.Methods.Add(md);
    }
}
assembly.Write("iremovalpro.skeleton.dll");
```

### 4.4 Descompilación de cuerpos de método (la parte dura)
NativeAOT produce native x64. Para cada método, Ghidra/IDA te da desensamblado + pseudocódigo C. Eso no es C#.

**Estrategias para recuperar el C# original:**

1. **Pattern matching contra BCL**: muchas llamadas son a métodos estándar de `System.*`. Con el signature de CoreCLR y los nombres de símbolo (que NativeAOT preserva parcialmente), se pueden mapear x86 calls a llamadas BCL conocidas.
2. **Tool `ILReverser` (custom)**: script que recorre los cuerpos native y produce pseudocódigo C# alto nivel. Existen herramientas experimentales (ej: `ILSpy.NativeAot` plugin, `nativeaot-decompiler` en GitHub) pero están en estado alpha.
3. **Reconstrucción manual método a método**: el path más realista para los métodos críticos. Empezar por:
   - `Usbmuxd*.Connect/Send/Receive` — protocolo bien documentado en `libimobiledevice`, el código .NET debe mirror.
   - `Afc*.Read/Write/Open` — idem.
   - `ActivationRecord.Parse/Validate` — formato plist conocido.
   - `LicenseClient.*` — menos documentado pero inferible del tráfico capturado.
4. **Comparar con la reimplementación open-source** `imobiledevice-net` (`Quamotion`): es una lib .NET que reimplementa usbmuxd/AFC/lockdown. **iRemoval PRO casi con seguridad se basó en ella** (los nombres de tipo en los strings — `UsbmuxdDevice`, `AfcFileOpenMode` — son casi idénticos). Descargar `imobiledevice-net` source y usar como referencia de alto nivel para cada clase. Esto ahorra días.

### 4.5 Anti-debug / anti-dump
Si el engine tiene checks (TLS callbacks, `IsDebuggerPresent`, `NtQueryInformationProcess(ProcessDebugPort)`, timing checks), hay que bypassearlos antes del dump:
- **ScyllaHide** en x64dbg cubre los clásicos.
- Para TLS callbacks: poner breakpoints en `TlsCallback` antes del entry point.
- Para timing: hooks con Frida que retornen valores esperados.
- Para anti-VM: si detecta VMware por CPUID, setear `cpuid_mask` en VMware config o hook CPUID con Frida.

### 4.6 Criterio de salida
- [ ] Dump dinámico del engine (`engine-dump/iremovalpro.relocated.dll`)
- [ ] `types.json` + `methods.json` + `strings.json` de ClrMD
- [ ] `iremovalpro.skeleton.dll` navegable en dnSpy
- [ ] Cuerpos de los métodos críticos (usbmuxd, AFC, activation, license) decompilados a C# legible
- [ ] Mapping de cada método native → nombre inferido → firma C#

### 4.7 Riesgo y fallback
Si Fase 4 no logra recuperar IL suficiente (probable para los cuerpos no críticos):
- **Aceptar pérdida parcial** en métodos no esenciales (logging, UI helpers).
- Reemplazarlos con stubs que replican el comportamiento observado en runtime.
- Documentar qué se perdió y por qué en `LOSS.md` del proyecto final.

---

## Fase 5 — Mapeo de protocolos iDevice

**Objetivo:** validar la reconstrucción de la lógica crítica comparándola con la especificación pública de los protocolos Apple.

### 5.1 Referencias públicas
- **libimobiledevice** source: <https://github.com/libimobiledevice/libimobiledevice>
- **imobiledevice-net** source: <https://github.com/libimobiledevice/imobiledevice-net>
- **usbmuxd** protocol: <https://github.com/libimobiledevice/usbmuxd/blob/master/docs/>
- **Apple Mobile Device Interface (lockdown)**: reverse-engineered docs en <https://www.theiphonewiki.com/wiki/Lockdownd>
- **AFC (Apple File Conduit)**: <https://theapplewiki.com/wiki/Apple_File_Conceal>

### 5.2 Tabla de mapeo
Por cada clase en el engine, validar contra la referencia pública:

| Clase en engine | Referencia pública | Origen preferido |
|---|---|---|
| `UsbmuxdDevice`, `UsbmuxdHeader`, `UsbmuxdMessageType` | `usbmuxd.h` / `imobiledevice-net` | struct-for-struct |
| `AfcService`, `AfcFileOpenMode`, `AfcOpCode` | `afc.h` | opcode enum |
| `AfcPacket`, `AfcHeader` | idem | struct layout |
| `ActivationRecord`, `activation-record` | Apple activation docs | field names |
| `MdM[` (MDM) | Apple MDM protocol | profile structure |
| `LockdownClient` (si aparece) | lockdown protocol | port 62078 |

### 5.3 Pruebas funcionales
- Implementar un mini-cliente .NET que use nuestras clases reconstruidas y hablar con un iPhone real.
- Comparar paquetes enviados con los capturados en Fase 2.6.
- Si coinciden byte-for-byte (excepto timestamps y nonces), la reconstrucción es fiel.

### 5.4 Criterio de salida
- [ ] Tabla de mapeo completa
- [ ] Mini-cliente .NET prueba-concepto que talk con iPhone real
- [ ] Diferencias documentadas (¿hay extensiones propietarias del autor? ¿mods a los protocolos estándar?)

---

## Fase 6 — Identificación del server de licencia

**Objetivo:** descubrir a qué dominio/IP llama el engine para validar la licencia, y cómo negocia (TLS, cert pinning, signing de requests).

### 6.1 TLS interception
- Confirmar si hay cert pinning. En Fase 2.3, si mitmproxy captura el tráfico sin error → no hay pinning. Si TLS falla → hay pinning y hay que bypassearlo.
- **Bypass de pinning** (si aplica):
  - Frida script que hookea `ServicePointManager.ServerCertificateValidationCallback` y retorna true (para .NET Framework).
  - Para .NET 6+ NativeAOT: hookear `SslStream.InternalRemoteCertificate` o similar.
  - En último caso, **patch del binario**: cambiar la dirección del callback de validación por una NOP en el código native (después de Fase 4, sabiendo dónde está).

### 6.2 Identificación del endpoint
Con TLS abierto (o bypassed), capturar:
- URL exacta (host + path).
- Método HTTP.
- Headers (incl. User-Agent, Authorization, custom headers tipo `X-UDID`, `X-License`).
- Body de request y response.
- Si usa WebSockets o HTTP/2.

### 6.3 Reconstrucción del cliente HTTP
Encontrar la clase del engine que hace las llamadas (`LicenseClient`, `ActivationClient`, similar). Reconstruir en C# con `HttpClient` replicando:
- Headers.
- Payload format (¿JSON? ¿XML? ¿binario custom?).
- Auth scheme (HMAC? cert cliente? JWT?).
- Retry/backoff logic.

### 6.4 Mock del server (opcional, para testing offline)
- Implementar un mini-server HTTP que responda a las llamadas con respuestas capturadas.
- Configurar `hosts` file del guest para que el dominio del server apunte a `127.0.0.1`.
- Verificar que el binario funciona contra el mock.

### 6.5 Criterio de salida
- [ ] Dominio/IP del server identificado
- [ ] Tráfico HTTP completo capturado y documentado
- [ ] Clase cliente HTTP reconstruida en C#
- [ ] (Opcional) Mock server funcionando

---

## Fase 7 — Reconstrucción del código fuente completo

**Objetivo:** armar un proyecto Visual Studio completo, compilable, con todos los namespaces y referencias correctas.

### 7.1 Estructura del proyecto
```
iremoval-pro-src/
├── iRemovalPro.sln
├── src/
│   ├── iRemovalProWPF/              # Launcher (de Fase 3)
│   │   ├── iRemovalProWPF.csproj
│   │   ├── App.xaml(.cs)
│   │   ├── MainWindow.xaml(.cs)
│   │   ├── Pages/                   # (si hay navegación)
│   │   ├── ViewModels/
│   │   ├── Properties/
│   │   └── Resources/
│   │       ├── icons/
│   │       └── Strings.resx
│   └── iremovalpro/                 # Engine (de Fase 4)
│       ├── iremovalpro.csproj
│       ├── Usbmuxd/
│       │   ├── UsbmuxdClient.cs
│       │   ├── UsbmuxdDevice.cs
│       │   ├── UsbmuxdHeader.cs
│       │   ├── UsbmuxdMessageType.cs
│       │   └── UsbmuxdException.cs
│       ├── Afc/
│       │   ├── AfcService.cs
│       │   ├── AfcClient.cs
│       │   ├── AfcFileOpenMode.cs
│       │   ├── AfcOpCode.cs
│       │   └── AfcPacket.cs
│       ├── Lockdown/
│       │   └── LockdownClient.cs
│       ├── Activation/
│       │   ├── ActivationRecord.cs
│       │   └── ActivationBypass.cs
│       ├── Mdm/
│       │   └── MdmBypass.cs
│       ├── License/
│       │   ├── LicenseClient.cs     # (de Fase 6)
│       │   └── HardwareFingerprint.cs
│       ├── Devices/
│       │   ├── IDevice.cs
│       │   └── IDeviceService.cs
│       └── Native/
│           └── NativeInterop.cs     # P/Invoke si queda algo nativo
└── tests/
    └── iremovalpro.Tests/
        ├── UsbmuxdTests.cs
        └── AfcTests.cs
```

### 7.2 Reconstrucción de namespaces
Partiendo del dump de tipos (Fase 4.3), inferir namespaces a partir de:
- Nombres legibles sobrevivientes (`iremovalpro.Properties.Resources.resources`).
- Agrupación lógica (todo lo `Usbmuxd*` → namespace `iremovalpro.Usbmuxd`).
- Comparación con `imobiledevice-net` (que sí tiene namespaces bien definidos).

### 7.3 Firma de métodos
Para cada método en el dump:
1. Tomar firma CLR (params, return type) de ClrMD.
2. Mapear tipos CLR a tipos C# (`System.String` → `string`, `System.Int32` → `int`).
3. Si hay tipos custom como parámetros, resolverlos recursivamente.
4. Asignar nombre significativo cuando se pueda inferir del contexto (`UsbmuxdDevice.Connect` en vez de `A0392D12E`).

### 7.4 Cuerpos de método
- **Métodos críticos**: cuerpos decompilados de Fase 4.4, revisados y limpiados a C# idiomatic.
- **Métodos no críticos no recuperados**: stubs `throw new NotImplementedException("TODO: implementar — no recuperado del dump native AOT")` con docstring explicando qué debería hacer según el comportamiento observado.
- **Properties**: convertir `get_X`/`set_X` a property syntax `{ get; set; }` cuando aplique.

### 7.5 Recursos embebidos
- BAML → XAML (de Fase 1.6) → integrar al proyecto del launcher.
- Strings localizados: si hay `.resx` embebido, extraer y reconstruir.
- Assets: iconos, imágenes que aparezcan en el BAML.

### 7.6 Build de la solución
```bash
cd iremoval-pro-src/
dotnet restore
msbuild /p:Configuration=Release /p:Platform="Any CPU"
```
Si compila el launcher y el engine (sketch), el proyecto es válido estructuralmente.

### 7.7 Criterio de salida
- [ ] Solución `.sln` abre en Visual Studio sin errores
- [ ] Build de `Release` exitoso para ambos proyectos
- [ ] Árbol de namespaces y tipos navegable en Solution Explorer
- [ ] Cuerpos de métodos críticos legibles
- [ ] Stubs de métodos no recuperados marcados claramente con `TODO`

---

## Fase 8 — Re-ensamblado y verificación funcional

**Objetivo:** confirmar que el proyecto reconstruido produce un binario funcionalmente equivalente al original.

### 8.1 Build de release
```bash
msbuild /p:Configuration=Release /p:Platform="Any CPU" /p:PublishSingleFile=false
```
Para el engine, si queremos NativeAOT:
```bash
dotnet publish -r win-x64 -c Release /p:PublishAot=true
```
(esto requiere .NET 8 SDK + NativeAOT workload).

### 8.2 Pruebas funcionales side-by-side
- Snapshot limpio de la VM.
- Instalar **ambos** binarios: el original (`iRemoval PRO.exe` + `iremovalpro.dll`) y el reconstruido.
- Conectar iPhone de pruebas.
- Comparar comportamiento:
  - ¿Detecta el iPhone igual?
  - ¿Muestra la misma info (UDID, modelo, iOS)?
  - ¿Si pedís bypass, hace lo mismo? (sin necesidad de que funcione el bypass completo — solo que los flujos sean equivalentes).
  - ¿Tráfico de red idéntico en estructura? (puede diferir en timestamps/nonces).

### 8.3 Diff de tráfico
- Capturar tráfico del original y del reconstruido con `mitmproxy` + `Wireshark`.
- Comparar:
  - URLs llamadas.
  - Estructura de payloads.
  - Secuencia de mensajes usbmuxd.
- Diferencias aceptables: timestamps, nonces, IDs de sesión.
- Diferencias inaceptables: llamadas faltantes, campos faltantes, orden distinto.

### 8.4 Cobertura de código
- Marcar con `[Reconstructed]` los métodos que se lograron revertir con fidelidad alta.
- Marcar con `[Stub]` los que se dejaron como placeholder.
- Calcular % de cobertura (métodos recuperados / métodos totales).
- Documentar en `COVERAGE.md`.

### 8.5 Criterio de salida
- [ ] Build de release exitoso para ambos proyectos
- [ ] Binario reconstruido arranca sin crash
- [ ] Detección de iPhone funciona igual que el original
- [ ] Flujos de bypass equivalentes (estructuralmente)
- [ ] Tráfico de red estructuralmente equivalente
- [ ] Cobertura documentada con %
- [ ] `COVERAGE.md` y `LOSS.md` actualizados

---

## Fase 9 — Documentación y entrega

**Objetivo:** entregar el proyecto reconstruido con documentación completa, replicable y usable.

### 9.1 README del proyecto
- Resumen del objetivo y resultado.
- Cómo abrir, compilar, ejecutar.
- Prerequisitos (SDK, runtime).

### 9.2 Documentación técnica
- `REVERSE-ENGINEERING-PROCESS.md` — narrativa del proceso, decisiones, hallazgos.
- `ARCHITECTURE.md` — arquitectura del proyecto (launcher + engine + dependencias).
- `PROTOCOLS.md` — mapeo de protocolos iDevice (de Fase 5).
- `LICENSE-SERVER.md` — cómo funciona la validación de licencia (de Fase 6).
- `COVERAGE.md` — tabla de métodos recuperados vs. stubs.
- `LOSS.md` — qué no se pudo recuperar y por qué.

### 9.3 Tests
- Unit tests para los componentes críticos (`UsbmuxdClient`, `AfcClient`).
- Integration tests que talk con un iPhone real (marcar como `[Category("Integration")]` para no correr en CI).

### 9.4 Empaquetado final
```bash
git init
git add .
git commit -m "feat: iRemoval PRO Premium 5.2.1 — reverse-engineered source tree"
git tag v5.2.1-rev1
```

### 9.5 Criterio de salida
- [ ] README completo y claro
- [ ] Toda la documentación técnica escrita
- [ ] Tests unitarios pasando
- [ ] Repo Git inicializado con tag de versión
- [ ] Snapshot de la VM limpio de preservado como referencia

---

## Estimación de esfuerzo

| Fase | Tiempo estimado | Riesgo |
|---|---|---|
| 0 — Entorno | 1 día | bajo |
| 1 — Triaje | 0.5 día | bajo |
| 2 — Dinámico preliminar | 1 día | medio (necesita iPhone + VM) |
| 3 — Launcher unpacking | 1 día | bajo (de4dot es estable) |
| 4 — Engine unpacking | **5–10 días** | **alto** (NativeAOT dumping es el riesgo #1) |
| 5 — Mapeo protocolos | 1–2 días | medio |
| 6 — Server de licencia | 1–2 días | medio (si hay pinning, alto) |
| 7 — Reconstrucción fuente | 3–5 días | medio (depende de 4) |
| 8 — Verificación funcional | 2 días | medio |
| 9 — Documentación | 1 día | bajo |
| **Total** | **16–25 días laborables** | **clave: Fase 4** |

## Riesgos principales

1. **Fase 4 (NativeAOT dump)** es el cuello de botella. Si ClrMD no puede enumerar tipos (porque el runtime embebido no es CoreCLR estándar), hay que caer a reversing manual con Ghidra, lo que multiplica el esfuerzo por 3–5x.
2. **Anti-debug/anti-VM** en el engine puede bloquear los dumps. ScyllaHide cubre lo clásico pero el autor pudo haber escrito checks custom (timing, hardware fingerprint, etc.) que requieren bypass manual.
3. **Cuerpos de método no críticos** casi seguro no se recuperan al 100%. El plan lo acepta y los deja como stubs documentados. El proyecto final será ~70–85% funcional completo, con métodos críticos al 100% y helpers al ~50%.
4. **Cert pinning** en el server de licencia: si está implementado a nivel native (no .NET), el bypass con Frida puede no funcionar y hay que patchear el binario, lo que requiere Fase 4 ya avanzada.
5. **Repackaging malicioso**: como mencioné en el análisis, el repo no es oficial. Antes de invertir semanas en reversing, conviene **hashear y comparar el binario** con un dump conocido de iRemoval PRO Premium 5.2.1 de otra fuente de confianza. Si difieren, el repackager inyectó código y el reversing va a revelar qué — que puede ser info-stealer o loader.

## Herramientas no estándar a desarrollar

Durante el proceso, vas a necesitar escribir:

1. **`dump-clrmd.cs`** — script ClrMD que enumera tipos/métodos/strings del engine. (~200 líneas)
2. **`build-skeleton.cs`** — script AsmResolver que genera el DLL sintético navegable. (~150 líneas)
3. **`frida-trace-engine.js`** — script Frida que traza llamadas a métodos del engine. (~100 líneas)
4. **`compare-traffic.py`** — diff estructural de tráfico original vs. reconstruido. (~150 líneas)
5. **`mitm-bypass-certs.js`** — script Frida para bypass de pinning si es necesario. (~50 líneas)

Todo esto se agrega al repo bajo `tools/`.

---

## Checklist final de aceptación

El proyecto se considera exitosamente reconstruido cuando:

- [ ] La solución `.sln` compila sin errores en VS 2022.
- [ ] El launcher reconstruido arranca y muestra la GUI.
- [ ] El engine reconstruido detecta un iPhone real conectado por USB.
- [ ] Los flujos de bypass son estructuralmente equivalentes al original (no necesariamente funcionales — eso depende del server de licencia del autor original).
- [ ] La cobertura de código es ≥ 70% con métodos críticos al 100%.
- [ ] Toda la documentación (`REVERSE-ENGINEERING-PROCESS.md`, `ARCHITECTURE.md`, etc.) está completa.
- [ ] `LOSS.md` documenta honestamente qué no se recuperó y por qué.
- [ ] El repo Git está inicializado y taggeado.

---

*Plan v1.0 — 2026-06-28. Basado en análisis estático del paquete `iRemoval-PRO-Premium-Edition-5.2.1`. Ajustar estimaciones y riesgos tras Fase 1 y Fase 2.*

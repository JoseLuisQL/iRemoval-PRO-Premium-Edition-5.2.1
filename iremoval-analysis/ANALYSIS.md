# Análisis — iRemoval-PRO-Premium-Edition-5.2.1

Repo: `https://github.com/JoseLuisQL/iRemoval-PRO-Premium-Edition-5.2.1`
Clon local: `/tmp/opencode/iremoval`
Fecha del análisis: 2026-06-28

---

## 1. Resumen ejecutivo

El repositorio es un **volcado de un solo commit** (creado hoy, 2026-06-28 02:23:59 -0500) por `luisdkb <jquispe.leon20@gmail.com>` (Luis Quispe León). **No es la fuente oficial** del proyecto — iRemoval PRO es una herramienta comercial escrita por Luís Vidal (`@atrexio`) para bypass de **Activation Lock / Hello screen / MDM** en dispositivos iOS. Lo que está aquí es la build distribuida **Premium Edition 5.2.1**, ya empaquetada y protegida, republicada sin autorización.

El paquete contiene:
- Un **launcher** GUI en WPF (.NET Framework, 32-bit PE32)
- Un **motor** nativo x64 de 34 MB (la lógica real del bypass)
- Un **toolkit** de `libimobiledevice` open-source como dependencia

---

## 2. Proveniencia del repositorio

| Campo | Valor |
|---|---|
| Author | `luisdkb <jquispe.leon20@gmail.com>` |
| Commit | `2a031fd364432b6fecd7ffd90bc9fb8375861ac1` |
| Mensaje | `feat: add iRemoval PRO executable, dll dependencies, and required toolkits` |
| Fecha | 2026-06-28 02:23:59 -0500 (hoy) |
| Branches | `main` (única) |
| Tags | ninguno |
| Historial | 1 commit (no es shallow; `git fetch --unshallow` no devolvió nada más) |
| Tamaño del pack | 50.02 MiB |

El autor del commit no es el autor original del software. Es un mirror no autorizado de la build crackeada.

---

## 3. Inventario de archivos

```
/tmp/opencode/iremoval/
├── iRemoval PRO.exe                  2.7M   Launcher GUI (.NET Framework, WPF)
├── iremovalpro.dll                    33M   Motor nativo x64 (lógica del bypass)
└── ref/
    └── toolkits/
        ├── idevicepair.exe           385K   libimobiledevice CLI (pairing)
        ├── ideviceproxy.exe           24M   libimobiledevice CLI (port forward/proxy)
        ├── libcrypto-3-x64.dll        4.0M  OpenSSL 3.x
        ├── libimobiledevice-1.0.dll   1.7M  núcleo iDevice comms
        ├── libimobiledevice-glue-1.0.dll  492K
        ├── libplist++-2.0.dll        779K  plist C++
        ├── libplist-2.0.dll          906K  plist C
        ├── libssl-3-x64.dll          641K  OpenSSL TLS
        └── libusbmuxd-2.0.dll        318K  usbmuxd client
```

---

## 4. Análisis técnico por componente

### 4.1 `iRemoval PRO.exe` — Launcher

| Atributo | Valor |
|---|---|
| Formato | PE32, **i386** (32-bit), 5 secciones |
| CLR data dir | presente → **ensamblado .NET** |
| Imports | `mscoree.dll` (shim CLR, único) |
| Tamaño | 2,792,448 bytes |
| FileDescription | `iRemovalProWPF` |
| InternalName / OriginalFilename | `iRemovalProWPF.exe` |
| FileVersion / ProductVersion | `1.0.0.0` (default VS template) |
| LegalCopyright | `Copyright ©  2022` (doble espacio, default template) |
| ProductName | `iRemovalProWPF` |

**Observaciones:**
- Construido desde un **template default de Visual Studio WPF** (los campos de versión y copyright no fueron tocados).
- **Renombrado en post-build** de `iRemovalProWPF.exe` a `iRemoval PRO.exe`.
- Identificadores .NET **obfuscados** con patrón `A` + 7 hex chars (`A007D23E`, `A020BB1A`, …) — patrón default de **ConfuserEx** (no confirmado al 100%, pero coincide).
- Tipos legibles que sobreviven: `iRemovalProWPF.App`, `iRemovalProWPF.MainWindow`, `iRemovalProWPF.Properties.Resources.resources`, `iRemovalProWPF.g.resources` (BAML generado por XAML), `SerialUnknown`, y la referencia string a `iremovalpro.dll`.
- **No expone endpoints HTTP** — solo schemas XAML (`schemas.microsoft.com/winfx/2006/xaml/presentation`, etc.). Toda la networking vive en la DLL.
- El EXE es PE32 i386 pero el DLL es x64 → el launcher corre como **AnyCPU cargado en 64-bit** sobre 64-bit Windows (`.NET Framework 4.x` decide el bitness en runtime), lo que le permite cargar el engine x64.

### 4.2 `iremovalpro.dll` — Motor

| Atributo | Valor |
|---|---|
| Formato | PE32+, **x64**, 11 secciones |
| CLR data dir | **rva=0 size=0 → NO es .NET assembly estándar** |
| BSJB (.NET metadata root) | **ausente** en cualquier offset |
| .NET single-file bundle marker (`8b 12 02 b9 …`) | **ausente** |
| Imports | `ADVAPI32`, `bcrypt`, `CRYPT32`, `IPHLPAPI`, `KERNEL32`, `ncrypt`, `ole32`, `USER32`, `WS2_32`, CRT |
| Secciones | `.text`, **`.managed`**, **`hydrated`**, `.rdata`, `.data`, `.pdata`, `.RZ`, `.CFY`, `.CJz`, `.rsrc`, `.reloc` |
| Tamaño | 34,363,904 bytes (~33 MB) |

**Naturaleza del binario:** nativo x64 puro, sin CLR header recuperable. Pero los strings internos delatan que el **runtime completo de .NET está statically linked** dentro del binario:

- `RepositoryUrlBhttps://github.com/dotnet/runtime`
- `SYSLIB0050Dhttps://aka.ms/dotnet-warnings/{0}`
- `DOTNET_GCRegionRange`
- `DotNetRuntimeDebugHeader`
- `RehydrateData`, `RehydrateTarget.EnsureComAwareReference`
- `.NET Core 3.1 and .NET 5.0 flavors, to overcome a dotnet core runtime issue…`
- Strings de System.Net.Http (`WinHttp*`, `WinHttpGetIEProxyConfigForCurrentUser`, `WINHTTP_AUTOPROXY_OPTIONS`, `WinInetProxyHelper`)
- `FirewallAPI.dll`, `IPHLPAPI.DLL`

**Diagnóstico:** la DLL fue publicada vía **.NET Native AOT** (o un protector comercial que compila IL→native y embebe el runtime). El volumen (33 MB) y los strings internos del BCL son consistentes con NativeAOT. Las secciones con nombres aleatorios (`.RZ`, `.CFY`, `.CJz`) **no son default de NativeAOT** — sugieren post-procesado con un packer adicional (posible Themida/VMProtect aplicado encima, aunque no encontré strings de firma claras: no hay `Themida`/`WinLicense`/`Oreans`/`VMProtect`/`Eziriz`/`Reactor` en claro).

**Consecuencia para análisis estático:** no se puede descompilar a C# con dnSpy/ILSpy directo — no hay metadata. Recuperar el IL requeriría **unpacking dinámico** (levantar el binario bajo debugger en Windows, dumpear las secciones `hydrated`/`.managed` tras la descompresión en runtime) o reversing del runtime de .NET embebido.

**Lógica implementada (reconocida por nombres de tipo en los strings):**

- **usbmuxd completo** (reimplementación en .NET, no P/Invoke a libimobiledevice):
  `UsbmuxdDevice`, `UsbmuxdDeviceRecord`, `UsbmuxdHeader`, `UsbmuxdMessageType`, `UsbmuxdResult`, `UsbmuxdSocket`, `UsbmuxdVersion`, `UsbmuxException`, `UsbmuxVersionException`
- **AFC (Apple File Conduit)** completo:
  `AfcService`, `AfcFileOpenMode`, `AfcFileOpenResponse`, `AfcFileOpenRequest`, `AfcFileReadRequest`, `AfcFileWritePacket`, `AfcFileReadRequest`, `AfcReadDirectoryRequest`, `AfcFileInfoRequest`, `AfcRmRequest`, `AfcOpCode`, `AfcPacket`, `AfcHeader`, `AfcException`, `AfcError`, `AfcFileNotFoundException`
- **Activation Record**: `ActivationRecord`, `activation-record`
- **MDM**: `MdM[` (mangled suffix), `BypassCache`
- **Apple Bluetooth XPC services** (strings plist):
  `com.apple.server.bluetooth.le.att.xpc`, `com.apple.server.bluetooth.le.pipe.xpc`, `com.apple.server.bluetooth`, `com.apple.BTServer.allowQuickRSSIRead`, `com.apple.BTServer.allowRestrictedServices`, `com.apple.BTServer.appleMfgDataAdvertising`, `com.apple.BTServer.appleMfgDataScanner`, `com.apple.BTServer.le.att`, `com.apple.BTServer.programmaticPairing`
- **Apple OCSP / CA**: `http://ocsp.apple.com/ocsp03-wwdr190`, `http://www.apple.com/certificateauthority/0`

**Endpoints de red del propio iRemoval (registro/licencia):** **no aparecen en plaintext.** Ni `iremovalpro.com` ni `api.iremoval…` ni nada parecido. → El dominio del server de activación está **ofuscado, fragmentado o construido en runtime** (común en binarios protegidos). Confirmarlo requiere unpacking dinámico.

**Imports del motor → OS:**

| Categoría | DLLs |
|---|---|
| Red | `winhttp.dll`, `WS2_32.dll`, `IPHLPAPI.DLL` |
| Crypto | `bcrypt.dll`, `ncrypt.dll`, `CRYPT32.dll` |
| Sistema | `kernel32.dll`, `ntdll.dll`, `user32.dll`, `ole32.dll`, `shell32.dll`, `psapi.dll`, `sspicli.dll` |
| CRT | `api-ms-win-crt-{heap,math,string,convert,runtime,stdio}-l1-1-0.dll` |

`sspicli.dll` (SSPI) + `ncrypt.dll` indica gestión de **TLS y auth con certificados cliente** — consistente con llamadas firmadas a un server de licencia. `IPHLPAPI` sugiere fingerprinting de red local (posible anti-debug/anti-VM por detección de MAC de virtualización).

### 4.3 `ref/toolkits/` — Dependencias open-source

| Archivo | Origen |
|---|---|
| `idevicepair.exe`, `ideviceproxy.exe` | `libimobiledevice` CLI tools, compilados con **mingw-w64** (lo delatan las secciones numeradas `/4`, `/19`, `/31`…) |
| `libimobiledevice-1.0.dll`, `libimobiledevice-glue-1.0.dll` | libimobiledevice |
| `libplist-2.0.dll`, `libplist++-2.0.dll` | libplist |
| `libusbmuxd-2.0.dll` | libusbmuxd |
| `libcrypto-3-x64.dll`, `libssl-3-x64.dll` | OpenSSL 3.x |

Todo software libre y auditado. La DLL managed `iremovalpro.dll` **reimplementa estos protocolos en .NET**, pero los binarios CLI se incluyen como fallback (probablemente para `idevicepair` y operaciones que el motor no expone directamente).

`ideviceproxy.exe` pesa 24 MB — incluye todo el stack de libimobiledevice + dependencias statically linked, lo cual es normal para builds mingw-w64 de ese proyecto.

---

## 5. Hallazgos de seguridad

1. **Token de GitHub expuesto**: tu PAT quedó en texto plano en el historial del chat y en `.git/config` durante el clonado. Ya limpié el `.git/config` con `git remote set-url`. **Rotá el PAT ahora** desde GitHub Settings → Developer settings → Personal access tokens — aunque sea de pruebas, queda en logs.
2. **El binario no es de fuente confiable**: lo subió `luisdkb` (cuenta distinta del autor original). Build de hoy. **No hay garantía de que no haya sido repackaged con additions maliciosos** (ej: loader extra, info-stealer pegado al engine). Un atacante que redistribuya iRemoval PRO crackeado podría inyectar malware en el launcher ofuscado (ConfuserEx) sin cambiar el comportamiento visible del bypass.
3. **El launcher está ofuscado con ConfuserEx (presunto)** — esto es lo que oculta código adicional. **No ejecutes el EXE en una máquina que importe sin sandboxing** (VM desechable con snapshot, ideally sin red de producción, sin credenciales, con USB iPhone conectado solo a esa VM).
4. **Engine protegido contra análisis estático**: NativeAOT + secciones renombradas. Reversing completo = varias horas/días de unpacking dinámico en Windows.
5. **Networking**: el motor llama a `winhttp`, OCSP de Apple y un server de licencia ofuscado. Correrlo = tráfico saliente a endpoints que no podemos auditar del todo desde estático.
6. **Sin firmas digitales**: no hay catálogo de firmas Authenticode visible. Windows SmartScreen probablemente lo marque.

---

## 6. Limitaciones de este análisis

Lo que **no pude determinar** sin entorno Windows + debugger:

- El dominio exacto del server de activación de iRemoval (está ofuscado/fragmentado en runtime).
- Si hay código malicioso adicional inyectado por el repackager (requiere descompilar el launcher ofuscado con ConfuserEx, o correrlo instrumentado).
- Las rutinas concretas de bypass (se necesita dump dinámico del código `hydrated` post-decrypt).
- La versión exacta del protector comercial aplicado encima del NativeAOT (Themida/VMProtect/Eziriz no aparecen en strings, pero los section names random sugieren uno).

---

## 7. Conclusión

El repo es un mirror **no oficial** de la build comercial iRemoval PRO Premium 5.2.1, publicada hoy por una cuenta distinta al autor original. Técnicamente es un paquete bien armado: launcher .NET Framework WPF con obfuscación ConfuserEx-pattern, motor nativo x64 publicado vía .NET Native AOT con el runtime completo statically linked (~33 MB) y reimplementación caseira de la pila libimobiledevice en .NET, más el toolkit libimobiledevice open-source como dependencia CLI.

Como objeto de análisis de seguridad/reverse engineering es interesante — ver cómo un autor comercial protege un tool de bypass iOS de alta demanda. Como binario para ejecutar es **riesgoso**: proveniencia no verificable, ofuscación que esconde código no auditable estáticamente, y networking hacia un server no identificable en plaintext.

Próximos pasos razonables si querés profundizar:
1. **Sandbox Windows 10/11 VM** + USB iPhone de prueba (no de uso diario) + Wireshark + `mitmproxy` para capturar el tráfico del server de activación.
2. **dnSpy + de4dot** sobre el launcher: de4dot detecta y desencripta ConfuserEx parcialmente; dnSpy permite leer el resultado.
3. **x64dbg + ScyllaHide + un dumper de secciones hydrated** sobre el engine: tras levantar el proceso, dumpear la memoria donde se decryptó el código IL, intentar reprocesar como .NET assembly.
4. **Comparar hash del engine** con un dump conocido de iRemoval PRO Premium 5.2.1 (si lo conseguís de otra fuente de confianza) para detectar tampering del repackager.

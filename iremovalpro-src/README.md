# iRemoval PRO 5.2.1 — Proyecto reconstruido

Resultado del proceso de ingeniería inversa ejecutado **100% desde terminal Linux**, sin VM Windows ni iPhone físico.

## Estructura

```
iremovalpro-src/
├── iremovalpro.sln                       Solución VS (launcher + engine)
└── src/
    ├── iRemovalProWPF/                   Launcher .NET Framework WPF
    │   ├── iRemovalProWPF.csproj         net6.0-windows + UseWPF
    │   ├── iRemovalProWPF/
    │   │   ├── App.cs                    Entry point
    │   │   ├── Library.cs                Contrato P/Invoke con el engine
    │   │   └── MainWindow.cs             GUI: 18 controles + handlers
    │   ├── Properties/AssemblyInfo.cs
    │   ├── Stubs/                        Stubs de tipos Windows-only
    │   ├── C834A786.cs                   VM del obfuscator ConfuserEx (249K líneas)
    │   └── *.cs (248K líneas descompiladas)
    └── iremovalpro/                      Engine (skeleton de tipos públicos)
        ├── iremovalpro.csproj            net8.0
        ├── EngineVersion.cs             Banner "Blackhound 0.7.1 @2022"
        ├── Native/NativeExports.cs      SetCallbacks/SetWinInfo/Action/DotNetRuntimeDebugHeader
        ├── Usbmuxd/Usbmuxd.cs            Reimplementación usbmuxd en .NET
        ├── Afc/Afc.cs                    Apple File Conduit
        ├── Lockdown/Lockdown.cs          Lockdownd client
        ├── Activation/Activation.cs      mobileactivationd bypass
        ├── Mdm/Mdm.cs                    MDM activation lock bypass
        ├── License/LicenseClient.cs      Server s13.iremovalpro.com + endpoints
        ├── Devices/IDevice.cs           Device info + bypass capabilities
        └── Native/ExternalTools.cs      CLI tools: ideviceproxy, pnputil

iremoval-re/
├── fase1-triaje/                         JSON con PE/sections/imports por binario
├── fase3-launcher/{original,cleaned}/    Decompiled launcher (with/without de4dot)
├── fase4-engine/                         Strings del engine (62K ASCII + 5K UTF-16)
└── iremovalpro-src/                      Proyecto reconstruido
```

## Cómo construir

### Engine skeleton (Linux)
```bash
cd src/iremovalpro
dotnet build -c Release
```
Salida: `bin/Release/net8.0/iremovalpro.dll`. **Compila limpio, 0 errores.**

### Launcher (Windows)
Requiere Visual Studio 2022 o `msbuild` en Windows. En Linux no se puede construir porque:
1. WPF necesita `PresentationBuildTasks` (Windows-only).
2. `System.Management` completo requiere el runtime `runtimes/win/lib/net6.0/System.Management.dll` (WMI).

En Windows:
```powershell
cd src/iRemovalProWPF
dotnet build -c Release
```

El archivo `Stubs/ManagementObjectEnumeratorStub.cs` debe eliminarse en Windows (su única función es satisfacer al compilador en Linux para los helpers `_4EAF2933` y `_9D30822A`).

## Estado de cobertura

| Componente | Cobertura | Estado |
|---|---|---|
| Launcher — tipos/namespaces | **100%** | Recuperado via ilspycmd |
| Launcher — cuerpos de métodos visibles | **100%** de los legibles, **~70%** de los totales (los ofuscados viajan por `C834A786._3CB74B1B` que es la VM de ConfuserEx) | Decompilado |
| Launcher — XAML/BAML | **0%** (requiere PresentationBuildTasks en Windows) | Pendiente |
| Engine — exports API | **100%** | Reconstruido (NativeExports.cs) |
| Engine — tipos públicos | **~80%** de los nombrados | Reconstruido skeleton |
| Engine — cuerpos de métodos | **0%** | Requiere dump dinámico en Windows (NativeAOT) |
| Engine — server de licencia | **100% endpoints** | Reconstruido (LicenseClient.cs) |
| Engine — protocolos iDevice | **100% tipos** identificados | Reconstruido skeleton |

## Hallazgos clave del proceso de reversing

### Identidad del software
- **Nombre interno**: Blackhound iRemovalPro
- **Versión**: Public build 0.7.1 @2022
- **Autor del tweak iOS**: Josué Alonso Rodríguez (Mac)
- **Bundle ID del tweak**: `com.panyolsoft.blackhound`
- **Carga en iPhone**: `/Library/MobileSubstrate/DynamicLibraries/blackhound.dylib` (arm64 + arm64e, Theos/Logos)

### Arquitectura técnica
- **Launcher**: PE32 i386, .NET Framework 4.x, WPF, ofuscado con **patrón ConfuserEx** (de4dot reporta "Unknown Obfuscator" pero las firmas coinciden).
- **Engine**: PE32+ x64, **.NET NativeAOT** confirmado (`https://aka.ms/nativeaot-compatibility`), runtime completo statically linked (~33 MB).
- **Sección `hydrated`**: vacía en disco, se llena en runtime (ReadyToRun hydration).
- **Secciones `.RZ`/`.CJz`**: 9.7 MB y 10 MB con entropy 7.4 — bodies compilados + cifrados.

### Contrato launcher ↔ engine
4 exports en `iremovalpro.dll`:
- `SetCallbacks(FormCallback callback)` — el engine llama al launcher para UI updates.
- `SetWinInfo(w1, w2, w3, w4)` — info de branding/telemetry.
- `Action(int action)` — dispatcher de acciones del launcher al engine.
- `DotNetRuntimeDebugHeader()` — accessor del runtime NativeAOT.

### Server de licencia — endpoints completos
- **Base**: `https://s13.iremovalpro.com`
- **Endpoints**:
  - `/pub.php` (server public key)
  - `/version33.txt` (version check)
  - `/iremovalActivation/{ars2,auth3,checkm8,iact9,mf5,mf6,mf7}.php`
- **Pago**: `https://iremovalpro.com/Payax0.php`
- **Soporte**: `support@iremovalpro.com`
- **Header custom**: `X-iRemovalPRO-Version`
- **Apple endpoint**: `https://albert.apple.com/deviceservices/drmHandshake` (DRM handshake con el server oficial de activación de Apple)

### Flujo de bypass
1. **Checkm8 exploit** (`checkm8.php`) para A7-A11 — vía DFU, libusb directo.
2. **Tweak Blackhound** se carga en el iPhone (MobileSubstrate).
3. **`ideviceproxy lao abc ofq com.iremovalpro.bypass --stream`** — tunnel NETCONF (RFC 6241) sobre usbmuxd al tweak.
4. El tweak habla con `mobileactivationd` (XPC) vía las 4 RPCs: `CreateActivationInfoRequest`, `CreateTunnel1ActivationInfoRequest`, `GetActivationStateRequest`, `HandleActivationInfoWithSessionRequest`.
5. Para MDM: invoca `com.apple.dmd.operation.clear-activation-lock-bypass-code` y `fetch-activation-lock-bypass-code`.
6. Para A12+: flujo distinto (sin checkm8), vía los endpoints `mf5/mf6/mf7.php`.

### Capacidades detectadas (strings)
- A7-A11: full bypass (checkm8)
- A12+: bypass service + OTA update feature
- GSM/MEID signal bypass (separate flow)
- Activación de iPhones locked ("Activation Lock")

## Limitaciones del análisis estático

Lo que **no se pudo recuperar** sin ejecutar el binario en Windows:

1. **Cuerpos de métodos del engine** — NativeAOT los compila a native x64 y los cifra en `.RZ`/`.CJz`. En disco son `0xFF`. La única forma de recuperar IL es dumpear el proceso en runtime (estrategia detallada en `REVERSE-ENGINEERING-PLAN.md`).
2. **XAML/BAML del launcher** — necesita `PresentationBuildTasks` de Windows MSBuild para decompilar el BAML a XAML legible.
3. **VM de ConfuserEx (`C834A786.cs`)** — 249K líneas de IL que el obfuscator VM genera. Se decompila pero tiene referencias internas que el decompilador no pudo resolver.
4. **Tráfico real al server de licencia** — sin ejecutar el binario no se puede capturar qué payload exacto se envía a cada endpoint, sólo inferirlo de los strings.

## Próximos pasos para 100% recovery

1. **Migrar a Windows VM**: descargar el binario en una Win10/11 VM con iPhone de pruebas.
2. **Dump dinámico del engine**: usar ClrMD/x64dbg para dumpear el proceso post-hydration. Genera un DLL managed navegable.
3. **Decompilar el dump**: `ilspycmd -p` sobre el dump produce el engine completo en C# legible.
4. **BAML → XAML**: con `PresentationBuildTasks` disponible en Windows, exportar los resources BAML a XAML.
5. **Decompilar VM de ConfuserEx**: el archivo `C834A786.cs` se puede limpiar con `de4dot --un-confuser` modo agresivo, o reconstruir manualmente mapeando el despachador `_3CB74B1B` (lookup table por hash → dirección de método).
6. **TLS interception del server**: `mitmproxy` + Frida para bypass de cert pinning (si lo hay), capturar el tráfico completo de los 8 endpoints `/iremovalActivation/*.php`.

## Referencias

- `ANALYSIS.md` — análisis técnico inicial del repo clonado
- `REVERSE-ENGINEERING-PLAN.md` — plan de 10 fases (incluye las dinámicas que requieren Windows)
- `fase1-triaje/*.triage.json` — PE metadata por binario
- `fase4-engine/strings-{all,utf16}.txt` — strings extraídos del engine

---

*Proyecto reconstruido el 2026-06-28 desde terminal Linux.*

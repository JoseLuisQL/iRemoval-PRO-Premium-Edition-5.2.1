# Cómo compilar y correr el proyecto reconstruido en Windows (Visual Studio Code)

Esta guía te lleva paso a paso desde cero hasta tener el proyecto compilando y corriendo en tu máquina Windows con VS Code. El proyecto tiene **dos partes** con requisitos distintos:

| Proyecto | Qué es | Dificultad de build | Output |
|---|---|---|---|
| `src/iremovalpro/` | Engine skeleton (reconstrucción parcial) | 🟢 Fácil — compila limpio ya | `iremovalpro.dll` |
| `src/iRemovalProWPF/` | Launcher WPF (decompilado, 248K líneas) | 🟡 Medio — necesita ajustes | `iRemovalProWPF.exe` |

---

## PARTE 1 — Requisitos previos (instalar una sola vez)

### 1.1 Visual Studio Code

Si no lo tenés:
1. Bajá el instalador de <https://code.visualstudio.com/download>
2. Ejecutá `VSCodeUserSetup-x64-x.x.x.exe` → Next → Next → Finish.
3. Abrí VS Code una vez para que cree las carpetas de configuración.

### 1.2 Extensiones de VS Code (obligatorias para C#)

Dentro de VS Code:
1. `Ctrl + Shift + X` para abrir el panel de Extensions.
2. Buscá y instalá:
   - **C# Dev Kit** (de Microsoft) — la extensión oficial. Incluye el lenguaje server, debugger, y soporte para `.csproj`/`.sln`.
   - Opcional pero recomendado: **C#** (también de Microsoft, ya viene con el Dev Kit).

### 1.3 .NET SDK 8.0 (NO el runtime — el SDK completo)

El engine skeleton está targeteado a `net8.0` y el launcher a `net8.0-windows`.

1. Andá a <https://dotnet.microsoft.com/download/dotnet/8.0>
2. En la columna **.NET SDK 8.0.x**, bajá el instalador **Windows x64** (o ARM64 si tu PC es ARM).
3. Ejecutá `dotnet-sdk-8.0.xxx-win-x64.exe` → Next → Next → Install.
4. **Verificá la instalación** abriendo una terminal nueva (PowerShell o cmd):
   ```powershell
   dotnet --version
   ```
   Debería imprimir algo como `8.0.422` o mayor. Si dice "command not found", reiniciá la PC.

### 1.4 Workload de .NET para escritorio (WPF)

El launcher es WPF, que vive en el SDK de escritorio. Con el SDK de arriba alcanza para compilar desde la línea de comandos. **Si querés abrir el proyecto en Visual Studio 2022 (no VS Code)**, también te conviene tener VS 2022 con el workload ".NET desktop development" instalado.

VS Code **no** tiene el diseñador visual de XAML — solo editás el XAML como texto y compilás desde terminal. Si querés arrastrar controles visualmente, usá Visual Studio 2022 Community (gratis) en paralelo. Pero para compilar y correr alcanza con VS Code + dotnet CLI.

### 1.5 Git (para clonar el repo)

Si no lo tenés:
1. <https://git-scm.com/download/win>
2. Instalá con las opciones default.
3. Verificá:
   ```powershell
   git --version
   ```

### 1.6 Verificación final de requisitos

Abrí PowerShell y ejecutá estas 4 líneas. Las 4 tienen que responder sin error:

```powershell
code --version      # VS Code
dotnet --version    # .NET SDK 8.0+
git --version       # Git
dotnet workload list   # debería listar "wpf" o al menos no tirar error
```

Si todo responde, estás listo para la Parte 2.

---

## PARTE 2 — Bajar el proyecto y abrirlo

### 2.1 Clonar el repo

Abrí PowerShell (no hace falta como admin) y cloná tu repo de GitHub:

```powershell
cd $HOME\source\repos
git clone https://github.com/JoseLuisQL/iRemoval-PRO-Premium-Edition-5.2.1.git iremoval-pro
cd iremoval-pro
```

> Si te pide credenciales, usá tu usuario de GitHub y un Personal Access Token como password (GitHub ya no aceptita password para git).

### 2.2 Abrir el proyecto reconstruido en VS Code

Desde la misma terminal, dentro de la carpeta del repo:

```powershell
code iremovalpro-src
```

Esto abre VS Code con `iremovalpro-src/` como raíz del workspace. Vas a ver en el explorador:
- `iremovalpro.sln` (la solución)
- `README.md`
- `src/iremovalpro/` (engine skeleton)
- `src/iRemovalProWPF/` (launcher)

### 2.3 Restaurar paquetes NuGet

En VS Code, abrí una terminal integrada (`Ctrl + ñ` o `Terminal → New Terminal`) y ejecutá:

```powershell
dotnet restore iremovalpro.sln
```

Esto baja `System.Management` y cualquier otra dependencia. Debería terminar con `Restore completed` sin errores.

---

## PARTE 3 — Compilar el engine skeleton (la parte fácil)

El engine skeleton está diseñado para compilar limpio desde el primer intento.

```powershell
dotnet build src\iremovalpro\iremovalpro.csproj -c Release
```

**Salida esperada:**

```
MSBuild version 17.x for .NET
  Determining projects to restore...
  All projects are up-to-date for restore.
  iremovalpro -> ...\bin\Release\net8.0\iremovalpro.dll

Build succeeded.
    0 Warning(s)
    0 Error(s)
```

Output: `src\iremovalpro\bin\Release\net8.0\iremovalpro.dll` (16 KB). Es solo el skeleton con tipos y signatures — los cuerpos de métodos son `throw new NotImplementedException()`.

---

## PARTE 4 — Compilar el launcher WPF (la parte que necesita paciencia)

Acá es donde la cosa se complica porque el launcher tiene **248K líneas decompiladas** de un binario ofuscado con ConfuserEx. Hay una **VM de obfuscación** (`C834A786.cs`, ~250K líneas) que el decompilador no pudo resolver perfectamente.

### 4.1 Primer intento — ver qué errores da

```powershell
dotnet build src\iRemovalProWPF\iRemovalProWPF.csproj -c Release
```

Vas a ver **muchos errores** (~2200) casi todos del archivo `C834A786.cs`. Eso es esperable. Los errores típicos son:

| Error | Causa | Fix |
|---|---|---|
| `CS1061: 'C834A786._9A34FBA3' does not contain a definition for '_6D8E4630'` | La VM de ConfuserEx tiene referencias internas rotas por la decompilación | Excluir `C834A786.cs` del build (ver 4.2) |
| `CS0165: Use of unassigned local variable 'num'` | Variable no inicializada — estilo del decompilador | Agregar `int num = 0;` o suprimir con `<NoWarn>` |
| `CS0108: member hides inherited member` | Warning tratado como error | Ya está en `NoWarn` |

### 4.2 La decisión: excluir la VM de ConfuserEx del build

La VM de ConfuserEx (`C834A786.cs`) es la implementación del despachador `C834A786._3CB74B1B(args, magicNumber)` que vimos en todos los handlers de MainWindow. Sin esa VM, **los handlers no hacen nada** (lanzan `NotImplementedException` implícito), pero el resto del launcher compila.

Si querés que el launcher **compile y abra la GUI** (aunque los botones no respondan funcionalmente), excluí la VM del build. Si querés intentar reconstruir la VM completa, saltá a la Parte 5.

**Para excluir `C834A786.cs` del build, editá `src/iRemovalProWPF/iRemovalProWPF.csproj`** y agregá este `<ItemGroup>` antes de `</Project>`:

```xml
  <!-- Excluir la VM de ConfuserEx — tiene ~2200 errores de referencias internas rotas -->
  <ItemGroup>
    <Compile Remove="C834A786.cs" />
    <None Include="C834A786.cs" />
  </ItemGroup>
```

### 4.3 Reconstruir

```powershell
dotnet build src\iRemovalProWPF\iRemovalProWPF.csproj -c Release
```

Ahora el count de errores debería bajar drásticamente. Quedan algunos errores sueltos por tipos obfuscados que faltan. Para esos, agregá estas supresiones al `<NoWarn>` del csproj:

```xml
    <NoWarn>$(NoWarn);CS0052;CS0246;CS0108;CS0114;CS0649;CS0169;CS0165;CA1416</NoWarn>
    <TreatWarningsAsErrors>false</TreatWarningsAsErrors>
```

Y volvé a compilar:

```powershell
dotnet build src\iRemovalProWPF\iRemovalProWPF.csproj -c Release
```

### 4.4 Si quedan errores sueltos

Cada error `CS0246: type 'X' could not be found` es un tipo que la VM referenciaba y que se fue al excluir `C834A786.cs`. Tienen forma `A0392D12E`, `C8BFB49E`, etc. Para esos:

1. Buscá el archivo que usa el tipo: `Ctrl+Shift+F` en VS Code y buscá el nombre del tipo.
2. Si el archivo es `MainWindow.cs`, `App.cs`, `Library.cs` — no los toques, son los que querés conservar.
3. Si el archivo es otro `.cs` obfuscado (como `5C946296.cs`), excluirlo también del build:
   ```xml
   <Compile Remove="5C946296.cs" />
   <None Include="5C946296.cs" />
   ```
4. Repetí hasta que compile.

### 4.5 Salida del build exitoso

```
  iRemovalProWPF -> ...\bin\Release\net8.0-windows\iRemovalProWPF.exe

Build succeeded.
```

Output: `src\iRemovalProWPF\bin\Release\net8.0-windows\iRemovalProWPF.exe`

---

## PARTE 5 — Correr el launcher compilado

### 5.1 Ejecutar desde terminal

```powershell
.\src\iRemovalProWPF\bin\Release\net8.0-windows\iRemovalProWPF.exe
```

O desde VS Code con F5 (ver Parte 7).

### 5.2 Qué deberías ver

La ventana WPF debería abrirse con:
- Un botón **"checkrain"** (arriba a la izquierda)
- Dos botones más sin etiqueta (los `Button_Click` y `Button_Click_1`)
- Una imagen de logo (arriba)
- Una imagen de "plug your device" (medio)
- Una imagen de iPhone (medio)
- Una barra de progreso
- Un botón **"activate"** (centro)
- Etiquetas: **Model**, **iOS**, **SN** (serial number), **service**, **IMEI**
- Una imagen de código QR
- Un texto "scan" 
- Un botón **"erase"** (abajo)

### 5.3 Limitaciones esperadas

- **Los botones no van a hacer nada funcional** porque la VM de ConfuserEx quedó excluida y el engine skeleton tiene `throw new NotImplementedException()` en todos los métodos.
- **La ventana va a abrir, pero el único flujo que funciona es**:
  - El handler `Button_Click` corre (llama a `Library.Action(9)`) → va a tirar `NotImplementedException` → la app crash.
- Para prevenir el crash, podés comentar la línea que invoca a `Action(9)` en `MainWindow.cs` (línea 30):
  ```csharp
  // Library.Action(9);  // temporalmente deshabilitado para que no crashee
  ```

---

## PARTE 6 — (Opcional) Configurar el depurador en VS Code

Para poder hacer F5 / F10 / F11 y depurar línea por línea:

### 6.1 Generar launch.json

1. `Ctrl + Shift + D` para abrir el panel de Run and Debug.
2. Clic en **"create a launch.json file"**.
3. Elegí **.NET** (o ".NET Core").
4. VS Code genera `.vscode/launch.json`. Modificalo así:

```json
{
    "version": "0.2.0",
    "configurations": [
        {
            "name": "iRemovalProWPF (launch)",
            "type": "coreclr",
            "request": "launch",
            "preLaunchTask": "build",
            "program": "${workspaceFolder}/src/iRemovalProWPF/bin/Debug/net8.0-windows/iRemovalProWPF.exe",
            "args": [],
            "cwd": "${workspaceFolder}",
            "console": "internalConsole",
            "stopAtEntry": false
        },
        {
            "name": "iremovalpro engine (test)",
            "type": "coreclr",
            "request": "launch",
            "preLaunchTask": "build-engine",
            "program": "dotnet",
            "args": [
                "${workspaceFolder}/src/iremovalpro/bin/Debug/net8.0/iremovalpro.dll"
            ],
            "cwd": "${workspaceFolder}",
            "console": "internalConsole"
        }
    ]
}
```

### 6.2 Generar tasks.json

En la misma carpeta `.vscode/`, creá `tasks.json`:

```json
{
    "version": "2.0.0",
    "tasks": [
        {
            "label": "build",
            "command": "dotnet",
            "type": "process",
            "args": [
                "build",
                "${workspaceFolder}/src/iRemovalProWPF/iRemovalProWPF.csproj",
                "/property:GenerateFullPaths=true",
                "/consoleloggerparameters:NoSummary"
            ],
            "problemMatcher": "$msCompile"
        },
        {
            "label": "build-engine",
            "command": "dotnet",
            "type": "process",
            "args": [
                "build",
                "${workspaceFolder}/src/iremovalpro/iremovalpro.csproj",
                "/property:GenerateFullPaths=true",
                "/consoleloggerparameters:NoSummary"
            ],
            "problemMatcher": "$msCompile"
        }
    ]
}
```

### 6.3 Depurar

- Abrí `src/iRemovalProWPF/iRemovalProWPF/MainWindow.cs`.
- Poné un breakpoint en la línea 229 (método `Button_Click`).
- Presioná **F5**.
- VS Code compila automáticamente y lanza `iRemovalProWPF.exe`.
- Cuando la ventana se abra, hacé clic en el botón principal → el debugger se detiene en el breakpoint.

---

## PARTE 7 — (Opcional) Abrir el proyecto en Visual Studio 2022

Si tenés VS 2022 Community (gratis) instalado con el workload ".NET desktop development":

1. Doble clic en `iremovalpro-src\iremovalpro.sln`.
2. VS 2022 abre la solución.
3. En el Solution Explorer (derecha), clic derecho en `iRemovalProWPF` → **Set as Startup Project**.
4. Presioná **F5** (debug) o **Ctrl+F5** (release sin debug).
5. Compila y ejecuta la GUI WPF.

VS 2022 tiene el diseñador visual de XAML — útil si querés modificar la UI.

---

## PARTE 8 — Próximos pasos (para que sea funcional)

El proyecto reconstruido **compila y abre la GUI**, pero **no es funcional** porque:

1. **El engine skeleton no implementa los métodos** — son `throw new NotImplementedException()`. Para que funcione, tenés que:
   - Dumpear el IL del engine original (`iremovalpro.dll`) en runtime con ClrMD/x64dbg en una VM Windows.
   - Decompilar ese dump con `ilspycmd -p` para obtener los cuerpos reales.
   - Pegarlos en los stubs correspondientes.

2. **La VM de ConfuserEx (`C834A786.cs`) no reconstruye** los handlers — los métodos como `Button_Click` terminan llamando a `C834A786._3CB74B1B(args, magicNumber)` que es el despachador obfuscado. Sin la VM, los botones no responden.

3. **El launcher no carga `iremovalpro.dll`** automáticamente — cuando compila, no sabe dónde está el engine. Necesitás copiar el `iremovalpro.dll` original (el de 33 MB del repo, **NO** el skeleton que compila tu build) al mismo directorio que el `.exe`:
   ```powershell
   copy "iRemoval PRO.exe" "src\iRemovalProWPF\bin\Release\net8.0-windows\"
   copy "iremovalpro.dll" "src\iRemovalProWPF\bin\Release\net8.0-windows\"
   ```
   Y desde ahí el launcher puede cargar el engine original via P/Invoke.

---

## Resumen — chequeo de salud

| Item | Estado |
|---|---|
| .NET SDK 8.0+ instalado | □ |
| VS Code + C# Dev Kit | □ |
| Repo clonado | □ |
| `dotnet restore` exitoso | □ |
| Engine skeleton compila (0 errores) | ✅ |
| Launcher compila (excluyendo VM ConfuserEx) | 🟡 requiere ajuste manual |
| Launcher abre la GUI | 🟡 depende del paso 4.2 |
| Launcher funcional | ❌ requiere dump dinámico del engine original |

---

## Troubleshooting común

### "dotnet no se reconoce como comando"
Reiniciá la PC después de instalar el SDK. Si persiste, agregá `C:\Program Files\dotnet\` al PATH.

### "Could not load file or assembly 'iRemovalpro' al ejecutar"
Copiá el `iremovalpro.dll` original al directorio del `.exe` (ver Parte 8.3).

### "Unrecognized target framework 'net8.0-windows'"
Actualizá el SDK a 8.0+: <https://dotnet.microsoft.com/download/dotnet/8.0>

### "MSB4019: The imported target 'Microsoft.WinUI.targets' was not found"
Asegurate de que el csproj tenga `<UseWPF>true</UseWPF>` y no `<UseWinUI>`.

### "The type or namespace 'Management' does not exist"
Falta el paquete NuGet. Ejecutá `dotnet restore` y si persiste:
```powershell
dotnet add src\iRemovalProWPF\iRemovalProWPF.csproj package System.Management --version 8.0.0
```

### VS Code no resalta C# / no autocompleta
Verificá que la extensión **C# Dev Kit** esté instalada y habilitada. Reiniciá VS Code.

### La GUI abre pero los botones tiran NotImplementedException
Esperado — ver Parte 5.3. Para silenciar el crash, comentá `Library.Action(9)` en `MainWindow.cs:30`.

# ExceptionReporter.NET 6.0 — change summary

Items marked ↩ directly answer the fork author's points.

## Framework / modernization
- Multi-target **net40 → net48 + net10.0-windows** (WinForms & WPF libraries) ↩ (".NET 4.8 and .NET 10")
- Bumped package **version to 6.0** (breaking change: minimum framework is now 4.8)
- **Dropped DotNetZip**, replaced with built-in **System.IO.Compression** (same `IZipper` contract) ↩ ("removed DotNetZip … replaced with System.IO")
- Guarded **ClickOnce** (`System.Deployment`) with `#if` — falls back to assembly version on modern .NET
- **WMI**: framework reference on net48, `System.Management` NuGet on net10
- Bumped **Simple-MAPI.NET 1.2.0 → 1.2.1** (native .NET asset, no compat shim)
- **Kept translations** (en/ru/pt-BR) and **kept WPF** — deliberately diverging from the fork ↩

## Project format / tooling
- Converted the **WPF library + both demos** from old-style to **SDK-style** projects ↩ ("cleanup and simplification")
- Fixed a **.sln casing bug** (`Net`→`NET`) so the Tests project loads on case-sensitive Linux
- Removed stale `C:\code\...` absolute paths from csproj files

## Clean build (0 warnings / 0 errors on Linux)
- Added `Directory.Build.props` documenting intentional suppressions: **CA1416** (Windows-only library) and **SYSLIB0014** (WebClient kept deliberately)
- Scoped **System.Resources.Extensions** to net48 (in-box on net10)
- Suppressed test-only **NU1701/NU1602** (AutoMoq's Framework-only deps)
- Removed a stale **Moq binding redirect** from Tests `app.config`
- Added **Microsoft.NETFramework.ReferenceAssemblies** so net48 builds off-Windows via CLI

## Tests
- Multi-targeted **net472 → net48 + net10.0-windows**; bumped Test SDK / NUnit adapter
- Dropped the obsolete `DotNetZip` assembly-reference assertion

---

## Reply to fork author

Hi, and thanks for the kind note — it's genuinely what prompted me to modernize the original.

It's now converged on your two main drivers:
- **.NET Framework 4.8 + .NET 10** (multi-targets `net48;net10.0-windows`, up from net40)
- **DotNetZip removed** — replaced with built-in `System.IO.Compression`

I kept translations and WPF, since some users still rely on them.

So the big reasons behind your fork are now addressed upstream if you ever want to come back — and no hard feelings either way. Thanks again for taking the time.

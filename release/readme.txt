This 'release' folder is used by the build, to copy merged assemblies.
The assemblies in here are also required for the Demo Apps (in the solution) to build.

The subject of the merging is "ExceptionReporter.core.dll". It is merged (using ILMerge.exe) with both WinForms and Wpf assemblies, into 2 releaseable assemblies:

1) ExceptionReporter.WinForms.dll
2) ExceptionReporter.Wpf.dll

That is, ExceptionReporter.core.dll is not released separately, because it would force users to include 2 assemblies just to use ExceptionReporter.
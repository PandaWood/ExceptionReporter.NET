<sub><sup>We've just moved over from CodePlex so this is now the official home of the Exception Reporter</sub></sup>


# Exception Reporter
Exception Reporter is a .NET component that shows a dialog with detailed information on an
Exception and the application/system running it

### The nuget package [![NuGet](https://img.shields.io/nuget/v/ExceptionReporter.svg)](https://www.nuget.org/packages/ExceptionReporter/)
```
PM> Install-Package ExceptionReporter
```

### The assemblies
For the un-nugetted, the latest release dll/xml for ExceptionReporter is in the 'dist' folder

## How it Looks

Many interface elements can be configured: whether buttons have images, what text appears in the labels,
font-sizes/colors, which tabs are shown etc

![](images/er-user-input.png)

In this example, there are no images on the buttons,
the window title is customised and an extra Tab (_Contact_) is visible -
see the wiki page on [Configuration Options](https://github.com/PandaWood/Exception-Reporter/wiki/Configuration-Options) for more info

![](images/er-exceptions-tab.png)

## How it works

The Exception Reporter is invoked manually or by setting up it's invocation on an event, where it's basically passed the root Exception -
see [Sample Usage](https://github.com/PandaWood/Exception-Reporter/wiki/Sample-Usage)

The user is shown this dialog which is auto-populated with the exception it was given and certain system details.
A screenshot is also taken (jpeg, multiple screens if present) and you can also add attachments to be sent (via email).

The ultimate goal is the developer receiving a formatted exception report - see
[Creating and Sending a Report](https://github.com/PandaWood/Exception-Reporter/wiki/Creating-and-Sending-a-Report)


#### Some Important Features
(At this time, these are the features that the similar and simpler project [Crash Reporter.NET](https://github.com/ravibpatel/CrashReporter.NET) don't have)

- Support for inner exceptions and passing multiple exceptions (ie a list of exceptions)
- Screenshot is automatically taken and attached (support for multiple monitors) - but is configurable
- Attach additional files to the email - useful for including log/trace files to help with diagnosis
- Report includes system information such as Windows version, CPU, memory and all the assemblies with versions being used by the current executable (see example below)

```
[Assembly Info]
 
mscorlib, Version=2.0.0.0
System.Windows.Forms, Version=2.0.0.0
System, Version=2.0.0.0
ExceptionReporter.WinForms, Version=2.1.0.0
System.Drawing, Version=2.0.0.0
EO.WebBrowser, Version=16.0.91.0
  System.Xml.Linq, Version=4.0.0.0
  Esent.Collections, Version=1.9.3.2
-----------------------------

[System Info]

Operating System
-Microsoft Windows 7 Enterprise
--CodeSet = 1252
--CSDVersion =
--CurrentTimeZone = 600
--FreePhysicalMemory = 1947848
--OSArchitecture = 32-bit
--OSLanguage = 1033
--ServicePackMajorVersion = 0
--ServicePackMinorVersion = 0
--Version = 6.1.7600

[Machine]
-MOSESMACHINE
--Manufacturer = Gigabyte Technology Co., Ltd.
--Model = P35-DS3L
--TotalPhysicalMemory = 3756515328
--UserName = MachineName\GruberMachine
  ```

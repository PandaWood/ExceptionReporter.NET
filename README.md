<sub><sup>We've just moved over from CodePlex so this is now the official home of the Exception Reporter</sub></sup>


# Exception Reporter.NET
Exception Reporter is a .NET component that shows a dialog with detailed information on an
Exception and the application/system running it

### The nuget package
 [![NuGet](https://img.shields.io/nuget/v/ExceptionReporter.svg)](https://www.nuget.org/packages/ExceptionReporter/)
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
the window title is customised and an extra Tab (_Contact_) is visible

![](images/er-exceptions-tab.png)

## How it works

The Exception Reporter is invoked manually or by setting up it's invocation on an event, where it's basically passed the root Exception -
see [Sample Usage](https://github.com/PandaWood/Exception-Reporter/wiki/Sample-Usage)

The user is shown this dialog which is auto-populated with the exception it was given and certain system details.
A screenshot is also taken (jpeg, multiple screens if present) and you can also add attachments to be sent (via email).

The ultimate goal is the developer receiving a formatted exception report - see
[Creating and Sending a Report](https://github.com/PandaWood/Exception-Reporter/wiki/Creating-and-Sending-a-Report)


## Some Important Features
(Note: these seem to be the features that the similar but simpler project [Crash Reporter.NET](https://github.com/ravibpatel/CrashReporter.NET) doesn't have)

- Support for inner exceptions and passing multiple exceptions (ie a list of exceptions)
- Screenshot is automatically taken and attached (support for multiple monitors) - but is configurable
- Attach additional files to the email - useful for including log/trace files to help with diagnosis
- Support for connecting to email client (via MAPI) as well as SMTP - this means that the report body/subject/attachments will connect to Outlook (or the default Email client) and allow the user to send and manage the email themselves (without requiring an SMTP server)
- The Report is plain text and includes exception stack traces and system information such as Windows version, CPU, memory and a list of all the assemblies (with versions) being used by the current executable (see example report below)

```

-----------------------------
[General Info]

Application: ExceptionReporter Demo App
Version:     2.1.2.0
Region:      English (Australia)
Machine:     PANDAMAN
User:        JohnGruber
Date: 30/05/2017
Time: 12:40 AM

User Explanation:

JohnGruber said "I just pressed Connect and this happened"
-----------------------------
 
[Exception Info 1]

Top-level Exception
Type:        System.IO.IOException
Message:     Unable to establish a connection with the Foo bank account service. The error number is #FFF474678.
Source:      WinFormsDemoApp
Stack Trace: at WinFormsDemoApp.DemoAppView.AndAnotherOne() in Z:\MyProjects\ExceptionReporter\src\WinFormsDemoApp\DemoAppView.cs:line 110
    at WinFormsDemoApp.DemoAppView.CallAnotherMethod() in Z:\MyProjects\ExceptionReporter\src\WinFormsDemoApp\DemoAppView.cs:line 101
    at WinFormsDemoApp.DemoAppView.SomeMethod() in Z:\MyProjects\ExceptionReporter\src\WinFormsDemoApp\DemoAppView.cs:line 96
    at WinFormsDemoApp.DemoAppView.ShowExceptionReporter(Boolean useConfig) in Z:\MyProjects\ExceptionReporter\src\WinFormsDemoApp\DemoAppView.cs:line 81

Inner Exception 1
Type:        System.Exception
Message:     This is an Inner Exception message - with a message that is not too small but perhaps it should be smaller

-----------------------------

[Assembly Info] 
mscorlib, Version=2.0.0.0
System.Windows.Forms, Version=2.0.0.0
System, Version=2.0.0.0
ExceptionReporter.WinForms, Version=2.1.2.0
System.Drawing, Version=2.0.0.0
EO.WebBrowser, Version=16.0.91.0
Esent.Collections, Version=1.9.3.2

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
-PANDAMAN
--Manufacturer = Gigabyte Technology Co., Ltd.
--Model = P35-DS3L
--TotalPhysicalMemory = 3756515328
--UserName = MachineName\GruberMachine
  ```

# Exception Reporter.NET

[![AppVeyor Tests](https://img.shields.io/appveyor/ci/gruntjs/grunt/master.svg)](https://ci.appveyor.com/project/pandawood/exceptionreporter-net/build/tests)
&nbsp;[![NuGet Badge](https://buildstats.info/nuget/ExceptionReporter)](https://www.nuget.org/packages/ExceptionReporter/)

```
PM> Install-Package ExceptionReporter
```

## How it Looks

If you choose to show a dialog (reports can also be sent silently) there are 2 *modes* - *Less Detail* and *More Detail*

### **Less Detail** mode
![Compact Mode](images/er2-less-detail.png)

### **More Detail** mode
![More Detail Mode](images/er2-more-detail.png)

#### Interface Configuration Options
In the next screenshot, we have an example of some customization that can be made with *configuration*. 
There are no images on the buttons,
the window title is customised and an extra Tab (_Contact_) is visible

![Customized Example](images/er-customized.png)

The buttons for *More detail* and *Less Detail* allow the user to switch between these modes.

## How it works

The Exception Reporter is invoked manually or by setting up it's invocation on a Windows Error event, where it's basically passed the root Exception -
see [Sample Code Usage](https://github.com/PandaWood/Exception-Reporter/wiki/Sample-Usage)

The report is either sent silently or you can choose to show a dialog which is populated with the exception and certain system details.
A screenshot is also taken (jpeg, multiple screens if present) and you can add file attachments to be sent (if using email as the method of sending).

The ultimate goal is the developer receiving a formatted exception report - see
[Creating and Sending a Report](https://github.com/PandaWood/Exception-Reporter/wiki/Creating-and-Sending-a-Report)


## Some Important Features

- Support for inner exceptions and passing multiple exceptions (ie a list of exceptions)
- Support for generating a report without showing a dialog (sending silently)
- Support for sending a report to a RESTful API (WebService)
- Attach additional files to an email (automatically compressed into a single zip file before being attached) - useful for including any log/config files etc to help with diagnosis
- Support for using the user's installed email client (SimpleMAPI) - as well as an SMTP mail server (or WebService)
- The solution includes a Demo App for testing config and sending reports via Email/WebService
- The solution also includes a .NET Core REST/WebService project to demonstrate processing ExceptionReporter.NET reports (it's also configured to run on F5 along with the WinForms Demo and starts listening for reports immediately)
- The "Report" which is sent is plain text and includes exception stack traces, various related data and important system information (using WMI) such as CPU, memory, Windows versions as well as a list of all the assemblies (with versions) being used by the current executable (see example report below)

```
-----------------------------
[General Info]

Application: ExceptionReporter Demo App
Version:     2.2.1
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
--Manufacturer = Gigabyte Technology Co., Ltd.
--Model = P35-DS3L
--TotalPhysicalMemory = 3756515328
  ```

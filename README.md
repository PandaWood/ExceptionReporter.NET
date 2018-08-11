# Exception Reporter.NET

[![AppVeyor Tests](https://ci.appveyor.com/api/projects/status/e2b3sruf4fpmcohm?svg=true)](https://ci.appveyor.com/project/pandawood/exceptionreporter-net/build/tests)
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

You can also hide the email button and various other options - see the property `Config` on the main `ExceptionReporter` class.

![Customized Example](images/er-customized.png)

The buttons for *More detail* and *Less Detail* allow the user to switch between these modes.

## How it works

The Exception Reporter can be called manually or by setting up a Windows Exception event - 
see [Sample Code Usage](https://github.com/PandaWood/Exception-Reporter/wiki/Sample-Usage)

The report can be sent silently or the dialog (above) shown, which is then populated with the exception and certain system details.
A screenshot can be taken and file attachments added (if using email as the method of sending).

The ultimate goal is the developer receiving a formatted exception report - see
[Creating and Sending a Report](https://github.com/PandaWood/Exception-Reporter/wiki/Creating-and-Sending-a-Report)


## Some Important Features

- Gathers and reports on inner exceptions and accepts multiple exceptions
- Option to send a report silently (and asynchronously) ie without showing a dialog
- Support for sending a report to a RESTful API/WebService (v3.0)
- Easily attach additional files to an email (automatically compressed into a single zip file) - useful for including log/config files etc to help with diagnosis
- Support for using the user's Windows email client (SimpleMAPI) - in addition to using SMTP mail server (and, of course, the WebService option)
- The solution includes a Demo App for testing config and sending reports via Email/WebService
- The solution also includes a .NET Core REST/WebService project to demonstrate the requirements of sending reports to a WebService
- The report that is sent is plain text and includes exception stack traces, various related data such time/version and important system information (using WMI) such as CPU, memory, Windows version - as well as a list of all referenced assemblies (with versions) being used by the current executable

## Sample Report

This is a sample of the plain text report:

```text
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

## Build 
ExceptionReporter has a dependency on the [.NET4 Framework](https://en.wikipedia.org/wiki/.NET_Framework_version_history#.NET_Framework_4) - so can go as low as supporting Windows XP

There is a suite of Unit Tests to support ExceptionReporter using [Moq](https://github.com/Moq/moq4/wiki/Quickstart) and [NUnit](https://nunit.org/) libraries. 
Coverage is not as high as it should be but we're working on that.
The tests run every commit via AppVeyor

[![Build status](https://ci.appveyor.com/api/projects/status/e2b3sruf4fpmcohm?svg=true)](https://ci.appveyor.com/project/PandaWood/exceptionreporter-net)

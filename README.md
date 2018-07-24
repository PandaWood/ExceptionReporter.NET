<sub><sup>We've moved from CodePlex - this is now the official home of Exception Reporter</sub></sup>

# Exception Reporter.NET

[![AppVeyor branch](https://img.shields.io/appveyor/ci/gruntjs/grunt/master.svg)](https://ci.appveyor.com/project/pandawood/exceptionreporter-net/build/tests)

### The nuget package
 [![NuGet](https://img.shields.io/nuget/v/ExceptionReporter.svg)](https://www.nuget.org/packages/ExceptionReporter/)
```
PM> Install-Package ExceptionReporter
```

## How it Looks

If you choose to show a dialog (and you don't have to, there is an API for getting the info and keeping it to yourself) there are 2 *modes* - *less details* and *more details*

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
see [Sample Usage](https://github.com/PandaWood/Exception-Reporter/wiki/Sample-Usage)

The user is shown this dialog which is auto-populated with the exception it was given and certain system details.
A screenshot is also taken (jpeg, multiple screens if present) and you can also add attachments to be sent (via email).

The ultimate goal is the developer receiving a formatted exception report - see
[Creating and Sending a Report](https://github.com/PandaWood/Exception-Reporter/wiki/Creating-and-Sending-a-Report)


## Some Important Features
(Note: most of these seem to be the features that similar projects don't have)

- Support for inner exceptions and passing multiple exceptions (ie a list of exceptions)
- Support for generating a report without showing a dialog (see ExceptionReportGenerator class)
- Screenshot is automatically taken and attached (support for multiple monitors) - but is configurable
- Attach additional files to the email (automatically compressed into a single zip file before being attached) - useful for including any log/config files etc to help with diagnosis
- Support for connecting to email client (MAPI) - as well as an SMTP server. This basically means that the report body/subject/attachments can connect to Outlook (or the default Email client) and allow the user to send and manage the email themselves without requiring an SMTP server
- The solution includes a Demo App for testing config and sending emails etc (with commented-out variables for testing)
- The Report is plain text and includes exception stack traces and important system information (using WMI) such as CPU, memory, Windows versions as well as a list of all the assemblies (with versions) being used by the current executable (see example report below)

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

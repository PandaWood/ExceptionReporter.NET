<sub><sup>We've just moved over from CodePlex so this is now the official home of the Exception Reporter</sub></sup>


# Exception Reporter
Exception Reporter is a .NET component that shows a dialog with detailed information on an
Exception and the application/system running it

### The nuget package [![NuGet](https://img.shields.io/nuget/v/ExceptionReporter.svg)](https://www.nuget.org/packages/ExceptionReporter/)
```
PM> Install-Package ExceptionReporter
```

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


#### Most Recent Features
- Added Less/More detail button - toggles between showing just the exception message or the dialog
as normal (for users who get confused with too much information)
- Support for multiple exceptions (ie a list of exceptions)
- A  _custom message_ can override the main message (ie Exception.Message)
- Screenshot saved as jpeg format (rather than .bmp) to reduce file size
- Attach any files to the optional email message (just add to the _FilesToAttach_  array with the
full path & those files will be automatically attached to the outgoing email (MAPI or SMTP)

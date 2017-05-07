<sub><sup>We've just moved over from CodePlex so this is now the official home of the Exception Reporter</sub></sup>


# Exception Reporter
Exception Reporter is a .NET component that shows a dialog with detailed information on an Exception and the application/system running it

Most things can be configured: whether buttons have images, what text appears in the labels, font-sizes/colors, which tabs are shown etc

![](images/er-user-input.png)

In this example, there are no images on the buttons, the window title is customised and an extra Tab (_Contact_) is visible - see the wiki page on [Configuration Options](https://github.com/PandaWood/Exception-Reporter/wiki/Configuration-Options) for more info

![](images/er-exceptions-tab.png)

#### Most Recent Features
- Added Less/More detail button - toggles between showing just the exception message or the dialog as normal (for users who get confused with too much information)
- Support for multiple exceptions (ie a list of exceptions)
- A  _custom message_ can override the main message (ie Exception.Message)
- Screenshot saved as jpeg format (rather than .bmp) to reduce file size
- Attach any files to the optional email message (just add to the _FilesToAttach_  array with the full path & those files will be automatically attached to the outgoing email (MAPI or SMTP)

A WPF/XAML version of ExceptionReporter is in progress. Please feel free to download the source and contribute (eg submit a patch)

As of 2017, I must admit, the WPF version is not being worked on, and I don't envision doing it any time soon, so just hoping someone else feels generous enough to commit work on it

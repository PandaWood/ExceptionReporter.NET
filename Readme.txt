Exception Reporter
------------------

ExceptionReporter is in the process of being moved to VS2008 and significantly updated as of 24th July, 2008 - I will be making updates very soon. In the meantime here is some summary of what's in here now.

* Folder Structure

SLSExceptionReporter.dll - this DLL contains the Exception Reporter component for you to reference from your projects.  This file must be distributed with any application that makes use of the Exception Reporter
SLSExceptionReporterDemo.exe  - this is an executable that demonstrates the Exception Reporter
SLSExceptionReporterDemo.exe.config  - this file is used by the Demo, and illustrates the usage of a config file to set Exception Reporter properties
doc  - HTML documentation for the Exception Reporter, refer to Documentation\index.html
src - the source code for the exception reporter and the demo application




** Changes so far for Release 2.0**
Below are removed config items
1) The config items which specificed whether or not buttons were displayed (eg Save/Print/Email/Copy) are removed - the buttons are always shown. Except for 'Printer' which is not decided on yet (perhaps not show at all)
"SLS_ER_PRINT_BUTTON"
"SLS_ER_SAVE_BUTTON"
"SLS_ER_COPY_BUTTON"
"SLS_ER_EMAIL_BUTTON"
"SLS_ER_SERIAL_NUMBER"

2) The serial number config has been  removed. This was a left-over from when this was commercial software
"SLS_ER_SERIAL_NUMBER"

3) Config property names prefixed with "SLS_ER" are now just "ER" (ie "SLS_" is removed)
SLS - was [S]trata[L]ogic[S]oftware - which is no longer relevant, since ExceptionReporter has become Open Source.

4) SLS_ENUMERATE_PRINTERS removed


Contributors:

phillippettit
PandaWood (Peter van der Woude - spurrymoses@gmail.com)
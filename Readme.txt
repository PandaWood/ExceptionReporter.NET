Exception Reporter
------------------

ExceptionReporter is in the process of being moved to VS2008 and significantly updated as of 24th July, 2008 - I will be making updates very soon. In the meantime here is some summary of what's in here now.

* Folder Structure

SLSExceptionReporter.dll - this DLL contains the Exception Reporter component for you to reference from your projects.  This file must be distributed with any application that makes use of the Exception Reporter
SLSExceptionReporterDemo.exe  - this is an executable that demonstrates the Exception Reporter
SLSExceptionReporterDemo.exe.config  - this file is used by the Demo, and illustrates the usage of a config file to set Exception Reporter properties
doc  - HTML documentation for the Exception Reporter, refer to Documentation\index.html
src - the source code for the exception reporter and the demo application

Contributors:
phillippettit
PandaWood (spurrymoses@gmail.com)


** Changes **
The config items which specificed whether or not buttons were displayed (eg Save/Print/Email/Copy) have been removed, the buttons are always shown.
"SLS_ER_PRINT_BUTTON"
"SLS_ER_SAVE_BUTTON"
"SLS_ER_COPY_BUTTON"
"SLS_ER_EMAIL_BUTTON"


Exception Reporter
------------------

** Changes so far for Release 2.0 (1st drop on CodePlex - post SourceForge project) **

[Functionality]

- The enumerating of printers is completely removed (can cause random hanging, is expensive, and probably of little use to 99% of users (?))

- Printing functionality is in limbo (not sure the best way to include it, I suspect it is best to include it somehow)

[CONFIG CHANGES]

NB - The easiest way to deal with config (including these config changes) is to copy/paste from "app.config" in the Demo App (which contains all possible config properties and sensible defaults) and modify for your application.

- SLS_ER -> ER
Config property names prefixed with "SLS_ER" are now just "ER" (ie "SLS_" is removed)
SLS - was [S]trata[L]ogic[S]oftware - which is no longer relevant.

- "Show Button" properties removed
The config items which specificed whether or not buttons were displayed (eg Save/Print/Email/Copy) are removed - the buttons are always shown. Except for 'Printer' which is not decided on yet (perhaps not show at all)

SLS_ER_PRINT_BUTTON
SLS_ER_SAVE_BUTTON
SLS_ER_COPY_BUTTON
SLS_ER_EMAIL_BUTTON

- SLS_ER_SERIAL_NUMBER removed - This was a left-over from when this was commercial software

- SLS_ENUMERATE_PRINTERS removed - printers are never enumerated. It causes some machines to hang and it's difficult to see this being popularly required.

- The 'contact message' config properties have changed (to be more specific/precise)
ER_CONTACT_MESSAGE_1 -> ER_CONTACT_MESSAGE_TOP
ER_CONTACT_MESSAGE_2 -> ER_CONTACT_MESSAGE_BOTTOM

- y/Y/n/N/true/TRUE/false/FALSE -> all of these work as boolean config srings; previously only upper case 'Y'/'N' worked

- ER_MAIL_TYPE -> ER_MAIL_METHOD
- ER_SHOW_SETTINGS -> ER_SHOW_CONFIG
- ER_SHOW_ENVIRONMENT -> ER_SHOW_SYSTEM

[OTHER CHANGES]
...


Contributors:

phillippettit (original SourceForge project author)
PandaWood (Peter van der Woude - spurrymoses@gmail.com)
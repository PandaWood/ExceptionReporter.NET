Exception Reporter
------------------

Version 1

Welcome to Release 1 of the ExceptionReporter project on CodePlex.
The project has changed a bit since it was moved onto CodePlex, an explanation of the changes follows:

[FUNCTIONALITY CHANGES]
- The enumerating of printers is completely removed (can cause random hanging, is expensive, and probably of little most users)
- Printing functionality is removed - for similar reasons to above - printing complicates everything, and I've chosen to remove it

[CONFIG CHANGES]
- I've changed the ExceptionReporter's application config to use it's own config section - see docs (or Demo App) for what to do. 
Basically, copy the sample config and edit as necessary.

[USER INTERFACE CHANGES]
- icons were updated
- minor touch ups with spacing/padding
- the ExceptionReporter dialog is now resizeable (and all the controls inside anchor appropriately)

[OTHER CHANGES]
- Most of the changes are code/design - there are now about 13 code files where there was previously one or two.
	The purpose of the redesign was:
	1 - to remove duplication of code
	2 - to simplify the code (extract classes that were doing too much into several classes - each with only one responsibility)
	3 - to enable unit testing of the code
	4 - to simplify future maintenance of the code

- I've also spent a fair chunk of time updating and improving the documentation by editing images, adding screenshots, 
clarifying the explanations and including text-dumps of the Formatted Exception Report.

[FUTURE MOVEMENTS]
There is a WPF (Windows Presentation Foundation) project in the solution with the bare minimum of code. It's just a skeletal beginning.
This is not part of this release, but hopefully will be ready for the next release, when we've had some time to work on it.


Contributors:
phillippettit (original SourceForge project author)
PandaWood (Peter van der Woude - spurrymoses@gmail.com)
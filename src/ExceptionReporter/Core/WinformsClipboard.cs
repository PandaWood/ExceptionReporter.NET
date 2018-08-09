// MIT License
// Copyright (c) 2008-2018 Peter van der Woude
// https://github.com/PandaWood/ExceptionReporter.NET
//

using System;

namespace ExceptionReporting.Core
{
	internal class WinFormsClipboard
	{
		public static void CopyTo(string text)
		{
			System.Windows.Forms.Clipboard.SetDataObject(text, true);
		}
	}
}
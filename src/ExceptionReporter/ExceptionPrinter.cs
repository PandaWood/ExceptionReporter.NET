using System;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using ExceptionReporting.Views;

namespace ExceptionReporting
{
	/// <summary>
	/// TODO I am putting 'printing' on hold pending investigation into making the code simpler/easier
	/// </summary>
	public class ExceptionPrinter
	{
		private Font _printFont;
		private Font _boldFont;
		private int _drawWidth;
		private int _drawHeight;
		private int _pageCount;
		private int _charactersPerLine;
		private int _linesPerPage;
		private StringBuilder _printString;

		private string _applicationName;	// TODO set
		private DateTime _exceptionDate;
			
		/// <summary>
		/// I have my doubts that all of this code will be necessary with .NET2 printing libraries?
		/// </summary>
		private void PrintPage(PrintPageEventArgs e, string _exceptionString, TextReader _stringReader)
		{
			int leftMargin = e.MarginBounds.Left;
			int rightMargin = e.MarginBounds.Right;
			int topMargin = e.MarginBounds.Top;
			int intCount = 0;

			_pageCount++;
			if (_pageCount == 1)
			{
				_printFont = new Font("Courier New", 12);
				_boldFont = new Font("Courier New", 12, FontStyle.Bold);
			}

			SizeF fSize = e.Graphics.MeasureString("WWWWWWWWWWWWWWWWWWWWWWWWWWWWWW", _printFont);
			float fltFontWidth = fSize.Width / 30;

			if (_pageCount == 1)
			{
				// setup for first page
				_drawWidth = e.MarginBounds.Size.Width; //- (e.MarginBounds.Left + e.MarginBounds.Right);
				_drawHeight = e.MarginBounds.Size.Height; //- (e.MarginBounds.Top+ e.MarginBounds.Bottom);

				_charactersPerLine = (int)(_drawWidth / fltFontWidth); //fSize.ToSize().Width;
				_linesPerPage = (int)(_drawHeight / _printFont.GetHeight());


				_printString = new StringBuilder();
				var swPrint = new StringWriter(_printString);
				var srException = new StringReader(_exceptionString);
				WrapText(srException, swPrint, _charactersPerLine);
				_stringReader = new StringReader(_printString.ToString());
			}
			// draw the border
			var rect = new Rectangle(leftMargin, topMargin, _drawWidth, _drawHeight);
			e.Graphics.DrawRectangle(Pens.Black, rect);

			//draw the header
			string strLine = "Error Report: " + _applicationName;
			e.Graphics.DrawString(strLine, _boldFont, Brushes.Black, leftMargin, topMargin + ((intCount) * _printFont.GetHeight()));
			intCount++;
			strLine = "Date/Time:    " + _exceptionDate.Date.ToShortDateString() + " " + _exceptionDate.Date.ToShortTimeString();
			e.Graphics.DrawString(strLine, _boldFont, Brushes.Black, leftMargin, topMargin + ((intCount) * _printFont.GetHeight()));
			intCount++;
			e.Graphics.DrawLine(Pens.Black, leftMargin, topMargin + ((intCount) * _printFont.GetHeight()), rightMargin,
								topMargin + ((intCount) * _printFont.GetHeight()));
			intCount++; // leave a space from header


			// draw the footer
			strLine = "Page: " + _pageCount;
			e.Graphics.DrawLine(Pens.Black, leftMargin, topMargin + ((_linesPerPage - 2) * _printFont.GetHeight()), rightMargin,
								topMargin + ((_linesPerPage - 2) * _printFont.GetHeight()));
			e.Graphics.DrawString(strLine, _boldFont, Brushes.Black, leftMargin,
								  topMargin + ((_linesPerPage - 1) * _printFont.GetHeight()));


			//loop for the number of lines a page
			while (intCount <= (_linesPerPage - 3)) // - 1 because of footer
			{
				Font currentFont = _printFont;
				bool blnSkip = false;
				// read the line
				strLine = _stringReader.ReadLine();
				if (strLine == null)
				{
					intCount = _linesPerPage + 1; //exit the loop
				}
				else
				{
					if (strLine.Length >= 5)
					{
						if (strLine.Substring(1, 4).Equals("----"))
						{
							//draw a seperator line
							e.Graphics.DrawLine(Pens.Black, leftMargin, topMargin + ((intCount) * _printFont.GetHeight()), rightMargin,
												topMargin + ((intCount) * _printFont.GetHeight()));
							blnSkip = true;
						}
					}
					if (!blnSkip)
					{
						// check if the line should be bold
						if (strLine.Equals("General") || strLine.Equals("Exceptions") || strLine.Equals("Explanation") ||
							strLine.Equals("Assemblies") || strLine.Equals("Application Settings") || strLine.Equals("Environment") ||
							strLine.Equals("Contact"))
						{
							currentFont = _boldFont;
						}

						// output the text line
						e.Graphics.DrawString(strLine, currentFont, Brushes.Black, leftMargin,
											  topMargin + ((intCount) * _printFont.GetHeight()));
					}
				}
				intCount++;
			}

			e.HasMorePages = _stringReader.Peek() != -1;
		}

		private static void WrapText(TextReader sr, TextWriter sw, int intMaxLineChars)
		{
			string strSubLine;

			string strLine = sr.ReadLine();
			while (strLine != null)
			{
				// handle blank lines
				if (strLine.Length == 0)
				{
					sw.WriteLine(strLine);
				}

				// handle long lines
				while (strLine.Length > intMaxLineChars)
				{
					strSubLine = strLine.Substring(0, intMaxLineChars);
					int intPos = strSubLine.LastIndexOf(" ");
					if (intPos > intMaxLineChars - 7)
					{
						// ie if space occurs within last set of characters
						// then wrap at the space (not in the middle of a word)
						strSubLine = strSubLine.Substring(0, intPos);
					}
					sw.WriteLine(strSubLine);
					strLine = strLine.Substring(strSubLine.Length);
				}

				// now just add remaining chars if there are any
				if (strLine.Length > 0)
				{
					sw.WriteLine(strLine);
				}

				// get the next line
				strLine = sr.ReadLine();
			}
		}

		//TODO this is not wired up
		private void printDocument1_BeginPrint(object sender, PrintEventArgs e)
		{
			_pageCount = 0;
		}

		public void Print()
		{
			var printSelectView = new PrintExceptionView();
			bool showGeneral = false;
			bool showExceptions = false;
			bool showAssemblies = false;
			bool showSettings = false;
			bool showEnvironment = false;
			bool showContact = false;

			try
			{
				if (
					!printSelectView.selectPrintDetails(ref showGeneral, ref showExceptions, ref showAssemblies, ref showSettings, ref showEnvironment,
					                          ref showContact))
				{
					//user has cancelled print
					return;
				}

				if (showGeneral == false && showExceptions == false && showAssemblies == false && showSettings == false &&
				    showEnvironment == false && showContact == false)
				{
					MessageBox.Show("No items have been selected for print. Printing has been cancelled.", "Printing Cancelled");
					return;
				}

//				BuildExceptionString(showGeneral, showExceptions, showAssemblies, showSettings, showEnvironment, showContact, true);

				PrintEventHandler peHandler = printDocument1_BeginPrint;
//				printDocument1.BeginPrint += peHandler;
			}
			catch (Exception ex)
			{
//				ShowError("There has been a problem preparing to Print", ex);
			}


//			printDialog1.Document = printDocument1;
//			DialogResult dr = printDialog1.ShowDialog(this);
//			if (dr == DialogResult.OK)
//			{
//				try
//				{
//					printDocument1.Print();
//				}
//				catch (Exception ex)
//				{
//					ShowError("There has been a problem printing", ex);
//				}
//			}
		
		}
	}
}
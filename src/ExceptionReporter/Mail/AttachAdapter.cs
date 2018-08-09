// MIT License
// Copyright (c) 2008-2018 Peter van der Woude
// https://github.com/PandaWood/ExceptionReporter.NET
//

namespace ExceptionReporting.Mail
{
	internal interface IAttach
	{
		void Attach(string filename);
	}

	/// <summary>
	/// Provide a plug between incompatible classes that nevertheless need the same "attach" treatment
	/// </summary>
	internal class AttachAdapter : IAttach
	{
		readonly Win32Mapi.SimpleMapi _mapi;
		readonly System.Net.Mail.MailMessage _mailMessage;

		public AttachAdapter(System.Net.Mail.MailMessage mailMessage)
		{
			_mailMessage = mailMessage;
		}

		public AttachAdapter(Win32Mapi.SimpleMapi mapi)
		{
			_mapi = mapi;
		}

		public void Attach(string filename)
		{
			if (_mapi != null) _mapi.Attach(filename);
			if (_mailMessage != null) _mailMessage.Attachments.Add(new System.Net.Mail.Attachment(filename));
		}
	}
}

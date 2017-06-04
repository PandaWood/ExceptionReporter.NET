﻿using System;
using Win32Mapi;
using System.Net.Mail;

namespace ExceptionReporting.Mail
{
	interface IAttach
	{
		void Attach(string filename);
	}

	/// <summary>
	/// Provide a plug between 2 incompatible classes that nevertheless need the same "attach" treatment
	/// </summary>
	class AttachAdapter : IAttach
	{
		readonly Mapi _mapi;
		readonly MailMessage _mailMessage;

		public AttachAdapter(MailMessage mailMessage)
		{
			_mailMessage = mailMessage;
		}

		public AttachAdapter(Mapi mapi)
		{
			_mapi = mapi;
		}

		public void Attach(string filename)
		{
			if (_mapi != null) _mapi.Attach(filename);
			if (_mailMessage != null) _mailMessage.Attachments.Add(new Attachment(filename));
		}
	}
}

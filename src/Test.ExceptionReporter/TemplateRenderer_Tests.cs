using System;
using System.Collections.Generic;
using ExceptionReporting.Templates;
using NUnit.Framework;

namespace ExceptionReporting.Tests
{
	public class TemplateRenderer_Tests
	{
		private const string TestApp = "TestApp";
		private const string Version = "4.0.1";
		private const string User = "Bob";
		private const string AssemblyName = "MyAssembly";
		private const string AssemblyVersion = "1.2.3";
		private const string UserExplanation = "crashed suddenly";

		[Test]
		public void Can_Render_Text_Template_Baseline_1_Of_Everything()
		{
			var renderer = new TemplateRenderer(new ReportModel
			{
				Error = new Error
				{
					Exception = new TestException(),
					Explanation = UserExplanation
				},
				App = new App
				{
					Name = TestApp,
					User = User,
					Version = Version,
					AssemblyRefs = new List<AssemblyRef>
					{
						new AssemblyRef
						{
							Name = AssemblyName,
							Version = AssemblyVersion
						}
					}
				}
			});

			var result = renderer.Render();
			
			Assert.That(result, Does.Contain(string.Format("Application: {0}", TestApp)));
			Assert.That(result, Does.Contain(string.Format("Version:     {0}", Version)));
			Assert.That(result, Does.Contain(string.Format("User:        {0}", User)));
			Assert.That(result, Does.Contain(string.Format("Message: {0}", TestException.ErrorMessage)));
			Assert.That(result, Does.Contain(string.Format("User Explanation: {0}", UserExplanation)));
			Assert.That(result, Does.Contain(string.Format("{0}, Version={1}", AssemblyName, AssemblyVersion)));
			Assert.That(result, Does.Contain(string.Format("Date: {0}", DateTime.Now.ToShortDateString())));
			Assert.That(result, Does.Contain(string.Format("Time: {0}", DateTime.Now.ToShortTimeString())));
		}
		
		[Test]
		public void Can_Render_Text_Template_Without_Some_Sections()
		{
			var renderer = new TemplateRenderer(new ReportModel
			{
				Error = new Error
				{
					Exception = new TestException()
				},
				App = new App
				{
					Name = TestApp,
					Version = Version
				}
			});

			var result = renderer.Render();
			
			Assert.That(result, Does.Contain(string.Format("Application: {0}", TestApp)));
			Assert.That(result, Does.Contain(string.Format("Version:     {0}", Version)));
			Assert.That(result, Does.Not.Contain("User Explanation:"));		// whole section not shown  
			Assert.That(result, Does.Not.Contain("User:"));								// whole section not shown  
		}

		[Test]
		public void Can_Render_Markdown_Template()
		{
			var renderer = new TemplateRenderer(new ReportModel
			{
				Error = new Error
				{
					Exception = new TestException()
				},
				App = new App
				{
					Name = TestApp,
					Version = Version
				}
			});

			var result = renderer.Render(TemplateFormat.Markdown);
			
			Assert.That(result, Does.Contain("#Exception Report"));
			Assert.That(result, Does.Contain(string.Format("**Application**: {0}", TestApp)));
			Assert.That(result, Does.Contain(string.Format("**Version**:     {0}", Version)));
		}
		
		[Test]
		public void Can_Render_Html_Template()
		{
			var renderer = new TemplateRenderer(new ReportModel
			{
				Error = new Error
				{
					Exception = new TestException()
				},
				App = new App
				{
					Name = TestApp,
					Version = Version,
					AssemblyRefs = new List<AssemblyRef>
					{
						new AssemblyRef
						{
							Name = AssemblyName,
							Version = AssemblyVersion
						}
					}
				}
			});

			var result = renderer.Render(TemplateFormat.Html);
			
			Assert.That(result, Does.Contain("<title>Exception Report</title>"));
			Assert.That(result, Does.Contain(string.Format("<input id=\"app-name\" readonly value=\"{0}\">", TestApp)));
			Assert.That(result, Does.Contain(string.Format("<li> {0}, Version={1} </li>", AssemblyName, AssemblyVersion)));
		}
	}
}
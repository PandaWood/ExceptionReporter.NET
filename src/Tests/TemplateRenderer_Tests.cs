using System;
using System.Collections.Generic;
using ExceptionReporting;
using ExceptionReporting.Templates;
using NUnit.Framework;

namespace Tests.ExceptionReporting
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

			var result = renderer.RenderPreset();
			
			Assert.That(result, Does.Contain($"Application: {TestApp}"));
			Assert.That(result, Does.Contain($"Version:     {Version}"));
			Assert.That(result, Does.Contain($"User:        {User}"));
			Assert.That(result, Does.Contain($"Message: {TestException.ErrorMessage}"));
			Assert.That(result, Does.Contain($"User Explanation: {UserExplanation}"));
			Assert.That(result, Does.Contain($"{AssemblyName}, Version={AssemblyVersion}"));
			Assert.That(result, Does.Contain($"Date: {DateTime.Now.ToShortDateString()}"));
			Assert.That(result, Does.Contain($"Time: {DateTime.Now.ToShortTimeString()}"));
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

			var result = renderer.RenderPreset();
			
			Assert.That(result, Does.Contain($"Application: {TestApp}"));
			Assert.That(result, Does.Contain($"Version:     {Version}"));
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

			var result = renderer.RenderPreset(TemplateFormat.Markdown);
			
			Assert.That(result, Does.Contain("# Exception Report"));
			Assert.That(result, Does.Contain($"**Application**: {TestApp}"));
			Assert.That(result, Does.Contain($"**Version**:     {Version}"));
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
					Title="Error Report",
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

			var result = renderer.RenderPreset(TemplateFormat.Html);
			
			Assert.That(result, Does.Contain("<title> Error Report </title>"));
			Assert.That(result, Does.Contain("<td> Application: </td>"));
			Assert.That(result, Does.Contain($"<td> {TestApp} </td>"));
		}
		
		[Test]
		public void Can_Render_Custom_Template()
		{
			var renderer = new TemplateRenderer(new ReportModel
			{
				Error = new Error
				{
					Exception = new TestException()
				},
				App = new App
				{
					Name = "Appy",
					Version = "v1.0"
				}
			});

			var result = renderer.RenderCustom(@"
Custom App: {{App.Name}}
Custom Version: {{App.Version}}
");
			Assert.That(result, Does.Contain($"Custom App: {"Appy"}"));
			Assert.That(result, Does.Contain($"Custom Version: {"v1.0"}"));
		}
	}
}
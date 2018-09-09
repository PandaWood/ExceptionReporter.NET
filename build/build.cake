#tool "nuget:?package=NUnit.ConsoleRunner"

var target = Argument("target", "Default");

Task("Default")
  .IsDependentOn("Tests");

Task("Build")
  .Does(() =>
{
  MSBuild("../ExceptionReporter.NET-sdk.sln");
});

Task("Tests")
	.IsDependentOn("Build")
	.Does(() =>
{
	NUnit3("../src/Tests/bin/Debug/Tests.*.dll", new NUnit3Settings {
		Labels = NUnit3Labels.All
	});
});

RunTarget(target);

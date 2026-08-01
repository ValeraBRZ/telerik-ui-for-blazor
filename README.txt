To open this solution, use Visual Studio 2022
	https://visualstudio.microsoft.com/vs/

To run the demos solution, you need to have the Telerik private NuGet feed set up:
	https://docs.telerik.com/blazor-ui/installation/nuget

Alternatively, set up a local feed so the Telerik references can be resolved:
	https://docs.telerik.com/blazor-ui/installation/zip#set-up-a-local-nuget-feed-in-visual-studio

If you are running from the CLI, make sure to also include the base path when opening the browser.
The output of dotnet run would state something like:
	Now listening on: https://localhost:5001
and you have to open the following in the browser:
	https://localhost:5001/blazor-ui
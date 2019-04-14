# Razor Mailer

A standalone console app for parsing and sending email templates using

- [Razor SDK](https://docs.microsoft.com/en-us/aspnet/core/razor-pages/sdk?view=aspnetcore-2.2)
- [SendGrid Azure WebJobs Extension](https://www.nuget.org/packages/Microsoft.Azure.WebJobs.Extensions.SendGrid)

## Running Locally

### Configure User Secrets

run the following commands from the solution root folder to add the API key to user secrets

	.\> cd .\RazorMailer.WebJob
	.\RazorMailer.WebJob> dotnet user-secrets set "AzureWebJobsSendGridApiKeyName" "Your Api Key Name"
	.\RazorMailer.WebJob> dotnet user-secrets set "AzureWebJobsSendGridApiKey" "SG._your_api_key_here"

### Configure Storage account

Either run the [Azure storage emulator](https://docs.microsoft.com/en-us/azure/storage/common/storage-use-emulator) or configure your own storage account

	.\RazorMailer.WebJob> dotnet user-secrets set "ConnectionStrings:AzureWebJobsStorage" "<insert connnectionstring here>"
# Deployment Guide[](https://msteams-captain.visualstudio.com/xGrowth%20App%20Templates/_git/msteams-app-scrum?path=%2FWiki%2FDeploymentGuide.md&_a=preview&anchor=deployment-guide)

**Prerequisites**

To begin, you will need:

-   An Azure subscription where you can create the following kinds of resources:
     -  App service
     -  App service plan
     -  Bot channels registration
    -   Azure storage account
    -   Application Insights
    -   A channel in Microsoft Teams with group of members. (You can add and remove members later!)
    -   A copy of the Scrum Status app GitHub  [repo](https://github.com/OfficeDev/microsoft-teams-app-scrumstatus)
    
**Step 1: Register Azure AD applications**

Register one Azure AD applications in your tenant's directory

1.  Log in to the Azure Portal for your subscription, and go to the "App registrations" blade  [here](https://portal.azure.com/#blade/Microsoft_AAD_IAM/ActiveDirectoryMenuBlade/RegisteredApps)  .
    
2.  Click on "New registration", and create an Azure AD application.
    
    -   **Name**: The name of your Teams app - if you are following the template for a default deployment, we recommend "Scrum Status Bot".
    -   **Supported account types**: Select "Accounts in any organizational directory (Any Azure AD directory - Multitenant)"
    -   Leave the "Redirect URI" field blank.

    ![Azure AD App registration](https://github.com/OfficeDev/microsoft-teams-app-scrumstatus/wiki/images/multitenant_app_creation.PNG)

3.  Click on the "Register" button.
4.  When the app is registered, you'll be taken to the app's "Overview" page. Copy the  **Application (client) ID**; we will need it later. Verify that the "Supported account types" is set to  **Multiple organizations**.

    ![Azure AD Overview page](https://github.com/OfficeDev/microsoft-teams-app-scrumstatus/wiki/images/azure-config-app-step3.PNG)

5.  On the side rail in the Manage section, navigate to the "Certificates & secrets" section. In the Client secrets section, click on "+ New client secret". Add a description (Name of the secret) for the secret and select an expiry time (As per the requirement). Click "Add".

    ![Azure AD Overview page](https://github.com/OfficeDev/microsoft-teams-app-scrumstatus/wiki/images/multitenant_app_secret.PNG)

6.  Once the client secret is created, copy its  **Value**; we will need it later.

    At this point you have 2 unique values:

    -   Application (client) ID for the bot
    -   Client secret for the bot
    -   Directory (tenant) ID
We recommend that you copy these values into a text file, using an application like Notepad. We will need these values later.

7. Go to API permissions, the below Application permissions needs to be granted:

    - Files.Read.All

    - Files.ReadWrite.All

    - Sites.Read.All

    - Sites.ReadWrite.All

**Step 2: Deploy to your Azure subscription**

1. Disconnect the scrum status v1 app service from the corresponding repository. For this, follow below steps:
    - Go to app service -> Deployment centre -> Disconnect (from the upper menu)

2.  Click on the "Deploy to Azure" button below.

[![Deploy to Azure](https://github.com/OfficeDev/microsoft-teams-app-scrumstatus/wiki/images/AzureDeployButton.png)] <<Link for deploy button>>

3.  When prompted, log in to your Azure subscription.
4.  Azure will create a "Custom deployment" based on the ARM template and ask you to fill in the template parameters.
5.  Select a subscription and resource group.

    -   We recommend creating a new resource group.
    
    -   The resource group location MUST be in a datacenter that supports: Application Insights; Storage Account. For an up-to-date list, click  [here](https://azure.microsoft.com/en-us/global-infrastructure/services/?products=logic-apps%2Ccognitive-services%2Csearch%2Cmonitor)  , and select a region where the following services are available:
    
    -   Application Insights
    
    -   Storage Account
    

6.  Enter a "Base Resource Name", which the template uses to generate names for the other resources.

    -   The app service names [Base Resource Name] should be given same as for scrum status v1 resources.
    -   Remember the base resource name that you selected. We will need it later.


7.  Fill in the various IDs in the template:
    
    - **Bot Client ID**: This should be given same as that of scrum status v1.
    
    - **Bot Client Secret**: This should be given same as that of scrum status v1.
    
    - **Tenant Id**: The tenant ID of Bot

    - **Storage account**: This should be same as the storage name of scrum status v1.
    

Make sure that the values are copied as-is, with no extra spaces. The template checks that GUIDs are exactly 36 characters.

8.  If you wish to change the app name, description, and icon from the defaults, modify the corresponding template parameters.
9.  Agree to the Azure terms and conditions by clicking on the check box "I agree to the terms and conditions stated above" located at the bottom of the page.
10.  Click on "Purchase" to start the deployment.
11.  Wait for the deployment to finish. You can check the progress of the deployment from the "Notifications" pane of the Azure Portal. It can take more than 10 minutes for the deployment to finish.
12.  Once the deployment has finished, you would be directed to a page that has the following fields:
        -   BotId: This is the Microsoft Application ID for the Scrum Status Bot.
        -   AppDomain: This is the base domain for the Scrum Status Bot.

**Step 3: Create the Teams app packages**

Create Teams app package: to install in channel.

1.  Open the Manifest\manifest.json file in a text editor.
2.  Replace the application id with the scrum status v1 manifest application id.
3.  Give the version as 2.0.0 .
4.  Change the placeholder fields in the manifest to values appropriate for your organization.

    -   [developer.name](http://developer.name/)  ([What's this?](https://docs.microsoft.com/en-us/microsoftteams/platform/resources/schema/manifest-schema#developer)  )
    -   developer.websiteUrl
    -   developer.privacyUrl
    -   developer.termsOfUseUrl

5.  Change the <botId> placeholder to your Azure AD application's ID from above. This is the same GUID that you entered in the template under "Bot Client ID".
    
6.  In the "validDomains" section, replace the <appDomain> with your Bot App Service's domain. This will be [BaseResourceName].azurewebsites.net. For example if you chose "scrumstatusbot" as the base name, change the placeholder to  [scrumstatusbot.azurewebsites.net](http://scrumstatusbot.azurewebsites.net/)  .
    
    ![Manifest](/wiki/images/Manifest.png)

7.  Create a ZIP package with the manifest.json, color.png and outline.png. The two image files are the icons for your app in Teams.
    

    -   Name this package ScrumStatus.zip.
    -   Make sure that the 3 files are the  _top level_  of the ZIP package, with no nested folders.

**Step 4: Run the apps in Microsoft Teams**

1.  If your tenant has sideloading apps enabled, you can install your app by following the instructions  [here](https://docs.microsoft.com/en-us/microsoftteams/platform/concepts/apps/apps-upload#load-your-package-into-teams)
2.  You can also upload it to your tenant's app catalog, so that it can be available for everyone in your tenant to install. See  [here](https://docs.microsoft.com/en-us/microsoftteams/tenant-apps-catalog-teams)
    - Click on the menu section of the installed scrum status v1 app in the  app catalog and click on update.
    - Now upload the scrum status v2 manifest.
3.  Install the scrum status bot (the ScrumStatusBot.zip package) to your channel.
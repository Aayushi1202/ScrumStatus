# General template issues

**Generic possible issues**

There are certain issues that can arise that are common to many of the app templates. Please check [here](https://github.com/OfficeDev/microsoft-teams-stickers-app/wiki/Troubleshooting) for reference to these.

## Problems deploying to Azure

**1. Error when attempting to reuse a Microsoft Azure AD application ID for the bot registration**

Bot is not valid. Errors: MsaAppId is already in use.
Creating the resource of type Microsoft.BotService/botServices failed with status "BadRequest"

This happens when the Microsoft Azure application ID entered during the setup of the deployment has already been used and registered for a bot.

  

**Fix**
Either register a new Microsoft Azure AD application or delete the bot registration that is currently using the attempted Microsoft Azure application ID.


## Problems in bot application

**1. If facing any issues related to bot.**

**Fix**

Please go to app-insights and check for errors.
- Go to [ azure portal](http://portal.azure.com/)
- Go to App-insights related to your app
- Open Logs (Analytics)
- Select Time Range & fire the query from different tables like exceptions, customEvent etc.

**2. If the scrum doesn't start as per the scheduled time.**

**Fix**

Scheduler job runs hourly and schedules scrums for the next 60 mins. It must be possible that you just missed the scheduler run. As a recommendation, please ensure to schedule scrums atleast 2 hours in advance.


# Didn't find your problem here?**

Please, report the issue [here](https://github.com/OfficeDev/microsoft-teams-app-<<To-Do>>/issues/new)
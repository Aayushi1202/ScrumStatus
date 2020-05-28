
# [](https://github.com/OfficeDev/microsoft-teams-scrumstatus/wiki/Telemetry#telemetry)Telemetry

The app logs telemetry to [Azure Application Insights](https://azure.microsoft.com/en-us/services/monitor/). You can go to the respective Application Insights blade of the Azure App Services to view basic telemetry about your services, such as requests, failures, and dependency errors, custom events, traces etc. .

The bot integrates with Application Insights to gather bot activity analytics, as described [here](https://blog.botframework.com/2019/03/21/bot-analytics-behind-the-scenes/).

The Bot logs a few kinds of events:

`Events` logs keeps the track of application events and also logs the user activities like:

- Click throughs on scrums started
- Click throughs on completed scrums
- Avg no. of users in scrums
- Total and Unique Users per month
- Average Response Time


The `Activity` event:

-   Basic activity info: `ActivityId`, `ActivityType`, `Event Name`
-   Basic user info: `From ID`

The `UserActivity` event:

-   Basic activity info: `ActivityId`, `ActivityType`, `Event Name`
-   Context of how it was invoked: `ConversationType`, `ConversationId`

`Trace` logs keeps the track of application events and also logs the user activities. 

`Exceptions` logs keeps the records of exceptions tracked in the application. 
 
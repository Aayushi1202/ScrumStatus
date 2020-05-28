
![Architecture Image](https://github.com/OfficeDev/microsoft-teams-app-scrumstatus/wiki/images/Scrum_Arch_Diagram.png)

**Bot Technical Details**

The bot is built using the [Bot Framework SDK v4 for .NET](https://docs.microsoft.com/en-us/azure/bot-service/bot-service-overview-introduction?view=azure-bot-service-4.0) and [ASP.NET Core 2.1](https://docs.microsoft.com/en-us/aspnet/core/?view=aspnetcore-2.1)

The  **Scrum for Channels Bot**  application has the following main capabilities:

-   The bot provides an easy way for the users to share their daily scrum updates. It provides interface to   
    -   Update scrum - allows an end-user to update scrum details in respective team in channel.
    
    -   Scrum details - will show the daily status for all the team members in that scrum team.
    
    -   Notify end users when scrum is started.
    
    -   End scrum - close the scrum for the day and will disable entering/updating status for anyone.
    
    -   Settings - Within a channel allow sub teams to be formed and schedule scrums for those teams so that they can report their daily status within their group without disturbing the flow of the channel. The Settings screen shows the list of all the scrum teams that have been created against a particular channel and allow participants to be added/removed from team.
    
    - An archival job run monthly to export all data till 1 month prior to current date for all the teams, file will be saved in channel files and once file is created, the old data is deleted from the table storage.

 **Archival job background service design:**
1) Background service will be executed monthly on last day of the month.  
2) Once service is invoked, the app reads all data from storage for the last 30 days and export to excel.
3) Archival steps:  
	- Fetch data from Scrum table storage which is created before the calculated date.  
	- Fetch data from ScrumStatus table storage for each scrum fetched using above step .  
	- Export to excel file stream.  
	- Share in channel documents with specified format.  
	- Delete exported data from storage after exporting successfully.

4) 10K rows constitutes to 1 MB and file less than 4MB can be exported.

**For example**: 
- Lets assume that App service is deployed on May 15th. The app will calculate next run date as May 31st.
- Service will get invoked on May 31st and tt will calculate export date as April 30th.
- It will check if there is any scrum data available to export which is created before April 30th.
- If found, it will export data as per above logic. If not then, it will skip exporting data
- Schedule next run to June 30th. 

**Technical specifications:**

**Microsoft graph APIs**

Application gets the drive details mainly drive id and post the excel to respective channel which requires application permissions as Files.Read.All, Files.ReadWrite.All, Sites.Read.All,Sites.ReadWrite.All .
Drive id obtained from get call api is passed to create excel post api call.
Group id is obtained from the channel link which is stored in the storage as AADGroup id.

|Sr. No.| Use Case | API|
|--|--|--|
|1. | Get drive id of team to export file at this drive in share point| GET [https://graph.microsoft.com/v1.0/groups/{groupId}/drive](https://graph.microsoft.com/v1.0/groups/%7BgroupId%7D/drive "https://graph.microsoft.com/v1.0/groups/%7bgroupid%7d/drive")
|2. | Create xlsx file and upload on sharepoint | POST [https://graph.microsoft.com/v1.0/drives/{driveId}/root:{FilePath}:/content](https://graph.microsoft.com/v1.0/drives/%7BdriveId%7D/root:%7BFilePath%7D:/content "https://graph.microsoft.com/v1.0/drives/%7bdriveid%7d/root:%7bfilepath%7d:/content")

ClosedXml Nuget package - We are using this nuget package to create excel workbook. We create memory stream and write stream to sharepoint.

For nuget referece click [here](https://www.nuget.org/packages/ClosedXML/)


**Bot Commands**

- **Settings**
   - Settings command will open a task module to everyone in the team and shows the list of all the scrum teams that have been created against a particular channel.
   - “+ Add a new scrum”: This button allows a new row to be created at the end of the grid, where you will be able to create a new team.
  
![ScrumSetting](/wiki/images/SettingsScreen.png)

 **Start scrum scenario**  
- Bot auto starts the scrum based on the day and time trigger. Before starting the scrum, bot will validate the scrum member list against the list of members in the team.
- Start scrum card is scheduled as per the start time entered by user. Start time is converted to UTC time and its respective hour is stored.
- Background service is scheduled every hour to get storage details. The scrum card is scheduled for next upcoming hour.
  
  For example, the scrum card is scheduled at 12 pm IST and its respective UTC hour i.e. 6 AM is stored. Since the storage call is scheduled at every hour so at 5 AM system's local time, it fetches all the scrum that are scheduled at UTC hour 6 AM. When the time is elapsed, the scrum cards are sent to respective channels.
- When a scrum is started, a notification will be sent to the user. When a user adds his/her updates to the scrum, the corresponding card for the user will be updated.

![ScrumStarted](/wiki/images/ScrumStatus1.png)

![ScrumStarted](/wiki/images/ScrumStatus2.png)

- Update scrum
  - Update scrum button invokes a task module that renders an adaptive card with 3 input fields for providing scrum updates.

![UpdateStatus](/wiki/images/UpdateStatus.png)

  - Invoke type is ‘task/fetch’.

  - The adaptive card has submit action button which invokes the submit action.

  - Refresh the card when validation fails with error message for mandatory field, does not store the field entries in table storage.


  - Scrum details will open a task module that will show the daily status for all the team members in that scrum team

![ScrumDetails1](/wiki/images/ScrumDetails1.png)

![ScrumDetails1](/wiki/images/ScrumDetails2.png)

- End scrum

  - Clicking this button will close the scrum for the day and will disable entering/updating statuses for anyone.
 
![EndScrum](/wiki/images/EndScrum.png)
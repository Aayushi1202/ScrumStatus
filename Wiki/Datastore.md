# Data stores

The app uses the following data store:

**Azure Storage Account**
- [Table] Stores the information of scrum configured in teams.
- [Table] Stores information of scrum card .
- [Table] Stores updated scrum details.

All these resources are created in your Azure subscription. None are hosted directly by Microsoft.
# [](/wiki/Datastore#storage-account)Storage account
## [](/wiki/Datastore#configurationinfo-table)Scrum Table
The **Scrum** table stores the conversation state like conversation id, members in group chat, scrum activity status, etc. The table has following rows :

 Attribute | Comment |
|--|--|
|PartitionKey|Represents the ThreadConversationId|
|RowKey| Represents combination of ThreadConversationId_TeamId|
|ThreadConversationId|Conversation ID of the team chat that started the scrum|
|ScrumMasterId| Unique ScrumMasterId of the root scrum card|
|ScrumStartActivityId| Activity ID of scrum card|
|CreatedOn| Current DateTime in UTC|
|ScrumStartCardResponseId| Trial card Activity ID of root scrum card |
|ScrumId|Represents the unique id of each scrum|
|TeamId|Unique Id of the configured team in channel|
|ChannelName|Channel Name in which scrum card is posted|
|UserPrincipalNames|UserPrincipalNames of members in a team|
|IsCompleted|Represents status of scrum|
|AADGroupID| Azure Active Directory group Id in which bot is installed|
|MembersActivityIdMap| Deserialized JSON of user mapping corresponding to their activity id. {“guid”:”member AadObjectId”}|

ScrumMaster table
-
The **Scrum master** table stores the conversation state like conversation id, members in group chat, scrum activity status, etc. The table has following rows :

 Attribute | Comment |
|--|--|
|PartitionKey| Represents the Team id.|
|RowKey| Represents the combination of TeamName_ChannelId|
|ScrumMasterID| Unique ScrumMasterId of the root scrum card   |
|ChannelId| Unique Channel Id in which scrum card is posted|
|ChannelName| Channel Name in which scrum card is posted|
|TeamId| Configured Team ID in a channel card|
|StartTime|Time at which scrum is started |
|StartTimeUTCHour|UTC hour of user specific start time|
|TimeZone| represents timezone|
|IsActive|status of scrum|
|UserPrincipalNames|UserPrincipalNames of members in a team|
|CreatedOn|Scrum creation time|
|CreatedBy|Name of the creator|
|TeamName|Name of configured team in channel|
|AADGroupID|Azure active directory group id in which bot is installed|
|ServiceURL|Service URL of activity|

ScrumStatus table
-
The **ScrumStatus** table stores the conversation state like conversation id, members in group chat, scrum activity status, etc. The table has following rows :

 Attribute | Comment |
|--|--|
|PartitionKey| Represents the combination of SummaryCardId_Username|
|RowKey| Represents SummaryCardId|
|YesterdayTaskDescription| Yesterday task Description updated by user.|
|TodayTaskDescription|Today task Description updated by user.|
|BlockerDescription| Blocker description entered by user.|
|CreatedOn| Current DateTime(UTC) at which card is updated|
|SummaryCardId| Update card id |
|MembersActivityIdMap| Deserialized JSON of user mapping corresponding to their activity id. {“Guid”:”activityId”}|
|Username|Name of user who updated the card|
|AadObjectId|AadObjectId of user who updated the card|
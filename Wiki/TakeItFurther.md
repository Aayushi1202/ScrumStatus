## Scenario 1

Scrum retrospective to be carried out using the bot.

**Suggested Solution:** Implement the feature to trigger a retrospective meeting which will be triggered using a command explicitly or initiated automatically on every sprint end and can be made to work in the following manner
- Users will mark the current sprint start and end dates and assign a scrum master through an adaptive card/task module
- Once the sprint end date is elapsed, on the next working day the bot can initiate the retrospective meeting automatically or explicitly through a command
- The scrum master can capture retrospective details using an adaptive card/task module
- The bot will post the retrospective details in the form of an adaptive card in the channel.

**Pros:** 
- Code extensibility

**Cons:** 
- Dev effort costs


## Scenario 2 

Link the bot with DevOps platform like Azure DevOps, GitHub, Jira, etc. 

**Suggested Solution:** Extend the existing implementation of update scrum further by connecting your DevOps platform. Once the scrum is submitted the records will directly be saved into the DevOps scrum board. 

**Pros:** 
- Code extensibility 
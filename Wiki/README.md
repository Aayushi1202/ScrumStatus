# Scrum for Channels Bot App Template
| [Documentation](/wiki/documentation.md) | [Deployment guide](/wiki/DeployementGuide.md) | [Architecture](/wiki/SolutionOverview.md) |
| ---- | ---- | ---- |

Scrum for Channels bot is a simple scrum assistant bot that will enable users to run and schedule stand-up meetings and provide and easy way for the users to share their daily updates. It is designed to work in the channel scope and all the members who have been selected in a scrum team can contribute to the scrum.
The bot works best in typical corporate workforce scenarios, where the team members are not confined to a specific geographic region, span across multiple time zones and are more diverse and remote in nature. Most often than not, in such cases it is difficult for all the members within a team to get together in a meeting room daily and share their updates.

Using the Scrum for Channels bot in Microsoft Teams, users will be able to:
 -  Run scrums in a channel
 -  Schedule a scrum at a given time depending on the timezone 
 -  Select the team members who will be part of the scrum 
 -  Configure multiple scrums to run in different or same channels
 -  Export scrum details for the past 30 days

A typical scrum workflow in the bot will be:
 - The bot will auto start the scrum at the specified time
 - It will respond with an adaptive card in the channel with actionable buttons to share status updates, view details updated by other scrum members and to end the scrum. The card will also display other details like the status of the scrum whether it is Active or Closed, the number of people who have contributed to the scrum and provide actionable insights like the number of people who have marked their status as blocked 
 - Users can choose to share their updates,  view details updated by other team members and end the scrum


## **Legal Notices**

This app template is provided under the [MIT License](https://github.com/OfficeDev/microsoft-teams-apps-<<ToDo>>/blob/master/LICENSE) terms. In addition to these terms, by using this app template you agree to the following:

-	You are responsible for complying with all applicable privacy and security regulations related to use, collection and handling of any personal data by your app.  This includes complying with all internal privacy and security policies of your organization if your app is developed to be sideloaded internally within your organization.

-	Where applicable, you may be responsible for data related incidents or data subject requests for data collected through your app.

-	Any trademarks or registered trademarks of Microsoft in the United States and/or other countries and logos included in this repository are the property of Microsoft, and the license for this project does not grant you rights to use any Microsoft names, logos or trademarks outside of this repository.  Microsoft’s general trademark guidelines can be found [here](https://www.microsoft.com/en-us/legal/intellectualproperty/trademarks/usage/general.aspx).

-	Use of this template does not guarantee acceptance of your app to the Teams app store.  To make this app available in the Teams app store, you will have to comply with the [submission and validation process](https://docs.microsoft.com/en-us/microsoftteams/platform/concepts/deploy-and-publish/appsource/publish), and all associated requirements such as including your own privacy statement and terms of use for your app.

## **Getting** **Started**

Begin with the [Solution overview](/wiki/SolutionOverview.md) to read about what the app does and how it works.

When you're ready to try out Scrum Status Bot, or to use it in your own organization, follow the steps in the [Deployment guide](/wiki/DeployementGuide.md).

## **Feedback**

Thoughts? Questions? Ideas? Share them with us on [Teams UserVoice](https://microsoftteams.uservoice.com/forums/555103-public)!

Please report bugs and other code issues [here](/issues/new).

## **Contributing**

This project welcomes contributions and suggestions. Most contributions require you to agree to a Contributor License Agreement (CLA) declaring that you have the right to, and actually do, grant us the rights to use your contribution. For details, visit [https://cla.microsoft.com](https://cla.microsoft.com/).

When you submit a pull request, a CLA-bot will automatically determine whether you need to provide a CLA and decorate the PR appropriately (e.g., label, comment). Simply follow the instructions provided by the bot. You will only need to do this once across all repos using our CLA.

This project has adopted the [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/). For more information see the [Microsoft Open Source Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq/) or contact [opencode@microsoft.com](mailto:opencode@microsoft.com) with any additional questions or comments.
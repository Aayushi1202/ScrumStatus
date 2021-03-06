﻿// <copyright file="ActivityHelper.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace Microsoft.Teams.Apps.ScrumStatus.Helpers
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Bot.Builder;
    using Microsoft.Bot.Schema;
    using Microsoft.Extensions.Localization;
    using Microsoft.Extensions.Logging;
    using Microsoft.Teams.Apps.ScrumStatus.Cards;
    using Microsoft.Teams.Apps.ScrumStatus.Common;
    using Microsoft.Teams.Apps.ScrumStatus.Models;
    using Newtonsoft.Json;

    /// <summary>
    /// Instance of class that handles Bot activity helper methods.
    /// </summary>
    public class ActivityHelper
    {
        /// <summary>
        /// Instance to send logs to the Application Insights service.
        /// </summary>
        private readonly ILogger<ActivityHelper> logger;

        /// <summary>
        /// The current cultures' string localizer.
        /// </summary>
        private readonly IStringLocalizer<Strings> localizer;

        /// <summary>
        /// Storage helper for working with scrum data in Microsoft Azure Table storage.
        /// </summary>
        private readonly IScrumStorageProvider scrumStorageProvider;

        /// <summary>
        /// Instance of class that is used for scrum helper methods.
        /// </summary>
        private readonly ScrumHelper scrumHelper;

        /// <summary>
        /// Storage helper for working with scrum master data in Microsoft Azure Table storage.
        /// </summary>
        private readonly IScrumMasterStorageProvider scrumMasterStorageProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityHelper"/> class.
        /// </summary>
        /// <param name="logger">Instance to send logs to the Application Insights service.</param>
        /// <param name="localizer">The current cultures' string localizer.</param>
        /// <param name="scrumStorageProvider">Scrum storage provider to maintain data in Microsoft Azure table storage.</param>
        /// <param name="scrumMasterStorageProvider">Scrum master storage provider to maintain data in Microsoft Azure table storage.</param>
        /// <param name="scrumHelper">Instance of class that handles scrum helper methods.</param>
        public ActivityHelper(
            ILogger<ActivityHelper> logger,
            IStringLocalizer<Strings> localizer,
            IScrumStorageProvider scrumStorageProvider,
            IScrumMasterStorageProvider scrumMasterStorageProvider,
            ScrumHelper scrumHelper)
        {
            this.logger = logger;
            this.localizer = localizer;
            this.scrumStorageProvider = scrumStorageProvider;
            this.scrumMasterStorageProvider = scrumMasterStorageProvider;
            this.scrumHelper = scrumHelper;
        }

        /// <summary>
        /// Get activity id which is being used to check if member in scrum exists.
        /// </summary>
        /// <param name="membersId">Members id.</param>
        /// <param name="activityFromId">Activity from id.</param>
        /// <returns>Returns members activity id string.</returns>
        public string GetActivityIdToMatch(string membersId, string activityFromId)
        {
            if (string.IsNullOrEmpty(membersId))
            {
                return null;
            }

            Dictionary<string, string> membersDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(membersId);
            return membersDictionary.TryGetValue(activityFromId, out string activityId) ? activityId : string.Empty;
        }

        /// <summary>
        /// Get end scrum summary card activity.
        /// </summary>
        /// <param name="scrum">Scrum details of the running scrum.</param>
        /// <param name="conversationId">Conversation id for updating the conversation.</param>
        /// <param name="scrumMembers">Members who are part of the scrum.</param>
        /// <param name="turnContext">Context object containing information cached for a single turn of conversation with a user.</param>
        /// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
        /// <returns>Returns end scrum summary card activity to be updated in team.</returns>
        public async Task<IActivity> GetEndScrumSummaryActivityAsync(Scrum scrum, string conversationId, string scrumMembers, ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            var activity = turnContext?.Activity;
            if (scrum == null || scrum.IsCompleted == true)
            {
                await turnContext.SendActivityAsync(string.Format(CultureInfo.CurrentCulture, this.localizer.GetString("ErrorScrumDoesNotExist"), activity.From.Name), cancellationToken: cancellationToken);
                return null;
            }

            var activityId = this.GetActivityIdToMatch(scrum.MembersActivityIdMap, activity.From.Id);
            if (string.IsNullOrEmpty(activityId))
            {
                await turnContext.SendActivityAsync(string.Format(CultureInfo.CurrentCulture, this.localizer.GetString("ErrorUserIsNotPartOfRunningScrumAndTryToEndScrum"), activity.From.Name), cancellationToken: cancellationToken);
                this.logger.LogInformation($"Member who is updating the scrum is not the part of scrum for: {conversationId}");
                return null;
            }

            if (string.IsNullOrEmpty(scrumMembers))
            {
                this.logger.LogInformation("Scrum members detail could not be found");
                await turnContext.SendActivityAsync(this.localizer.GetString("ErrorMessage"), cancellationToken: cancellationToken);
                return null;
            }

            var membersActivityIdMap = JsonConvert.DeserializeObject<Dictionary<string, string>>(scrumMembers);
            scrum.IsCompleted = true;
            scrum.ThreadConversationId = conversationId;
            var savedData = await this.scrumStorageProvider.CreateOrUpdateScrumAsync(scrum);
            if (!savedData)
            {
                this.logger.LogError("Error in saving scrum information in storage.");
                await turnContext.SendActivityAsync(this.localizer.GetString("ErrorSavingScrumData"), cancellationToken: cancellationToken);
                return null;
            }

            var scrumStartCardResponseId = scrum.ScrumStartCardResponseId;
            var scrumMaster = await this.scrumMasterStorageProvider.GetScrumMasterDetailsByScrumMasterIdAsync(scrum.ScrumMasterId);
            var updatedScrumSummary = await this.scrumHelper.GetScrumSummaryAsync(scrum.ScrumMasterId, scrumStartCardResponseId, membersActivityIdMap);
            var scrumStartCard = ScrumCard.GetScrumStartCard(updatedScrumSummary, membersActivityIdMap, scrum.ScrumMasterId, scrum.ScrumStartActivityId, this.localizer, scrumMaster.TimeZone);
            var activitySummary = MessageFactory.Attachment(scrumStartCard);
            activitySummary.Id = scrumStartCardResponseId;
            activitySummary.Conversation = activity.Conversation;
            return activitySummary;
        }
    }
}

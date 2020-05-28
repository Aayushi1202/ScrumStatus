## Assumptions

The estimate below assumes:

-   Scrum Status bot is installed in ~500 channel chats
-   1 scrum is running each day in each channel chat 

## [](/wiki/costestimate#sku-recommendations)SKU recommendations

The recommended SKUs for a production environment are:
-   App Service: Standard (S1)

## [](/wiki/costestimate#estimated-load)Estimated load

**Number of Scrum updates**: 500 channel chats * 1 update/day * 20 users/channel chat (max.) = 10,000 scrum updates every day

**Data storage**: 1 GB max

**Table data operations**:

-   Bot
    -   (number of channel chats * number of start/end scrum * number of days/month)  =  500 * 10 * 31 = 155,000 storage operations per month

## [](/wiki/costestimate#estimated-cost)Estimated cost

**IMPORTANT:**  This is only an estimate, based on the assumptions above. Your actual costs may vary.

Prices were taken from the  [Azure Pricing Overview](https://azure.microsoft.com/en-us/pricing/)  on 16 April 2020, for the West US 2 region.

Use the  [Azure Pricing Calculator](https://azure.com/e/a156b5a82f9c443a8ebcfe9d2cd547b8) to model different service tiers and usage patterns.


|  Resource |  Tier |  Load |  Monthly price |   
|---|---|---|---|
|  Storage account (Table)| Standard_LRS| upto 1GB data, 160,000 operations|  $0.10 |
|  Bot Channels Registration | F0  |  N/A | Free  |
|Application Insights (Bot)||< 5GB data|(free up to 5 GB)|
|App Service Plan|S1|1 month|$73.00 |
|**Total**|||**$73.10**|
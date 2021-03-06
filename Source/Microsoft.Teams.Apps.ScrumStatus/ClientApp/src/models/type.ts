﻿export interface IScrumProps {
    Key: string,
    ScrumMasterId: string,
    TeamId: string,
    StartTime: string,
    TimeZone: string,
    SelectedTimeZone: string,
    TeamName: string,
    IsActive: boolean,
    AADGroupID: string,
    UserPrincipalNames: string,
    ChannelId: string,
    ChannelName: string,
    CreatedOn: string,
    CreatedBy: string,
    SelectedMembers: any[],
    ServiceUrl: string | null,
}

export interface IChannelsInfo {
    ChannelId: string,
    header: string,
}

export interface IUserDetails {
    content: string,
    header: string,
    aadobjectid: string
}

export interface ITeamDetails {
    Channels: IChannelsInfo[],
    TeamMembers: IUserDetails[]
}
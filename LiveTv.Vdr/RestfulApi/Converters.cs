﻿using System;
using System.Collections.Generic;
using LiveTv.Vdr.RestfulApi.Resources;
using MediaBrowser.Controller.LiveTv;
using MediaBrowser.Model.LiveTv;

namespace LiveTv.Vdr.RestfulApi
{
    internal static class Converters
    {
        private static string BaseUri = Plugin.Instance.Configuration.VDR_RestfulApi_BaseUrl;

        internal static ChannelInfo ChannelResourceToChannelInfo(ChannelResource chRes)
        {
            return new ChannelInfo()
            {
                ChannelType = chRes.Is_radio ? ChannelType.Radio : ChannelType.TV,
                HasImage = chRes.Image,
                Id = chRes.Channel_id,
                ImageUrl = chRes.Image ?
                    string.Format("{0}/channels/image/{1}", BaseUri, chRes.Channel_id) :
                    null,
                Name = chRes.Name,
                Number = chRes.Number.ToString(),
            };
        }

        internal static ProgramInfo EventResourceToProgramInfo(EventResource eventRes)
        {            
            return new ProgramInfo()
            {
                ChannelId = eventRes.Channel,
                Id = eventRes.Id.ToString(),
                Name = eventRes.Title,
                Overview = eventRes.Description,
                StartDate = UnixTimeStampToDateTime(eventRes.Start_time).ToUniversalTime(),
                EndDate = UnixTimeStampToDateTime(eventRes.Start_time + eventRes.Duration).ToUniversalTime(),
                
                EpisodeTitle = eventRes.Short_text, //TODO: check if correct data

                IsHD = eventRes.Channel_name.ToLower().Contains("hd"),
                IsNews = eventRes.Contents.FindAll(str => str.ToLower().Contains("news")).Count > 0,
                IsMovie = eventRes.Contents.FindAll(str => str.ToLower().Contains("movie")).Count > 0,
                IsKids = eventRes.Contents.FindAll(str => str.ToLower().Contains("kid")).Count > 0,
                IsSports = eventRes.Contents.FindAll(str => str.ToLower().Contains("sport")).Count > 0,

                // Vdr allows multiple images, emby only one => use first image
                HasImage = eventRes.Images > 0,
                ImageUrl = eventRes.Images > 0 ? 
                    string.Format("{0}/events/image/{1}/1", BaseUri, eventRes.Id) : null
            };
        }

        internal static MyRecordingInfo RecordingResourceToRecordingInfo(RecordingResource recRes)
        {
            var Genre = recRes.Event_short_text;
            // split multiple entrys
            List<string> GenreList= new List<string>(Genre.Split(' '));
            return new MyRecordingInfo()
            {   
                Inode = recRes.Inode,
                Path = recRes.File_name,              
                Id = recRes.File_name,
                ChannelId = recRes.Channel_id,
                Name = recRes.Event_title,
                Genres = GenreList,
                EpisodeTitle = recRes.Event_title,
                Overview = recRes.Event_description,
                StartDate = UnixTimeStampToDateTime(recRes.Event_start_time),
                EndDate = UnixTimeStampToDateTime(recRes.Event_start_time + recRes.Event_duration)
            };
        }

        internal static TimerInfo TimerResourceToTimerInfo(TimerAPI timerRes, EventResource eventInfo)
        {
            var timerStartDate = DateTime.Parse(timerRes.Start_timestamp);
            var timerEndDate = DateTime.Parse(timerRes.Stop_timestamp);
            var eventStartDate = UnixTimeStampToDateTime(eventInfo.Start_time);
            var eventEndDate = UnixTimeStampToDateTime(eventInfo.Start_time + eventInfo.Duration);
            var PrePaddingTime = eventStartDate - timerStartDate;
            var PostPaddingTime = timerEndDate - eventEndDate;
            return new TimerInfo()
            {
                Name = timerRes.Filename,
                ChannelId = timerRes.Channel,
                Id = timerRes.Id,
                Status = CalcStatus(timerRes),
                StartDate = timerStartDate,
                EndDate = timerEndDate,
                Priority = timerRes.Priority,
                PrePaddingSeconds = PrePaddingTime.Hours / 3600 + PrePaddingTime.Minutes * 60 + PrePaddingTime.Seconds ,
                PostPaddingSeconds = PostPaddingTime.Hours / 3600 + PostPaddingTime.Minutes * 60 + PostPaddingTime.Seconds ,
                //Overview //TODO
            };

        }

        private static RecordingStatus CalcStatus(TimerAPI timerRes)
        {
            return RecordingStatus.New; //TODO
        }

        public static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        public static long DateTimeToUnixTimeStamp(DateTime dateTime)
        {
            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime();
            TimeSpan span = (dateTime.ToLocalTime() - epoch);
            return (long)span.TotalSeconds;
        }

        public static long DateTimeToUnixTimeStamp2(DateTime dateTime)
        {
            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime();
            TimeSpan span = (dateTime.ToLocalTime() - DateTime.Now.ToLocalTime());
            return (long)span.TotalSeconds;
        }


    }
}

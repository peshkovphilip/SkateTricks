using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Notifications.Android;
using Unity.Notifications.iOS;
using UnityEngine.iOS;

public static class NotificationManager
{
    private const String AndroidChannelMiss = "AndroidNotificationChannelMiss";
    
    public static void CreateNotification(ENotificationType notificationType)
    {
        #if UNITY_ANDROID
            CreateNotificationAndroid(notificationType);
        #elif UNITY_IOS
            CreateNotificationIOS(notificationType);
        #endif
    }

    private static void CreateNotificationAndroid(ENotificationType notificationType)
    {
        switch (notificationType)
        {
            case ENotificationType.Miss:
                AndroidMiss();
                break;
        }
    }

    private static void AndroidMiss()
    {
        var channel = new AndroidNotificationChannel
        {
            Id = AndroidChannelMiss,
            Name = "Miss",
            EnableVibration = true,
            Importance = Importance.High,
            CanShowBadge = true,
        };
        
        AndroidNotificationCenter.RegisterNotificationChannel(channel);

        var androidNotification = new AndroidNotification
        {
            Color = Color.gray,
            RepeatInterval = TimeSpan.FromSeconds(3600),
            Title = "We miss you..."
        };

        AndroidNotificationCenter.SendNotification(androidNotification, AndroidChannelMiss);
    }

    private static void CreateNotificationIOS(ENotificationType notificationType)
    {
        switch (notificationType)
        {
            case ENotificationType.Miss:
                IOSMiss();
                break;
        }
    }

    private static void IOSMiss()
    {
        var iosNotification = new iOSNotification
        {
            Identifier = "ios_miss",
            Title = "We miss you...",
            Body = "...",
            Data = DateTime.Now.AddDays(1).ToString(format:"d"),
            ForegroundPresentationOption = PresentationOption.Alert | PresentationOption.Badge
        };
        
        iOSNotificationCenter.ScheduleNotification(iosNotification);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Notifications.Android;
using UnityEngine;

public class MobileNotification : MonoBehaviour
{
#if UNITY_ANDROID

    private void Awake()
    {
        //Remove Notification that have already been displayed
        AndroidNotificationCenter.CancelAllDisplayedNotifications();
    }

    private void Start()
    {
        
        
        //Create Notif Channel
        var channel = new AndroidNotificationChannel()
        {
            Id = "channel_id",
            Name = "Notifications Channel",
            Importance = Importance.Default,
            Description = "Reminder notifications",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel);
        
        

       
       
    }

    private void OnApplicationQuit()
    {
        var notification = new AndroidNotification();
        notification.Title = "Come Back!";
        notification.Text = "The Concert is about to begin!!";
        notification.SmallIcon = "icon_s";
        notification.LargeIcon = "icon_l";
        notification.FireTime = System.DateTime.Now.AddSeconds(15);
        
        //Send the Notif
        var id =  AndroidNotificationCenter.SendNotification(notification, "channel_id");

        if (AndroidNotificationCenter.CheckScheduledNotificationStatus(id) == NotificationStatus.Scheduled)
        {
            AndroidNotificationCenter.CancelAllNotifications();
            AndroidNotificationCenter.SendNotification(notification, "channel_id");
        }
    }

#endif
}

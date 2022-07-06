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

    private int x;
    
    void OnApplicationPause(bool pauseStatus)
    {
   
#if UNITY_ANDROID
        if (!pauseStatus)
        {
                   
        }
    
        if (pauseStatus)
        {
            x = UnityEngine.Random.Range(0, 2);

            switch (x)
            {
                case 0:
                    var notification = new AndroidNotification();
                    notification.Title = "Come Back!";
                    notification.Text = "The Concert is about to begin!!";
                    notification.SmallIcon = "icon_s";
                    notification.LargeIcon = "icon_l";
                    notification.FireTime = System.DateTime.Now.AddHours(24);
        
                    //Send the Notif
                    var id =  AndroidNotificationCenter.SendNotification(notification, "channel_id");

                    if (AndroidNotificationCenter.CheckScheduledNotificationStatus(id) == NotificationStatus.Scheduled)
                    {
                        AndroidNotificationCenter.CancelAllNotifications();
                        AndroidNotificationCenter.SendNotification(notification, "channel_id");
                    }

                    break;
                
                case 1:
                    var notification2 = new AndroidNotification();
                    notification2.Title = "Where Did You Go!";
                    notification2.Text = "The Wombats miss you!!";
                    notification2.SmallIcon = "icon_s";
                    notification2.LargeIcon = "icon_l";
                    notification2.FireTime = System.DateTime.Now.AddHours(24);
        
                    //Send the Notif
                    var id2 =  AndroidNotificationCenter.SendNotification(notification2, "channel_id");

                    if (AndroidNotificationCenter.CheckScheduledNotificationStatus(id2) == NotificationStatus.Scheduled)
                    {
                        AndroidNotificationCenter.CancelAllNotifications();
                        AndroidNotificationCenter.SendNotification(notification2, "channel_id");
                    }

                    break;
            }
            
             
            
              
        } 
#endif
        
           
    }

    private void OnApplicationQuit()
    {
       
    }

#endif
}

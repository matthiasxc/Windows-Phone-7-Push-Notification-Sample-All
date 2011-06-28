using System;
using System.Net;
using System.Windows;
using Microsoft.Phone.Notification;
using PushSample7_0.Model;
using System.Collections.Generic;

namespace PushSample7_0.Services
{
    public class PushNotificationService
    {
        
        private HttpNotificationChannel _notificationChannel;
        private static string _pushChannelName = "SampleAppNotification";

        public PushNotificationService()
        {
        }

        public bool IsPushRegistered { get; set; }
        public Uri PushUri { get; set; }
        private string serverUrl = string.Empty;
        private Settings _appSettings = SettingsService.GetSettingsData();
        private Settings _previousSettings = SettingsService.GetImplementedSettings();

        public void GetPushSubscription(Settings _latestSettings)
        {
            _appSettings = _latestSettings;

            try
            {
                //Step 1: See if we already have a valid HttpNotificationChannel 
                _notificationChannel = HttpNotificationChannel.Find(_pushChannelName);
            } catch { }

            // If we have already implemented the channel && if nothing has 
            //      changed in the settings since last time
            if (_notificationChannel != null && DoSettingsMatch(_appSettings, _previousSettings))
            {
                // Step 2: Make sure we got the channel Uri appropriately                
                if (_notificationChannel.ChannelUri != null)
                {
                    // Success! 
                    RaiseGotPushUri(_notificationChannel.ChannelUri);
                }
                else
                {
                    // If we never got the Uri back, unbind and reset everything...
                    this.UnbindAndResetPush();
                    // Step 3: ... and re-register the event handlers
                    _notificationChannel = new HttpNotificationChannel(_pushChannelName);
                    _notificationChannel.ChannelUriUpdated += new EventHandler<NotificationChannelUriEventArgs>(_notificationChannel_ChannelUriUpdated);
                    _notificationChannel.HttpNotificationReceived += new EventHandler<HttpNotificationEventArgs>(_notificationChannel_HttpNotificationReceived);
                    _notificationChannel.ErrorOccurred += new EventHandler<NotificationChannelErrorEventArgs>(_notificationChannel_ErrorOccurred);
                    // Step 4: Ask for a new Uri
                    _notificationChannel.Open();
                    // Step 5: Set the HttpNotificationChannel to handle 
                    //      the appropriate push notifications
                    BindNotifications(_notificationChannel);
                }
            }
else
{
    // Step 3: Register the event handlers so we can handle when we get
    //      the channel information (or errors) back.
    _notificationChannel = new HttpNotificationChannel(_pushChannelName);
    _notificationChannel.ChannelUriUpdated += new EventHandler<NotificationChannelUriEventArgs>(_notificationChannel_ChannelUriUpdated);
    _notificationChannel.HttpNotificationReceived += new EventHandler<HttpNotificationEventArgs>(_notificationChannel_HttpNotificationReceived);
    _notificationChannel.ErrorOccurred += new EventHandler<NotificationChannelErrorEventArgs>(_notificationChannel_ErrorOccurred);

    try
    {
        // Step 4: Ask for a new Uri
        _notificationChannel.Open();
        // Step 5: Set the HttpNotificationChannel to handle 
        //      the appropriate push notifications
        BindNotifications(_notificationChannel);
        // Now that we've implememted the settings, let's save them so we can remember 
        //  them next time.
        SettingsService.SaveImplementedSettings(_appSettings);
        _previousSettings = _appSettings;
    }
    catch (Exception err)
    {
        // Oops! Something went wrong, null out the channel. 
        _notificationChannel = null;
    }
}
        }

        private void BindNotifications(HttpNotificationChannel _thisChannel)
        {
            if((_appSettings.IsUsingLiveTile) && (!_thisChannel.IsShellTileBound))
            {
                System.Collections.ObjectModel.Collection<Uri> permittedImageHosts = new System.Collections.ObjectModel.Collection<Uri>();
                permittedImageHosts.Add(new Uri("http://www.designersilverlight.com"));
                _thisChannel.BindToShellTile(permittedImageHosts);
            }
            else if ((_appSettings.IsUsingSimpleTile) && (!_thisChannel.IsShellTileBound))
            {
                _thisChannel.BindToShellTile();
            }

            if (_appSettings.IsUsingToast && !_thisChannel.IsShellToastBound)
            {
                _thisChannel.BindToShellToast();
            }
        }

        private void UnbindAndResetPush()
        {
            if (_notificationChannel != null)
            {
                _notificationChannel.ChannelUriUpdated -= _notificationChannel_ChannelUriUpdated;
                _notificationChannel.HttpNotificationReceived -= _notificationChannel_HttpNotificationReceived;
                _notificationChannel.ErrorOccurred -= _notificationChannel_ErrorOccurred;

                try
                {
                    if (_notificationChannel.IsShellToastBound)
                        _notificationChannel.UnbindToShellToast();
                    if (_notificationChannel.IsShellTileBound)
                        _notificationChannel.UnbindToShellTile();
                    _notificationChannel.Close();
                    _notificationChannel = null;
                }
                catch
                {
                    _notificationChannel = null;
                    IsPushRegistered = false;
                }
            }
        }

        private bool DoSettingsMatch(Settings _newSettings, Settings _oldSettings)
        {
            if ((_newSettings.IsUsingSimpleTile == _oldSettings.IsUsingSimpleTile) &&
                (_newSettings.IsUsingLiveTile == _oldSettings.IsUsingLiveTile) &&
                (_newSettings.IsUsingToast == _oldSettings.IsUsingToast))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        void _notificationChannel_ErrorOccurred(object sender, NotificationChannelErrorEventArgs e)
        {
            throw new NotImplementedException();
        }

        void _notificationChannel_HttpNotificationReceived(object sender, HttpNotificationEventArgs e)
        {
            throw new NotImplementedException();
        }

        void _notificationChannel_ChannelUriUpdated(object sender, NotificationChannelUriEventArgs e)
        {
            RaiseGotPushUri(e.ChannelUri);
        }
               
        public event EventHandler<GotPushUriEventArgs> GotPushUri;
        private void RaiseGotPushUri(Uri _latestUri)
        {
            if (GotPushUri != null)
            {
                GotPushUri(this, new GotPushUriEventArgs(_latestUri));
            }
        }

    }

    public class GotPushUriEventArgs : EventArgs
    {
        public Uri PushNotificationUri { get; set; }
        public GotPushUriEventArgs(Uri _pushNotificationUri)
        {
            PushNotificationUri = _pushNotificationUri;
        }
    }

    public class PushErrorEventArgs : EventArgs
    {
        public string ErrorMessage { get; set; }
        public PushErrorEventArgs(string _errorMessage)
        {
            ErrorMessage = _errorMessage;
        }
    }
}

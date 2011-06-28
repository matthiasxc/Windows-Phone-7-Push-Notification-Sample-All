using GalaSoft.MvvmLight;
using PushSample7_0.Model;
using PushSample7_0.Services;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.Windows;

namespace PushSample7_0.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm/getstarted
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {

        Settings _appSettings;
        PushNotificationService _pushNotificationService;

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {
                // Code runs "for real"
            }

            _appSettings = SettingsService.GetSettingsData();
            // Set everything
            IsUsingToast = _appSettings.IsUsingToast;
            IsUsingSimpleTile = _appSettings.IsUsingSimpleTile;
            IsUsingLiveTile = _appSettings.IsUsingLiveTile;

            _pushNotificationService = new PushNotificationService();
            _pushNotificationService.GotPushUri += new System.EventHandler<GotPushUriEventArgs>(_pushNotificationService_GotPushUri);
        }

        void _pushNotificationService_GotPushUri(object sender, GotPushUriEventArgs e)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                IsGettingPushUri = false;
                HasNotificationUri = true;
                PushUri = e.PushNotificationUri.AbsoluteUri;
            });
            
        }

        #region IsUsingToast (bool)
        /// <summary>
        /// The <see cref="IsUsingToast" /> property's name.
        /// </summary>
        public const string IsUsingToastPropertyName = "IsUsingToast";

        private bool _isUsingToast = false;

        /// <summary>
        /// Gets the IsUsingToast property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public bool IsUsingToast
        {
            get
            {
                return _isUsingToast;
            }

            set
            {
                if (_isUsingToast == value)
                {
                    return;
                }

                var oldValue = _isUsingToast;
                _isUsingToast = value;

                _appSettings.IsUsingToast = value;
                SettingsService.SaveSettingsData(_appSettings);

                // Update bindings, no broadcast
                RaisePropertyChanged(IsUsingToastPropertyName);
            }
        }
        #endregion

        #region IsUsingSimpleTile (bool)
        /// <summary>
        /// The <see cref="IsUsingSimpleTile" /> property's name.
        /// </summary>
        public const string IsUsingSimpleTilePropertyName = "IsUsingSimpleTile";

        private bool _isUsingSimpleTile = false;

        /// <summary>
        /// Gets the IsUsingSimpleTile property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public bool IsUsingSimpleTile
        {
            get
            {
                return _isUsingSimpleTile;
            }

            set
            {
                if (_isUsingSimpleTile == value)
                {
                    return;
                }

                var oldValue = _isUsingSimpleTile;
                _isUsingSimpleTile = value;

                _appSettings.IsUsingSimpleTile = value;
                SettingsService.SaveSettingsData(_appSettings);

                // Update bindings, no broadcast
                RaisePropertyChanged(IsUsingSimpleTilePropertyName);
            }
        }
        #endregion

        #region IsUsingLiveTile (bool)
        /// <summary>
        /// The <see cref="IsUsingLiveTile" /> property's name.
        /// </summary>
        public const string IsUsingLiveTilePropertyName = "IsUsingLiveTile";

        private bool _isUsingLiveTile = false;

        /// <summary>
        /// Gets the IsUsingLiveTile property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public bool IsUsingLiveTile
        {
            get
            {
                return _isUsingLiveTile;
            }

            set
            {
                if (_isUsingLiveTile == value)
                {
                    return;
                }

                var oldValue = _isUsingLiveTile;
                _isUsingLiveTile = value;

                _appSettings.IsUsingLiveTile = value;
                SettingsService.SaveSettingsData(_appSettings);

                // Update bindings, no broadcast
                RaisePropertyChanged(IsUsingLiveTilePropertyName);
            }
        }
        #endregion

        #region PushUri (string)
        /// <summary>
        /// The <see cref="PushUri" /> property's name.
        /// </summary>
        public const string PushUriPropertyName = "PushUri";

        private string _pushUri = "";

        /// <summary>
        /// Gets the PushUri property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public string PushUri
        {
            get
            {
                return _pushUri;
            }

            set
            {
                if (_pushUri == value)
                {
                    return;
                }

                var oldValue = _pushUri;
                _pushUri = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(PushUriPropertyName);
            }
        }
        #endregion

        #region IsGettingPushUri (bool)
        /// <summary>
        /// The <see cref="IsGettingPushUri" /> property's name.
        /// </summary>
        public const string IsGettingPushUriPropertyName = "IsGettingPushUri";

        private bool _isGettingPushUri = false;

        /// <summary>
        /// Gets the IsGettingPushUri property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public bool IsGettingPushUri
        {
            get
            {
                return _isGettingPushUri;
            }

            set
            {
                if (_isGettingPushUri == value)
                {
                    return;
                }

                var oldValue = _isGettingPushUri;
                _isGettingPushUri = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(IsGettingPushUriPropertyName);
            }
        }
        #endregion

        #region HasNotificationUri (bool)
        /// <summary>
        /// The <see cref="HasNotificationUri" /> property's name.
        /// </summary>
        public const string HasNotificationUriPropertyName = "HasNotificationUri";

        private bool _hasNotificationUri = false;

        /// <summary>
        /// Gets the HasNotificationUri property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public bool HasNotificationUri
        {
            get
            {
                return _hasNotificationUri;
            }

            set
            {
                if (_hasNotificationUri == value)
                {
                    return;
                }

                var oldValue = _hasNotificationUri;
                _hasNotificationUri = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(HasNotificationUriPropertyName);
            }
        }
        #endregion


        #region Commands

        #region GetNotificationUri calling MakePushUriCall
        public ICommand GetNotificationUri
        {
            get
            {
                return new RelayCommand(() =>
                {
                    MakePushUriCall();
                });
            }
        }

        private object MakePushUriCall()
        {
            // Put code here
            IsGettingPushUri = true;
            _pushNotificationService.GetPushSubscription(_appSettings);
            return null;
        }
        #endregion

        #endregion
    }
}
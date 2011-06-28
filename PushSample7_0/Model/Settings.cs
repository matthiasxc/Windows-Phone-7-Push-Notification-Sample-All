using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.ComponentModel;
using GalaSoft.MvvmLight;

namespace PushSample7_0.Model
{
    public class Settings : ViewModelBase
    {
        public Settings()
        {
        }

        #region IsUsingToast (bool)
        /// <summary>
        /// The <see cref="IsUsingToast" /> property's name.
        /// </summary>
        public const string IsUsingToastPropertyName = "IsUsingToast";

        private bool _isUsingToase = false;

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
                return _isUsingToase;
            }

            set
            {
                if (_isUsingToase == value)
                {
                    return;
                }

                var oldValue = _isUsingToase;
                _isUsingToase = value;

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

                // Update bindings, no broadcast
                RaisePropertyChanged(IsUsingLiveTilePropertyName);
            }
        }
        #endregion
    }
}

using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace AssetManagementUWP.Services
{
    internal static class NavigationService
    {
        private static Frame _frame;
        public static Frame Frame
        {
            get
            {
                if (_frame == null)
                {
                    _frame = Window.Current.Content as Frame;
                }
                return _frame;
            }
            set
            {
                _frame = value;
            }
        } 

        public static void Navigate(Type pageType, object parameter = null, NavigationTransitionInfo info = null)
        {
            if (Frame.Content?.GetType() == pageType) return;

            Frame.Navigate(pageType, parameter, info);
        }
    }
}

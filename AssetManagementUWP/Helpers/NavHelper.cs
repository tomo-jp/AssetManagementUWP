using System;
using Windows.UI.Xaml;

namespace AssetManagementUWP.Helpers
{
    public class NavHelper
    {
        public static Type GetNavigateTo(DependencyObject obj)
        {
            return (Type)obj.GetValue(NavigateToProperty);
        }

        public static void SetNavigateTo(DependencyObject obj, Type value)
        {
            obj.SetValue(NavigateToProperty, value);
        }

        // Using a DependencyProperty as the backing store for NavigateTo.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NavigateToProperty =
            DependencyProperty.RegisterAttached("NavigateTo", typeof(Type), typeof(NavHelper), new PropertyMetadata(null));
    }
}

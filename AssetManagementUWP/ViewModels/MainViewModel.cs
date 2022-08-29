using AssetManagementUWP.Helpers;
using AssetManagementUWP.Services;
using AssetManagementUWP.Views;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Linq;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using WinUI = Microsoft.UI.Xaml.Controls;

namespace AssetManagementUWP.ViewModels
{
    internal class MainViewModel
    {
        private WinUI.NavigationView _navigationView;
        private ICommand _itemInvokedCommand;

        public ICommand ItemInvokedCommand => _itemInvokedCommand ?? (_itemInvokedCommand = new RelayCommand<WinUI.NavigationViewItemInvokedEventArgs>(OnItemInvoked));

        private void OnItemInvoked(WinUI.NavigationViewItemInvokedEventArgs args)
        {
            var selectedItem = args.InvokedItemContainer as WinUI.NavigationViewItem;
            var pageType = selectedItem?.GetValue(NavHelper.NavigateToProperty) as Type;
            NavigationService.Navigate(pageType, null, args.RecommendedNavigationTransitionInfo);
        }

        public void Initialize(WinUI.NavigationView navigationView, Frame frame)
        {
            _navigationView = navigationView;
            NavigationService.Frame = frame;
            ////TODO 以下暫定処理。システムが大きくなる時にActivationServiceを実装したら、ここはdefaultでSimpleCalcPageが取れるようにする
            NavigationService.Navigate(typeof(SimpleCalcPage));
            _navigationView.SelectedItem = _navigationView.MenuItems.First();
        }
    }
}

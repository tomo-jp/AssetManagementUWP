using AssetManagementUWP.ViewModels;
using Windows.UI.Xaml.Controls;

namespace AssetManagementUWP.Views
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    internal sealed partial class SimpleCalcPage : Page
    {
        public SimpleCalcViewModel ViewModel { get; } = new SimpleCalcViewModel();

        public SimpleCalcPage()
        {
            this.InitializeComponent();
        }
    }
}

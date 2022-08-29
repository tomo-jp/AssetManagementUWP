using Microsoft.UI.Xaml.Controls;
using System;
using Windows.Globalization.NumberFormatting;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace AssetManagementUWP.Controls
{
    public sealed partial class InputControl : UserControl
    {
        public string ItemText { get; set; }
        public string UnitText { get; set; }

        #region dp
        public bool HasError
        {
            get { return (bool)GetValue(HasErrorProperty); }
            set { SetValue(HasErrorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HasErrorProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HasErrorProperty =
            DependencyProperty.Register("HasError", typeof(bool), typeof(InputControl), new PropertyMetadata(false));
        public int InputValue
        {
            get { return (int)GetValue(InputValueProperty); }
            set { SetValue(InputValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for InputValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InputValueProperty =
            DependencyProperty.Register("InputValue", typeof(int), typeof(InputControl), new PropertyMetadata(0));


        public  NumberBoxFormat Format
        {
            get { return ( NumberBoxFormat)GetValue(FormatProperty); }
            set { SetValue(FormatProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Format.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FormatProperty =
            DependencyProperty.Register("Format", typeof( NumberBoxFormat), typeof(InputControl), new PropertyMetadata(NumberBoxFormat.Year, new PropertyChangedCallback(SetFormat)));



        public NumberBoxSpinButtonPlacementMode SpinButtonPlacementMode
        {
            get { return (NumberBoxSpinButtonPlacementMode)GetValue(SpinButtonPlacementModeProperty); }
            set { SetValue(SpinButtonPlacementModeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SpinButtonPlacementMode.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SpinButtonPlacementModeProperty =
            DependencyProperty.Register("SpinButtonPlacementMode", typeof(NumberBoxSpinButtonPlacementMode), typeof(InputControl), new PropertyMetadata(NumberBoxSpinButtonPlacementMode.Hidden));



        #endregion

        public InputControl()
        {
            this.InitializeComponent();
            (this.Content as FrameworkElement).DataContext = this;
        }

        private static void SetFormat(DependencyObject dp, DependencyPropertyChangedEventArgs args)
        {
            InputControl control = dp as InputControl;
            switch (control.Format)
            {
                case NumberBoxFormat.Percent:
                    //TODO 利回りは小数第二桁まで入力可能にする
                    //control.InputArea.NumberFormatter = CreateDecimalFormatter();
                    control.InputArea.NumberFormatter = CreateIntFormatter();
                    break;
                case NumberBoxFormat.Year:
                    control.InputArea.NumberFormatter = CreateIntFormatter();
                    break;
                case NumberBoxFormat.Currency:
                    control.InputArea.NumberFormatter = CreateCurrencyFormatter();
                    break;
                default: 
                    throw new ArgumentOutOfRangeException(nameof(Format));
            }
        }

        private static INumberFormatter2 CreateDecimalFormatter()
        {
            var rounder = new IncrementNumberRounder();
            rounder.Increment = 0.01;
            rounder.RoundingAlgorithm = RoundingAlgorithm.RoundHalfUp;

            var formatter = new DecimalFormatter();
            formatter.FractionDigits = 2;
            formatter.NumberRounder = rounder;
            return formatter;
        }

        private static INumberFormatter2 CreateIntFormatter()
        {
            var formatter = new DecimalFormatter();
            formatter.FractionDigits = 0;
            return formatter;
        }

        private static INumberFormatter2 CreateCurrencyFormatter()
        {
            //var formatter = new CurrencyFormatter(CurrencyIdentifiers.JPY);
            var formatter = new DecimalFormatter();
            formatter.FractionDigits =  0;
            return formatter;
        }

        public enum NumberBoxFormat
        {
            Percent,
            Year,
            Currency
        }
    }
}

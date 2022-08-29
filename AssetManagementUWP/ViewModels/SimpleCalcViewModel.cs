using AssetManagementUWP.Models;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Windows.UI.Xaml;

namespace AssetManagementUWP.ViewModels
{
    internal class SimpleCalcViewModel : DependencyObject
    {
        private static int _thisYear = DateTime.Now.Year;

        private ICommand _buttonClickCommand;
        public ICommand ButtonClickCommand => _buttonClickCommand ?? (_buttonClickCommand = new RelayCommand(OnButtonClick));

        public ObservableCollection<ResultModel> GridData { get; } = new ObservableCollection<ResultModel>();

        #region dp

        public int StartYear
        {
            get { return (int)GetValue(StartYearProperty); }
            set { SetValue(StartYearProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StartYear.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StartYearProperty =
            DependencyProperty.Register("StartYear", typeof(int), typeof(SimpleCalcViewModel), new PropertyMetadata(_thisYear));

        public int StartAsset
        {
            get { return (int)GetValue(StartAssetProperty); }
            set { SetValue(StartAssetProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StartAsset.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StartAssetProperty =
            DependencyProperty.Register("StartAsset", typeof(int), typeof(SimpleCalcViewModel), new PropertyMetadata(0));

        public int GoalDividend
        {
            get { return (int)GetValue(GoalDividendProperty); }
            set { SetValue(GoalDividendProperty, value); }
        }

        // Using a DependencyProperty as the backing store for GoalDividend.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GoalDividendProperty =
            DependencyProperty.Register("GoalDividend", typeof(int), typeof(SimpleCalcViewModel), new PropertyMetadata(0));

        public int GoalRetireYear
        {
            get { return (int)GetValue(GoalRetireYearProperty); }
            set { SetValue(GoalRetireYearProperty, value); }
        }

        // Using a DependencyProperty as the backing store for GoalRetireYear.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GoalRetireYearProperty =
            DependencyProperty.Register("GoalRetireYear", typeof(int), typeof(SimpleCalcViewModel), new PropertyMetadata(_thisYear));

        public int ExpectedDividendRate
        {
            get { return (int)GetValue(ExpectedDividendRateProperty); }
            set { SetValue(ExpectedDividendRateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ExpectedDividendRate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ExpectedDividendRateProperty =
            DependencyProperty.Register("ExpectedDividendRate", typeof(int), typeof(SimpleCalcViewModel), new PropertyMetadata(0));

        public bool HasErrorGoalRetireYear
        {
            get { return (bool)GetValue(HasErrorGoalRetireYearProperty); }
            set { SetValue(HasErrorGoalRetireYearProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HasErrorGoalRetireYear.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HasErrorGoalRetireYearProperty =
            DependencyProperty.Register("HasErrorGoalRetireYear", typeof(bool), typeof(SimpleCalcViewModel), new PropertyMetadata(false));

        public bool HasErrorGoalDividend
        {
            get { return (bool)GetValue(HasErrorGoalDividendProperty); }
            set { SetValue(HasErrorGoalDividendProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HasErrorGoalDividend.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HasErrorGoalDividendProperty =
            DependencyProperty.Register("HasErrorGoalDividend", typeof(bool), typeof(SimpleCalcViewModel), new PropertyMetadata(false));

        public bool HasErrorExpectedDividendRate
        {
            get { return (bool)GetValue(HasErrorExpectedDividendRateProperty); }
            set { SetValue(HasErrorExpectedDividendRateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HasErrorExpectedDividendRate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HasErrorExpectedDividendRateProperty =
            DependencyProperty.Register("HasErrorExpectedDividendRate", typeof(bool), typeof(SimpleCalcViewModel), new PropertyMetadata(false));

        public bool HasError
        {
            get { return (bool)GetValue(HasErrorProperty); }
            set { SetValue(HasErrorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HasError.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HasErrorProperty =
            DependencyProperty.Register("HasError", typeof(bool), typeof(SimpleCalcViewModel), new PropertyMetadata(false));

        public int ResultValue
        {
            get { return (int)GetValue(ResultValueProperty); }
            set { SetValue(ResultValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ResultValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ResultValueProperty =
            DependencyProperty.Register("ResultValue", typeof(int), typeof(SimpleCalcViewModel), new PropertyMetadata(0));

        public Visibility ResultVisibility
        {
            get { return (Visibility)GetValue(ResultVisibilityProperty); }
            set { SetValue(ResultVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ResultVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ResultVisibilityProperty =
            DependencyProperty.Register("ResultVisibility", typeof(Visibility), typeof(SimpleCalcViewModel), new PropertyMetadata(Visibility.Collapsed));

        #endregion

        private void OnButtonClick()
        {
            var timePeriod = GoalRetireYear - StartYear + 1;
            GridData.Clear();

            HasError = !Validate(timePeriod);
            if (HasError)
            {
                ResultValue = 0;
                ResultVisibility = Visibility.Collapsed;
                return;
            }

            var requiredAmount = GoalDividend * 100 / ExpectedDividendRate - StartAsset;
            ResultValue = requiredAmount <= 0 ? 0 : requiredAmount / timePeriod;
            CreateGridData(timePeriod);
            ResultVisibility = Visibility.Visible;
        }

        private bool Validate(int timePeriod)
        {
            HasErrorGoalRetireYear = timePeriod <= 0;
            HasErrorGoalDividend = GoalDividend <= 0;
            HasErrorExpectedDividendRate = ExpectedDividendRate <= 0;
            return !HasErrorGoalRetireYear && !HasErrorGoalDividend && !HasErrorExpectedDividendRate;
        }

        private void CreateGridData(int timePeriod)
        {
            int currentAsset = StartAsset;
            for(int i = 0; i < timePeriod; i++)
            {
                var year = StartYear + i;
                currentAsset += ResultValue;

                GridData.Add(new ResultModel(year, currentAsset, currentAsset * ExpectedDividendRate / 100));
            }
        }
    }
}

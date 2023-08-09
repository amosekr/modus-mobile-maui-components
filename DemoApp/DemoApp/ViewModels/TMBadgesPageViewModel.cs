﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Trimble.Modus.Components;

namespace DemoApp.Views
{
    public partial class TMBadgesPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private BadgeSize _badgeSize;

        public TMBadgesPageViewModel()
        {
            BadgeSize = BadgeSize.Large;
        }
   
        [RelayCommand]
        private void BadgeSizeChanged(TMRadioButtonEventArgs e)
        {
            switch (e.RadioButtonIndex)
            {
                case 0:
                    BadgeSize = BadgeSize.Small;
                    break;
                case 2:
                    BadgeSize = BadgeSize.Large;
                    break;
                case 1:
                default:
                    BadgeSize = BadgeSize.Medium;
                    break;
            }
        }

    }
}

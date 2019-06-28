using Panacea.Controls;
using Panacea.Modules.AudioManager.Views;
using Panacea.Mvvm;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace Panacea.Modules.AudioManager.ViewModels
{
    [View(typeof(NavigationControl))]
    class NavigationControlviewModel : ViewModelBase
    {
        bool _popOpen;
        public bool PopupOpen
        {
            get => _popOpen;
            set
            {
                _popOpen = value;
                OnPropertyChanged();
            }
        }
        public NavigationControlviewModel()
        {
            ClickCommand = new RelayCommand(args =>
            {
                PopupOpen = !PopupOpen;
            });
        }
        public ICommand ClickCommand { get; }
    }

    public class HeightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (double)value * 3;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class HeightConverter2 : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (double)value * 2;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

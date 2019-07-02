using Panacea.Controls;
using Panacea.Modularity.AudioManager;
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
    class NavigationControlViewModel : ViewModelBase
    {
        public IAudioManager Manager { get; }
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
        public NavigationControlViewModel(IAudioManager manager)
        {
            Manager = manager;
            ClickCommand = new RelayCommand(args =>
            {
                PopupOpen = !PopupOpen;
            },
            args => !PopupOpen);
            VolumeDownCommand = new RelayCommand(args =>
            {

                manager.SpeakersVolume = RoundBy5Down(manager.SpeakersVolume) - 5;
            },
            args => manager.SpeakersVolume > 0);

            VolumeUpCommand = new RelayCommand(args =>
            {
                manager.SpeakersVolume = RoundBy5Up(manager.SpeakersVolume) + 5;
            },
            args => manager.SpeakersVolume < 100);
        }

        int RoundBy5Down(int v)
        {
            return (int)(v / 10 * 10.0 + Math.Ceiling(v % 10 / 5.0) * 5);
        }

        int RoundBy5Up(int v)
        {
            return (int)(v / 10 * 10.0 + Math.Floor(v % 10 / 5.0) * 5);
        }

        public ICommand ClickCommand { get; }

        public ICommand VolumeDownCommand { get; }

        public ICommand VolumeUpCommand { get; }
    }

    public class HeightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (double)value * 3.5;
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
            return (double)value * 1.8;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

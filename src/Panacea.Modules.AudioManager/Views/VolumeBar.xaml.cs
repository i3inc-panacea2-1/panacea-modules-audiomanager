using Panacea.Controls;
using Panacea.Modularity.AudioManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Panacea.Modules.AudioManager.Views
{
    /// <summary>
    /// Interaction logic for VolumeBar.xaml
    /// </summary>
    public partial class VolumeBar : NonFocusableWindow
    {
        DispatcherTimer _timer;
        public VolumeBar()
        {
            InitializeComponent();
          
        }

        public IAudioManager Manager { get; set; }

        public VolumeBar(IAudioManager manager)
        {
            Manager = manager;
            Manager.PropertyChanged += Manager_PropertyChanged;
            _timer = new DispatcherTimer()
            {
                Interval = TimeSpan.FromSeconds(3)
            };
            _timer.Tick += _timer_Tick;
            InitializeComponent();
            var screen = System.Windows.Forms.Screen.PrimaryScreen;
            Width = screen.WorkingArea.Width;
            Height = screen.WorkingArea.Height;
            Top = screen.WorkingArea.Top;
            Left = screen.WorkingArea.Left;
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            Hide();
            _timer.Stop();
        }

        private void Manager_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(IAudioManager.SpeakersVolume))
            {
                _timer.Stop();
                Show();
                _timer.Start();
            }
        }
    }
}

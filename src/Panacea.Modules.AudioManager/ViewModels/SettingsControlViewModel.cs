using Panacea.Modularity.AudioManager;
using Panacea.Modules.AudioManager.Views;
using Panacea.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panacea.Modules.AudioManager.ViewModels
{
    [View(typeof(SettingsControl))]
    class SettingsControlViewModel:ViewModelBase
    {
        public SettingsControlViewModel(IAudioManager manager)
        {
            Manager = manager;
        }

        public IAudioManager Manager { get; private set; }
    }
}

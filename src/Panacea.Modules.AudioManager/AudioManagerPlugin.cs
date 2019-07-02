using Panacea.Core;
using Panacea.Modularity.AudioManager;
using Panacea.Modularity.UiManager;
using Panacea.Modules.AudioManager.ViewModels;
using Panacea.Modules.AudioManager.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panacea.Modules.AudioManager
{
    public class AudioManagerPlugin : IAudioManagerPlugin
    {
        private readonly PanaceaServices _core;
        AudioManagerImpl _manager;

        [PanaceaInject("DefaultSpeakersVolume", "Sets the default percentage for speakers", "DefaultSpeakersVolume=50")]
        protected int DefaultSpeakersVolume { get; set; } = 100;


        protected int defaultMicrophoneVolume = 100;
        protected string mainAudioDevice = null;
        NavigationControlViewModel _navButton;
        VolumeBar _volumeBar;
        public AudioManagerPlugin(PanaceaServices core)
        {
            _core = core;
        }

        public Task BeginInit()
        {
            return Task.CompletedTask;
        }

        public void Dispose()
        {

        }

        public Task EndInit()
        {
            if(_core.TryGetUiManager(out IUiManager ui))
            {
                //ui.AddSettingsControl(new SettingsControlViewModel(GetAudioManager()));
                _navButton = new NavigationControlViewModel(GetAudioManager());
                ui.AddNavigationBarControl(_navButton);
            }
            _volumeBar = new VolumeBar(GetAudioManager());
            return Task.CompletedTask;
        }

        public IAudioManager GetAudioManager()
        {
            return _manager ?? (_manager = new AudioManagerImpl(_core.Logger, mainAudioDevice, DefaultSpeakersVolume, defaultMicrophoneVolume));
        }

        public Task Shutdown()
        {
            if (_navButton != null &&_core.TryGetUiManager(out IUiManager ui))
            {
                ui.RemoveNavigationBarControl(_navButton);
            }
            return Task.CompletedTask;
        }
    }
}

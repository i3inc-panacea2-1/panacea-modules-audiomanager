using Panacea.Core;
using Panacea.Modularity.AudioManager;
using Panacea.Modularity.UiManager;
using Panacea.Modules.AudioManager.ViewModels;
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

        protected int defaultSpeakersVolume = 100;
        protected int defaultMicrophoneVolume = 100;
        protected string mainAudioDevice = null;
        NavigationControlviewModel _navButton;

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
                _navButton = new NavigationControlviewModel();
                ui.AddNavigationBarControl(_navButton);
            }
            return Task.CompletedTask;
        }

        public IAudioManager GetAudioManager()
        {
            return _manager ?? (_manager = new AudioManagerImpl(_core.Logger, mainAudioDevice, defaultSpeakersVolume, defaultMicrophoneVolume));
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

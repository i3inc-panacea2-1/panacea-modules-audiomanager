using NAudio.CoreAudioApi;
using Panacea.Core;
using Panacea.Modularity.AudioManager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Panacea.Modules.AudioManager
{

    class AudioManagerImpl : IAudioManager
    {
        public AudioManagerImpl(ILogger logger, string mainAudioOut, int defaultSpeakersVolume, int defaultMicrophoneVolume)
        {
            _mainAudioOut = mainAudioOut;
            _logger = logger;
            if (Debugger.IsAttached) return;
            try
            {
                var enumerator = new MMDeviceEnumerator();

                var devs = enumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active);
                foreach (var dev in devs)
                    dev.AudioEndpointVolume.MasterVolumeLevelScalar = 1.0f;

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print("Could not enumerate devices due to an excepion: " + ex.Message);
            }


            SpeakersVolume = defaultSpeakersVolume;

            try
            {
                var enumerator = new MMDeviceEnumerator();

                var dev = enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
                SpeakersVolume = (int)(dev.AudioEndpointVolume.MasterVolumeLevelScalar * 100);
                dev.AudioEndpointVolume.Mute = false;

            }
            catch (Exception ex)
            {
                _logger.Error(this, "Could not enumerate devices due to an excepion: " + ex.Message);
            }
            MicrophoneVolume = defaultMicrophoneVolume;
        }


        public void MaxDevicesExceptDefault()
        {
            try
            {
                var enumerator = new MMDeviceEnumerator();

                var devs = enumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active);
                foreach (var dev in devs.Where(d => d.ID != enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia).ID))
                    dev.AudioEndpointVolume.MasterVolumeLevelScalar = 1;

            }
            catch (Exception ex)
            {
                _logger.Error(this, ex.Message);
            }
        }

        int _speakersVolume;
        public int SpeakersVolume
        {
            get { return _speakersVolume; }
            set
            {
                try
                {
                    var device = _mainAudioOut;
                    if (device == "None" || string.IsNullOrEmpty(device)) device = null;

                    var enumerator = new MMDeviceEnumerator();
                    var dev = enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
                    dev.AudioEndpointVolume.MasterVolumeLevelScalar = value / 100f;
                    dev.AudioEndpointVolume.Mute = false;
                    _speakersVolume = value;
                }
                catch (Exception ex)
                {
                    _logger.Error(this, ex.Message);
                }
                OnPropChanged();
            }
        }

        int _microphoneVolume;
        private readonly string _mainAudioOut;
        private readonly ILogger _logger;

        /// <summary>
        /// Set microphones volume. Minimum 0, maximum 1
        /// </summary>
        public int MicrophoneVolume
        {
            get { return _microphoneVolume; }
            set
            {
                try
                {
                    _microphoneVolume = value;
                    var enumerator = new MMDeviceEnumerator();

                    var devs = enumerator.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active);
                    foreach (var dev in devs)
                    {
                        dev.AudioEndpointVolume.MasterVolumeLevelScalar = 1;
                        dev.AudioEndpointVolume.Mute = false;
                    }

                }
                catch (Exception ex)
                {
                    _logger.Error(this, ex.Message);
                }
                OnPropChanged();
            }
        }
        protected void OnPropChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}

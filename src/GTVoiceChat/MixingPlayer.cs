using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTVoiceChat
{
    public class MixingPlayer
    {
        private WaveFormat _waveFormat;
        private MixingSampleProvider _mixer;
        private IWavePlayer _waveOut;
        private Dictionary<string, MyProvider> _providers;

        public MixingPlayer(WaveFormat waveFormat)
        {
            _waveFormat = waveFormat;

            var mixerWaveFormat = WaveFormat.CreateIeeeFloatWaveFormat(_waveFormat.SampleRate, _waveFormat.Channels);
            _mixer = new MixingSampleProvider(mixerWaveFormat);

            _waveOut = new WaveOut();
            _waveOut.Init(_mixer);

            _providers = new Dictionary<string, MyProvider>();
        }

        public bool IsAddedProvider(string key)
        {
            return _providers.ContainsKey(key);
        }

        public void AddProvider(string key)
        {
            if (_providers.ContainsKey(key)) return;

            var provider = new MyProvider(_waveFormat);
            _mixer.AddMixerInput(provider.VolumeProvider);
            _providers.Add(key, provider);
        }

        public void RemoveProvider(string key)
        {
            if (!_providers.ContainsKey(key)) return;

            var provider = _providers[key];
            _mixer.RemoveMixerInput(provider.VolumeProvider);
            _providers.Remove(key);
        }

        public void Play(string key, byte[] decoded)
        {
            if (!_providers.ContainsKey(key)) return;

            _providers[key].AddSamples(decoded);
            _waveOut.Play();
        }

        public void ChangeVolume(string key, float volume)
        {
            if (!_providers.ContainsKey(key)) return;

            var provider = _providers[key];
            provider.ChangeVolume(volume);
        }

        public void ChangeGeneralVolume(float generalVolume)
        {
            foreach (var provider in _providers.Values)
            {
                provider.ChangeGeneralVolume(generalVolume);
            }
        }

        public void Dispose()
        {
            if (_waveOut != null)
            {
                _waveOut.Stop();
                //_waveOut.Dispose();
                _waveOut = null;
            }
        }
    }
}
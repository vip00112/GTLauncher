using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTVoiceChat
{
    public class BufferdPlayer
    {
        private IWavePlayer _waveOut;
        private MyProvider _provider;

        public BufferdPlayer(WaveFormat waveFormat)
        {
            _waveOut = new WaveOut();
            _provider = new MyProvider(waveFormat);
            _waveOut.Init(_provider.VolumeProvider);

        }

        public void Play(string key, byte[] decoded)
        {
            _provider.AddSamples(decoded);
            _waveOut.Play();
        }

        public void ChangeVolume(string key, float volume)
        {
            _provider.ChangeVolume(volume);
        }

        public void ChangeGeneralVolume(float generalVolume)
        {
            _provider.ChangeGeneralVolume(generalVolume);
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

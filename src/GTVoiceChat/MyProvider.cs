using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTVoiceChat
{
    public class MyProvider
    {
        private BufferedWaveProvider _bufferd;
        private float _generalVolume;
        private float _volume;

        #region Constructor
        public MyProvider(WaveFormat waveFormat)
        {
            _bufferd = new BufferedWaveProvider(waveFormat);
            _generalVolume = 1.0f;
            _volume = 1.0f;
            Sample = new VolumeSampleProvider(_bufferd.ToSampleProvider());
        }
        #endregion

        #region Properties
        public VolumeSampleProvider Sample { get; }
        #endregion

        #region Public Method
        public void AddSamples(byte[] decoded)
        {
            _bufferd.AddSamples(decoded, 0, decoded.Length);
        }

        public void ChangeVolume(float volume)
        {
            _volume = volume;
            Sample.Volume = _generalVolume * _volume;
        }

        public void ChangeGeneralVolume(float generalVolume)
        {
            _generalVolume = generalVolume;
            Sample.Volume = _generalVolume * _volume;
        }
        #endregion

        #region Static Method
        public static WaveFormat CreateFormat(INetworkChatCodec codec)
        {
            int sampleRate = codec.RecordFormat.SampleRate;
            int channels = codec.RecordFormat.Channels;
            return WaveFormat.CreateIeeeFloatWaveFormat(sampleRate, channels);
        }
        #endregion
    }
}

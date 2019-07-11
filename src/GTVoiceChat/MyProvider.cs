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
        #region Constructor
        public MyProvider(WaveFormat waveFormat)
        {
            BufferdProvider = new BufferedWaveProvider(waveFormat);
            VolumeProvider = new VolumeSampleProvider(BufferdProvider.ToSampleProvider());
            GeneralVolume = 1.0f;
            Volume = 1.0f;
        }
        #endregion

        #region Properties
        public BufferedWaveProvider BufferdProvider { get; }

        public VolumeSampleProvider VolumeProvider { get; }

        public float GeneralVolume { get; private set; }

        public float Volume { get; private set; }
        #endregion

        #region Public Method
        public void AddSamples(byte[] decoded)
        {
            BufferdProvider.AddSamples(decoded, 0, decoded.Length);
        }

        public void ChangeVolume(float volume)
        {
            Volume = volume;
            VolumeProvider.Volume = GeneralVolume * Volume;
        }

        public void ChangeGeneralVolume(float generalVolume)
        {
            GeneralVolume = generalVolume;
            VolumeProvider.Volume = GeneralVolume * Volume;
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

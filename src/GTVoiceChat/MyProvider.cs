using NAudio.Wave;
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

        #region Constructor
        public MyProvider(WaveFormat waveFormat)
        {
            _bufferd = new BufferedWaveProvider(waveFormat);
            Sample = _bufferd.ToSampleProvider();
        }
        #endregion

        #region Properties
        public ISampleProvider Sample { get; }
        #endregion

        #region Public Method
        public void AddSamples(byte[] decoded)
        {
            _bufferd.AddSamples(decoded, 0, decoded.Length);
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

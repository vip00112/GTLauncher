using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTVoiceChat
{
    public class AudioOutput
    {
        private INetworkChatCodec _codec;
        private IWavePlayer _waveOut;
        private BufferedWaveProvider _waveProvider;

        #region Constructor
        public AudioOutput(INetworkChatCodec codec)
        {
            _codec = codec;
            _waveOut = new WaveOut();
            _waveProvider = new BufferedWaveProvider(_codec.RecordFormat);
            _waveOut.Init(_waveProvider);
            _waveOut.Play();
        }
        #endregion

        #region Public Method
        public void Play(byte[] data)
        {
            byte[] decoded = _codec.Decode(data, 0, data.Length);
            _waveProvider.AddSamples(decoded, 0, decoded.Length);
        }

        public void Dispose()
        {
            if (_waveOut != null)
            {
                _waveOut.Stop();
                _waveOut.Dispose();
            }
            if (_codec != null)
            {
                _codec.Dispose();
            }
            _waveOut = null;
            _codec = null;
        }
        #endregion
    }
}

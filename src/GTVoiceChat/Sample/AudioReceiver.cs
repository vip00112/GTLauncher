using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTVoiceChat
{
    public class AudioReceiver : IDisposable
    {
        private readonly INetworkChatCodec _codec;
        private readonly UdpAudioReceiver _receiver;
        private readonly IWavePlayer _waveOut;
        private readonly BufferedWaveProvider _waveProvider;

        public AudioReceiver(INetworkChatCodec codec, UdpAudioReceiver receiver)
        {
            _codec = codec;
            _receiver = receiver;
            _receiver.OnReceived(OnDataReceived);

            _waveOut = new WaveOut();
            _waveProvider = new BufferedWaveProvider(codec.RecordFormat);
            _waveOut.Init(_waveProvider);
            _waveOut.Play();
        }

        void OnDataReceived(byte[] compressed)
        {
            byte[] decoded = _codec.Decode(compressed, 0, compressed.Length);
            _waveProvider.AddSamples(decoded, 0, decoded.Length);
        }

        public void Dispose()
        {
            _receiver?.Dispose();
            _waveOut?.Dispose();
        }
    }
}

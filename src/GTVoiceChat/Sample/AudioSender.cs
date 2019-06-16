using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTVoiceChat
{
    public class AudioSender : IDisposable
    {
        private readonly INetworkChatCodec _codec;
        private readonly UdpAudioSender _sender;
        private readonly WaveIn _waveIn;

        public AudioSender(INetworkChatCodec codec, int inputDeviceNumber, UdpAudioSender sender)
        {
            _codec = codec;
            _sender = sender;
            _waveIn = new WaveIn();
            _waveIn.BufferMilliseconds = 50;
            _waveIn.DeviceNumber = inputDeviceNumber;
            _waveIn.WaveFormat = codec.RecordFormat;
            _waveIn.DataAvailable += OnAudioCaptured;
            _waveIn.StartRecording();
        }

        void OnAudioCaptured(object sender, WaveInEventArgs e)
        {
            byte[] encoded = _codec.Encode(e.Buffer, 0, e.BytesRecorded);
            _sender.Send(encoded);
        }

        public void Dispose()
        {
            _waveIn.DataAvailable -= OnAudioCaptured;
            _waveIn.StopRecording();
            _waveIn.Dispose();
            _waveIn?.Dispose();
            _sender?.Dispose();
        }
    }
}

using NAudio.Wave;
using NSpeex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTVoiceChat
{
    public class WideBandSpeexCodec : INetworkChatCodec
    {
        private readonly SpeexDecoder _decoder;
        private readonly SpeexEncoder _encoder;
        private readonly WaveBuffer _encoderInputBuffer;

        #region Constructor
        public WideBandSpeexCodec()
        {
            _decoder = new SpeexDecoder(BandMode.Wide);
            _encoder = new SpeexEncoder(BandMode.Wide);
            RecordFormat = new WaveFormat(16000, 16, 1);
            _encoderInputBuffer = new WaveBuffer(RecordFormat.AverageBytesPerSecond);
        }
        #endregion

        #region Properties
        public string Name { get { return "Speex Wide Band (16kHz)"; } }

        public bool IsAvailable { get { return true; } }

        public int BitsPerSecond { get { return -1; } }

        public WaveFormat RecordFormat { get; }
        #endregion

        #region Public Method
        public byte[] Encode(byte[] data, int offset, int length)
        {
            FeedSamplesIntoEncoderInputBuffer(data, offset, length);
            int samplesToEncode = _encoderInputBuffer.ShortBufferCount;
            if (samplesToEncode % _encoder.FrameSize != 0)
            {
                samplesToEncode -= samplesToEncode % _encoder.FrameSize;
            }
            var outputBufferTemp = new byte[length]; // contains more than enough space
            int bytesWritten = _encoder.Encode(_encoderInputBuffer.ShortBuffer, 0, samplesToEncode, outputBufferTemp, 0, length);
            var encoded = new byte[bytesWritten];
            Array.Copy(outputBufferTemp, 0, encoded, 0, bytesWritten);
            ShiftLeftoverSamplesDown(samplesToEncode);
            return encoded;
        }

        public byte[] Decode(byte[] data, int offset, int length)
        {
            var outputBufferTemp = new byte[length * 320];
            var wb = new WaveBuffer(outputBufferTemp);
            int samplesDecoded = _decoder.Decode(data, offset, length, wb.ShortBuffer, 0, false);
            int bytesDecoded = samplesDecoded * 2;
            var decoded = new byte[bytesDecoded];
            Array.Copy(outputBufferTemp, 0, decoded, 0, bytesDecoded);
            return decoded;
        }

        public void Dispose() { }

        public INetworkChatCodec Clone()
        {
            return new WideBandSpeexCodec();
        }
        #endregion

        #region Private Method
        private void ShiftLeftoverSamplesDown(int samplesEncoded)
        {
            int leftoverSamples = _encoderInputBuffer.ShortBufferCount - samplesEncoded;
            Array.Copy(_encoderInputBuffer.ByteBuffer, samplesEncoded * 2, _encoderInputBuffer.ByteBuffer, 0, leftoverSamples * 2);
            _encoderInputBuffer.ShortBufferCount = leftoverSamples;
        }

        private void FeedSamplesIntoEncoderInputBuffer(byte[] data, int offset, int length)
        {
            Array.Copy(data, offset, _encoderInputBuffer.ByteBuffer, _encoderInputBuffer.ByteBufferCount, length);
            _encoderInputBuffer.ByteBufferCount += length;
        }
        #endregion
    }
}

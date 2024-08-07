using System;
using System.IO;

namespace DG
{
    public class MemoryInputStream : InputStream
    {
        private byte[] _buffer;


        public MemoryInputStream(byte[] inBuffer)
        {
            _buffer = inBuffer;
            _length = inBuffer.Length;
        }


        public void Reset(byte[] inBuffer, int length)
        {
            _buffer = inBuffer;
            _length = length;
            _pos = 0;
        }


        private void ClearBuf(byte[] buffer, int offset, int length)
        {
            if (buffer == null) return;
            var num = buffer.Length;
            if (offset < 0) offset = 0;
            if (offset >= num) offset = num - 1;
            if (length < 0) length = 0;
            if (offset + length >= num) length = num - offset - 1;
            for (var i = 0; i < length; i++) buffer[offset + i] = 0;
        }


        public override byte[] GetBuffer()
        {
            return _buffer;
        }

        public override void Peek(byte[] buffer, int offset, int length)
        {
            if (_pos + length > _length)
            {
                var text = string.Concat(
                    "Peek out of stream,want -> mPos:",
                    _pos,
                    ",length: ",
                    length,
                    ",offset: ",
                    offset,
                    ", but mLength:",
                    _length,
                    ",buf.Length: ",
                    buffer.Length
                );
                var text2 = " --->bytes[";
                for (var i = 0; i < _buffer.Length; i++)
                {
                    text2 += StringConst.STRING_COMMA;
                    text2 += _buffer[i];
                }

                text += text2;
                text += StringConst.STRING_RIGHT_SQUARE_BRACKETS;
                ClearBuf(buffer, offset, length);
                throw new IOException(text);
            }

            Buffer.BlockCopy(_buffer, _pos, buffer, offset, length);
        }

        public override void Read(byte[] buffer, int offset, int length)
        {
            Peek(buffer, offset, length);
            _pos += length;
        }

        public override void Seek(int length)
        {
            if (_pos + length > _length)
            {
                DGLog.Warn(string.Concat(
                    "Seek out of stream, wanted:",
                    _pos + length,
                    ", but:",
                    _length
                ));
                _pos = _length;
                return;
            }

            _pos = length;
        }

        public override void Skip(int length)
        {
            if (_pos + length > _length)
            {
                DGLog.Warn(string.Concat(
                    "Skip out of stream, wanted:",
                    _pos + length,
                    ", but:",
                    _length
                ));
                _pos = _length;
                return;
            }

            _pos += length;
        }
    }
}
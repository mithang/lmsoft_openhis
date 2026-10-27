using System;
using System.IO;
using System.IO.Compression;

namespace Newtouch.CIS.Compat
{
    /// <summary>
    /// Minimal pure-managed PNG encoder (8-bit grayscale, non-interlaced).
    /// Used by the barcode / QR generators so image output works on Linux
    /// without libgdiplus / System.Drawing.
    /// </summary>
    public static class PngEncoder
    {
        public static byte[] EncodeGrayscale(byte[] pixels, int width, int height)
        {
            if (pixels == null || pixels.Length < width * height)
            {
                throw new ArgumentException("Pixel buffer is too small.", nameof(pixels));
            }
            using var ms = new MemoryStream();
            // PNG signature
            ms.Write(new byte[] { 137, 80, 78, 71, 13, 10, 26, 10 }, 0, 8);
            // IHDR: width, height, bit depth 8, color type 0 (grayscale)
            using (var ihdr = new MemoryStream())
            {
                WriteInt32BigEndian(ihdr, width);
                WriteInt32BigEndian(ihdr, height);
                ihdr.WriteByte(8);
                ihdr.WriteByte(0);
                ihdr.WriteByte(0);
                ihdr.WriteByte(0);
                ihdr.WriteByte(0);
                WriteChunk(ms, "IHDR", ihdr.ToArray());
            }
            // IDAT: zlib stream of scanlines, each prefixed with filter byte 0
            using (var raw = new MemoryStream())
            {
                for (int y = 0; y < height; y++)
                {
                    raw.WriteByte(0);
                    raw.Write(pixels, y * width, width);
                }
                raw.Position = 0;
                using var compressed = new MemoryStream();
                using (var zlib = new ZLibStream(compressed, CompressionLevel.Optimal, true))
                {
                    raw.CopyTo(zlib);
                }
                WriteChunk(ms, "IDAT", compressed.ToArray());
            }
            WriteChunk(ms, "IEND", Array.Empty<byte>());
            return ms.ToArray();
        }

        private static void WriteChunk(Stream stream, string type, byte[] data)
        {
            WriteInt32BigEndian(stream, data.Length);
            var typeBytes = System.Text.Encoding.ASCII.GetBytes(type);
            stream.Write(typeBytes, 0, 4);
            stream.Write(data, 0, data.Length);
            uint crc = Crc32(typeBytes, data);
            WriteUInt32BigEndian(stream, crc);
        }

        private static void WriteInt32BigEndian(Stream stream, int value)
        {
            stream.WriteByte((byte)((value >> 24) & 0xFF));
            stream.WriteByte((byte)((value >> 16) & 0xFF));
            stream.WriteByte((byte)((value >> 8) & 0xFF));
            stream.WriteByte((byte)(value & 0xFF));
        }

        private static void WriteUInt32BigEndian(Stream stream, uint value)
        {
            stream.WriteByte((byte)((value >> 24) & 0xFF));
            stream.WriteByte((byte)((value >> 16) & 0xFF));
            stream.WriteByte((byte)((value >> 8) & 0xFF));
            stream.WriteByte((byte)(value & 0xFF));
        }

        private static readonly uint[] _crcTable = BuildCrcTable();

        private static uint[] BuildCrcTable()
        {
            var table = new uint[256];
            for (uint n = 0; n < 256; n++)
            {
                uint c = n;
                for (int k = 0; k < 8; k++)
                {
                    c = (c & 1) != 0 ? 0xEDB88320u ^ (c >> 1) : c >> 1;
                }
                table[n] = c;
            }
            return table;
        }

        private static uint Crc32(byte[] typeBytes, byte[] data)
        {
            uint c = 0xFFFFFFFFu;
            foreach (var b in typeBytes)
            {
                c = _crcTable[(c ^ b) & 0xFF] ^ (c >> 8);
            }
            foreach (var b in data)
            {
                c = _crcTable[(c ^ b) & 0xFF] ^ (c >> 8);
            }
            return c ^ 0xFFFFFFFFu;
        }
    }
}

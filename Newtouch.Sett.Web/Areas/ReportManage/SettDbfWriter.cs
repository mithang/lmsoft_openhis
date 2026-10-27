using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace Newtouch.Sett.Web.Areas.ReportManage.Controllers
{
    /// <summary>Minimal dBase III (.dbf) writer, written to replace the
    /// third-party DBF library the Framework app used (no source, no
    /// .NET Core build). Supports exactly the call shape ReportController
    /// uses: ctor(Stream), settable Fields + CharEncoding, WriteRecord(object[]).
    /// Header is written on the first record (Fields must be set by then);
    /// record count is patched and the 0x1A EOF marker appended on Dispose.
    /// The underlying stream is NOT closed (the caller owns it).</summary>
    public enum NativeDbType
    {
        Char,
        Numeric,
        Date,
        Logical,
        Memo,
        Float
    }

    public class DBFField
    {
        public DBFField(string name, NativeDbType type, int length)
            : this(name, type, length, 0)
        {
        }

        public DBFField(string name, NativeDbType type, int length, int decimalCount)
        {
            Name = name;
            Type = type;
            Length = length;
            DecimalCount = decimalCount;
        }

        public string Name { get; set; }
        public NativeDbType Type { get; set; }
        public int Length { get; set; }
        public int DecimalCount { get; set; }
    }

    public class DBFWriter : IDisposable
    {
        private readonly Stream _stream;
        private bool _headerWritten;
        private int _recordCount;
        private bool _disposed;

        public DBFWriter(Stream stream)
        {
            _stream = stream;
        }

        public DBFField[] Fields { get; set; }

        public Encoding CharEncoding { get; set; } = Encoding.UTF8;

        public void WriteRecord(object[] values)
        {
            if (_disposed) throw new ObjectDisposedException(nameof(DBFWriter));
            if (!_headerWritten)
            {
                if (Fields == null || Fields.Length == 0)
                    throw new InvalidOperationException("Chưa đặt danh sách trường DBF trước khi ghi bản ghi.");
                WriteHeader();
                _headerWritten = true;
            }
            var record = new List<byte> { (byte)' ' };
            for (int i = 0; i < Fields.Length; i++)
            {
                var field = Fields[i];
                var value = i < values.Length ? values[i] : null;
                record.AddRange(FormatField(field, value));
            }
            _stream.Write(record.ToArray(), 0, record.Count);
            _recordCount++;
        }

        private void WriteHeader()
        {
            int recordLength = 1;
            foreach (var f in Fields) recordLength += f.Length;
            int headerLength = 32 + 32 * Fields.Length + 1;
            var header = new byte[headerLength];
            header[0] = 0x03; // dBase III, no memo
            // bytes 4-7: record count, patched on Dispose
            header[8] = (byte)(headerLength & 0xFF);
            header[9] = (byte)((headerLength >> 8) & 0xFF);
            header[10] = (byte)(recordLength & 0xFF);
            header[11] = (byte)((recordLength >> 8) & 0xFF);
            _stream.Write(header, 0, 32);
            foreach (var f in Fields)
            {
                var desc = new byte[32];
                var nameBytes = Encoding.ASCII.GetBytes(f.Name ?? "");
                Array.Copy(nameBytes, desc, Math.Min(nameBytes.Length, 11));
                desc[11] = (byte)(f.Type == NativeDbType.Numeric || f.Type == NativeDbType.Float ? 'N'
                    : f.Type == NativeDbType.Date ? 'D'
                    : f.Type == NativeDbType.Logical ? 'L' : 'C');
                desc[16] = (byte)f.Length;
                desc[17] = (byte)f.DecimalCount;
                _stream.Write(desc, 0, 32);
            }
            _stream.WriteByte(0x0D);
        }

        private byte[] FormatField(DBFField field, object value)
        {
            string text;
            if (field.Type == NativeDbType.Numeric || field.Type == NativeDbType.Float)
            {
                if (value == null || value == DBNull.Value) text = "";
                else if (decimal.TryParse(Convert.ToString(value, CultureInfo.InvariantCulture),
                    NumberStyles.Any, CultureInfo.InvariantCulture, out var dec))
                    text = dec.ToString("F" + field.DecimalCount, CultureInfo.InvariantCulture);
                else text = Convert.ToString(value, CultureInfo.InvariantCulture) ?? "";
                var bytes = Encoding.ASCII.GetBytes(text);
                if (bytes.Length > field.Length)
                    bytes = SubArray(bytes, bytes.Length - field.Length, field.Length);
                var padded = new byte[field.Length];
                Array.Fill(padded, (byte)' ');
                Array.Copy(bytes, 0, padded, field.Length - bytes.Length, bytes.Length);
                return padded;
            }
            text = value == null || value == DBNull.Value ? "" : Convert.ToString(value, CultureInfo.InvariantCulture) ?? "";
            var raw = CharEncoding.GetBytes(text);
            if (raw.Length > field.Length) raw = TruncateBytes(raw, field.Length);
            var result = new byte[field.Length];
            Array.Fill(result, (byte)' ');
            Array.Copy(raw, result, raw.Length);
            return result;
        }

        private static byte[] SubArray(byte[] src, int offset, int length)
        {
            var dst = new byte[length];
            Array.Copy(src, offset, dst, 0, length);
            return dst;
        }

        private byte[] TruncateBytes(byte[] raw, int maxLength)
        {
            // DBF fields are fixed-width in BYTES; a truncated multi-byte
            // tail is acceptable (consumers read these as single-byte
            // code pages), byte length must stay exact.
            return SubArray(raw, 0, Math.Min(raw.Length, maxLength));
        }

        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;
            if (_headerWritten)
            {
                long end = _stream.Position;
                _stream.Seek(4, SeekOrigin.Begin);
                var count = BitConverter.GetBytes(_recordCount);
                _stream.Write(count, 0, 4);
                _stream.Seek(end, SeekOrigin.Begin);
                _stream.WriteByte(0x1A);
            }
            _stream.Flush();
        }
    }
}

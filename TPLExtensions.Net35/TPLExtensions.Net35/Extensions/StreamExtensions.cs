namespace TPLExtensions.Net35.Extensions
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    public static class StreamExtensions
    {
        public static async Task<byte[]> ReadAsync(this Stream stream, int nbytes)
        {
            var buf = new byte[nbytes];
            var readpos = 0;
            while (readpos < nbytes) readpos += await stream.ReadAsync(buf, readpos, nbytes - readpos);
            return buf;
        }

        public static Task<int> ReadAsync(this Stream stream, byte[] buffer, int offset, int count)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            return Task<int>.Factory.FromAsync(stream.BeginRead, stream.EndRead, buffer, offset, count, null);
        }

        public static Task WriteAsync(this Stream stream, byte[] buffer, int offset, int count)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            return Task.Factory.FromAsync(stream.BeginWrite, stream.EndWrite, buffer, offset, count, null);
        }
    }
}

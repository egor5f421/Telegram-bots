namespace Telegram_bots.Files
{
    /// <summary>
    /// File to send
    /// </summary>
    public class InputFile
    {
        /// <summary>
        /// File name
        /// </summary>
        public string FileName { private set; get; }
        /// <summary>
        /// File Data
        /// </summary>
        public byte[] Data { set; get; } = [];

        /// <summary>
        /// Creates a new file to send
        /// </summary>
        /// <param name="stream">Stream to receive the file</param>
        /// <param name="fileName">File Name</param>
        public InputFile(Stream stream, string fileName)
        {
            FileName = fileName.Trim(' ', '/');

            Data = GetData(stream);
        }

        /// <summary>
        /// Creates a new file to send
        /// </summary>
        /// <param name="filePath">File Name</param>
        public InputFile(string filePath)
        {
            using FileStream stream = System.IO.File.OpenRead(filePath);

            FileName = Path.GetFileName(filePath);

            Data = GetData(stream);
        }

        private static byte[] GetData(Stream stream)
        {
            byte[] data = new byte[1024];
            using MemoryStream ms = new();
            int numBytesRead;
            while ((numBytesRead = stream.Read(data, 0, data.Length)) > 0)
            {
                ms.Write(data, 0, numBytesRead);
            }
            return ms.ToArray();
        }
    }
}

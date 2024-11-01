namespace Telegram_bots.Files
{
    public class File
    {
        public string FileName { private set; get; }
        public byte[] Data { set; get; } = [];

        public File(Stream stream, string fileName)
        {
            FileName = fileName.Trim(' ', '/');

            byte[] data = new byte[1024];
            using MemoryStream ms = new();
            int numBytesRead;
            while ((numBytesRead = stream.Read(data, 0, data.Length)) > 0)
            {
                ms.Write(data, 0, numBytesRead);
            }
            Data = ms.ToArray();
        }

        public File(string filePath)
        {
            using FileStream stream = System.IO.File.OpenRead(filePath);

            FileName = Path.GetFileName(filePath);

            byte[] data = new byte[1024];
            using MemoryStream ms = new();
            int numBytesRead;
            while ((numBytesRead = stream.Read(data, 0, data.Length)) > 0)
            {
                ms.Write(data, 0, numBytesRead);
            }
            Data = ms.ToArray();
        }
    }
}

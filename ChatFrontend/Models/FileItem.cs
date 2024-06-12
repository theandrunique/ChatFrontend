using System.Windows.Media.Imaging;

namespace ChatFrontend.Models
{
    public class FileItem
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public long FileSize { get; set; }
        public BitmapSource FileIcon { get; set; }

        public string NormalizedFileSize
        {
            get
            {
                return FormatFileSize(FileSize);
            }
        }

        private string FormatFileSize(long fileSize)
        {
            if (fileSize >= 1024 * 1024 * 1024)
            {
                return $"{fileSize / (1024.0 * 1024.0 * 1024.0):0.##} GB";
            }
            else if (fileSize >= 1024 * 1024)
            {
                return $"{fileSize / (1024.0 * 1024.0):0.##} MB";
            }
            else if (fileSize >= 1024)
            {
                return $"{fileSize / 1024.0:0.##} KB";
            }
            else
            {
                return $"{fileSize} bytes";
            }
        }
    }
}

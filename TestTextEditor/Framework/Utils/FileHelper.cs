using System.IO;

namespace TestTextEditor.Framework.Utils
{
    public static class FileHelper
    {
        public static string ReadAndDelete(string path)
        {
            var result = File.ReadAllText(path);
            File.Delete(path);
            return result;
        }

        public static string Read(string path) => File.ReadAllText(path);
    }
}
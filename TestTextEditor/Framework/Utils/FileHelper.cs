using System.IO;

namespace TestTextEditor.Framework.Utils
{
    public static class FileHelper
    {
        public static string ReadAndDelete(string path)
        {
            var sr = new StreamReader(path);
            var result = sr.ReadToEnd();
            sr.Close();
            File.Delete(path);
            return result;
        }
    }
}
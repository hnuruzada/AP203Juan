using System.IO;

namespace AP203Juan.Helpers
{
    public static class Helper
    {
        public static void DeleteImg(string root,string folder,string path)
        {
            string fullPath=Path.Combine(root,folder,path);
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }
    }
}

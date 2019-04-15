using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MagicTheGatheringApp.Managers
{
    public static class ImageManager
    {
        public static void SavePicture(string name, Stream data, string location)
        {
            var imgPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            imgPath = Path.Combine(imgPath, "Orders", location);
            Directory.CreateDirectory(imgPath);

            string filePath = Path.Combine(imgPath, name);

            byte[] byteArray = new byte[data.Length];
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                using (data)
                {
                    data.Read(byteArray, 0, (int)data.Length);
                }
                int length = byteArray.Length;
                fs.Write(byteArray, 0, length);
            }
        }
    }
}

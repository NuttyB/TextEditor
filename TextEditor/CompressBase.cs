using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using System.IO;
using System.Text.RegularExpressions;

namespace TextEditor
{
    class CompressBase
    {
        public static void Compress(string PathFile,string PathFolder,string FileName)//string DataBase)
        {
            
            using (FileStream fs = new FileStream(PathFolder + FileName + ".zip", FileMode.Create))
            using (ZipArchive arch = new ZipArchive(fs, ZipArchiveMode.Create))
            {

                arch.CreateEntryFromFile(PathFile, FileName + ".db");
                fs.Close();
            }
           

            File.Delete(PathFile);

            /*  string startPath = @".\base";
              string zipPath = @".\zis.zip";
              string extractPath = @".\extract";

              ZipFile.CreateFromDirectory(startPath, zipPath,CompressionLevel.Optimal,true);
              */
            //    ZipFile.ExtractToDirectory(zipPath, extractPath); 
        }
        public static void DeCompress(string PathFolder, string PathFileName)//string DataBase)
        {
            ZipFile.ExtractToDirectory(PathFolder + PathFileName + @".zip", PathFolder);
        }
    }
}

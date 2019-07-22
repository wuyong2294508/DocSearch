using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DocSearch.Controller
{
    class FindFiles
    {
        private List<String> fileNames = new List<string>();

        public List<string> findFiles(string path)
        {
            findFiles1(path);
            return fileNames;
        }

        private void findFiles1(string path)
        {
            //List<String> fileNames = new List<string>();
            DirectoryInfo di = new DirectoryInfo(path);
            FileSystemInfo[] fsinfos = di.GetFileSystemInfos();
            foreach (FileSystemInfo fsinfo in fsinfos)
            {
                String name = fsinfo.FullName;
                
                Console.WriteLine(name);
                if (fsinfo is DirectoryInfo)     //判断是否为文件夹
                {
                    findFiles1(name);//递归调用
                }
                else 
                {
                    string tolower = fsinfo.Extension.ToLower();
                    bool isDocPdf = tolower.Equals(".doc") || tolower.Equals(".docx") || tolower.Equals(".pdf");
                    if(isDocPdf)
                        fileNames.Add(name);
                }
            }
        }
    }
}

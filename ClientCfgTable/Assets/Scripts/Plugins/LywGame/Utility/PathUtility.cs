using System.IO;

namespace LywGames
{
    public static class PathUtility 
    {
        /** 统一路径
         * 1. String.Trim 返回一个新字符串, 它相当于从当前String对象中移除了一组指定字符的所有前导匹配项和尾随匹配项。
         * 2. Path.DirectorySeparatorChar 提供平台特定的字符, 该字符用于在反映分层文件系统组织的路径字符串中分隔目录级别。(Windows上反斜杠\ Linux上正斜杠/)
         * 3. Path.AltDirectorySeparatorChar 提供平台特定的字符, 该字符用于在反映分层文件系统组织的路径字符串中分隔目录级别。(Windows上正斜杠/ Linux上正斜杠/)
         * */
        public static string UnifyPath(string path, bool toLower = false)
        {
            string unitiedPath = (toLower ? path.ToLower() : path).Trim(new char[] { '/', '\\' }).Replace("//", "/");
            unitiedPath = unitiedPath.Replace(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar); // 替换反斜杠为正斜杠
            return unitiedPath;
        }

        public static string TripExtension(string path)
        {
            if (Path.HasExtension(path))
            {
                return Path.Combine(Path.GetDirectoryName(path), Path.GetFileNameWithoutExtension(path));
            }
            else
            {
                return path;
            }
        }

        public static string GetFileNameWithoutExtension(string path)
        {
            return Path.GetFileNameWithoutExtension(path);
        }

        public static string GetExtension(string path)
        {
            if (Path.HasExtension(path))
            {
                return Path.GetExtension(path);
            }
            else
            {
                return string.Empty;
            }
        }

        public static string Combine(params string[] pathArray)
        {
            return Combine(true, pathArray);
        }

        public static string Combine(bool toLower, params string[] pathArray)
        {
            if (pathArray == null || pathArray.Length == 0)
            {
                return string.Empty;
            }

            string path = string.Empty;
            foreach (var item in pathArray)
            {
                path += item + Path.AltDirectorySeparatorChar;
            }

            return UnifyPath(path, toLower);
        }

        public static string GetLocalFileUrl(string url)
        {
            if (!url.Contains("file:///"))
            {
                url = "file:///" + UnifyPath(url);
            }
            return url;
        }

        // Directory.CreateDirectory(string path) 在指定路径中创建所有目录和子目录, 除非它们已经存在。
        public static bool CreateDirectory(string directory)
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
                return Directory.Exists(directory);
            }

            return true;
        }

        /** 1. Directory.Exists(String) 确定给定路径是否引用磁盘上的现有目录。
         *  2. File.Exists(String) 确定指定的文件是否存在。
         *  string path = "E:/Client/Assets/Scripts";
         *  string filePath = "E:/Client/Assets/Scripts/GameMain.cs";
         *  Directory.Exists(path) 目录存在返回True Directory.Exists(filePath) 文件存在返回False, 只能判断目录, 不能判断文件
         *  File.Exists(path) 目录存在返回False File.Exists(filePath) 文件存在返回True, 只能判断文件, 不能判断目录
         *  
         *  3. Delete(String) 从指定路径删除空目录。
         *  4. Directory.Delete(String, Boolean) 删除指定的目录, 并删除该目录中的所有子目录和文件(如果表示)。
         * */
        public static void RemovePath(string path, bool recursive)
        {
            if (Directory.Exists(path))
            {
                Directory.Delete(path, recursive);
            }
        }

        public static void RemoveFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        /** 1. public static string GetFullPath (string path); 返回指定路径字符串的绝对路径。
         *  path string 要获取其绝对路径信息的文件或目录, path不需要存在。如果path是相对路径, 此重载方法返回可以基于当前驱动器和当前目录的完全限定的路径。
         * */
        public static void FileCopy(string sourceFile, string targetFile)
        {
            sourceFile = Path.GetFullPath(sourceFile);
            targetFile = Path.GetFullPath(targetFile);

            string targetPath = Path.GetDirectoryName(targetFile);
            if (!CreateDirectory(targetPath))
            {
                return;
            }

            RemoveFile(targetFile);
            File.Copy(sourceFile, targetFile);
        }

        /** 递归拷贝文件夹到目标文件夹
         * 1. DirectoryInfo 公开用于创建、移动和枚举目录和子目录的实例方法。 此类不能被继承。
         *    Exists 获取指示目录是否存在的值。
         *    FullName 获取目录的完整路径。
         *    Name 目录名称。
         *    GetFiles() 返回当前目录的文件列表。
         *    GetDirectories() 返回当前目录的子目录。
         *    
         *  2. FileInfo 提供创建、复制、删除、移动和打开文件的属性和实例方法，并且帮助创建 FileStream 对象。
         *  Name 获取文件名。
         *  CopyTo(String, Boolean)	将现有文件复制到新文件, 允许覆盖现有文件。  
         * */
        public static void DirectoryCopy(string sourceDir, string targetDir, bool recursive)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(sourceDir);

            if (!dirInfo.Exists)
            {
                return;
            }
            if (!CreateDirectory(targetDir))
            {
                return;
            }

            FileInfo[] fileInfoArray = dirInfo.GetFiles();
            foreach (var fileInfo in fileInfoArray)
            {
                string targetFilePath = Path.Combine(targetDir, fileInfo.Name);
                fileInfo.CopyTo(targetFilePath, true);
            }

            if (recursive)
            {
                DirectoryInfo[] dirInfoArray = dirInfo.GetDirectories();
                foreach (var subDirInfo in dirInfoArray)
                {
                    if (string.Compare(subDirInfo.Name, ".svn", true) == 0)
                    {
                        continue;
                    }
                    string subTargetDir = Path.Combine(targetDir, subDirInfo.Name);
                    DirectoryCopy(subDirInfo.FullName, subTargetDir, recursive);
                }
            }
        }

    }
}

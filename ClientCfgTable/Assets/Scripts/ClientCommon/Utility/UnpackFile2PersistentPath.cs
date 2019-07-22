using System;
using System.IO;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LywGames;

namespace ClientCommon
{
    /// <summary>
    /// 辅助类, 用于把包内的配置文件解压到包的外部私有持久路径
    /// </summary>
    public class UnpackFile2PersistentPath
    {
        public static string UnPackFileError = "";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subPath">包内文件的相对路径, 后边不带'/', 可以是""</param>
        /// <param name="existMarkFile">检查这个文件, 如果不存在，则说明没解压过, 必须解压</param>
        /// <param name="needCheckFileVersion">true: 表示在文件存在的前提下, 还要检查文件里的版本号是比较旧</param>
        /// <param name="filePathList">需要解压的文件名列表, 可以包含路径, 前后没有'/'</param>
        /// <param name="compress">是否需要解压</param>
        /// <param name="onFinishDelegate">完成时的回调</param>
        /// <returns></returns>
        public static IEnumerator ConstructLocalConfigDbFile(string subPath, string existMarkFile, bool needCheckFileVersion, List<string> filePathList, bool compress, Action<bool> onFinishDelegate, MonoBehaviour coroutineMb)
        {
            bool needCache = false;
            long newVersion = 0;

            if (needCheckFileVersion)
            {
                long oldVersion = long.MinValue;

                string markFilePath = PathUtility.GetLocalUrl4WWW(FileManager.GetStreamingAssetsPath(subPath)) + Path.AltDirectorySeparatorChar + existMarkFile;
                WWW loader = new WWW(markFilePath);
                yield return loader;
                
                try
                {
                    newVersion = long.Parse(loader.text);
                }
                catch (Exception e)
                {
                    Debug.LogError(e.StackTrace);
                }

                markFilePath = FileManager.GetPersistentDataPath(subPath) + Path.AltDirectorySeparatorChar + existMarkFile;
                if (File.Exists(markFilePath))
                {
                    loader = new WWW(PathUtility.GetLocalUrl4WWW(markFilePath));
                    yield return loader;
                    try
                    {
                        oldVersion = long.Parse(loader.text);
                    }
                    catch (Exception e)
                    {
                        Debug.LogError(e.Message);
                    }
                }
                else
                {
                    PathUtility.CreateDirectory(Path.GetDirectoryName(markFilePath));
                    WriteFile(markFilePath, oldVersion.ToString());
                }

                if (newVersion > oldVersion || newVersion == 0) // 如果是0的话每次都拷贝
                {
                    needCache = true;
                }
            }
            else
            {
                string checkPath = FileManager.GetPersistentDataPath(subPath) + Path.AltDirectorySeparatorChar + existMarkFile;
                needCache = !File.Exists(checkPath);
            }

            bool copyAllFileSuccess = true;
            int copyFileCount = 0;
            if (needCache)
            {
                foreach (var filePath in filePathList)
                {
                    string persistentFilePath = FileManager.GetPersistentDataPath(subPath + Path.AltDirectorySeparatorChar + filePath);
                    string streamingFilePath = FileManager.GetStreamingAssetsPath(subPath + Path.AltDirectorySeparatorChar + filePath);

                    /// <summary>
                    /// warning!!!!!
                    /// 为什么这么写
                    /// 似乎是ios上独有的问题，如果yield return www的话，如果www请求了一个不存在的地址（或者出错?) 
                    /// 那么www会一直等待，永远不会执行到下一句
                    /// 所以在外边套了一层wwwChecker
                    /// coroutineMono 也是为了能开启这个协程
                    /// </summary>
                    WWW www = new WWW(PathUtility.GetLocalUrl4WWW(streamingFilePath));
                    WWWRequestChecker wwwChecker = new WWWRequestChecker(www);
                    yield return coroutineMb.StartCoroutine(wwwChecker);
                    if (wwwChecker.IsError)
                    {
                        copyAllFileSuccess = false;
                        continue;
                    }

                    byte[] fileBytes = www.bytes;
                    if (compress)
                    {

                    }
                    else
                    {
                        PathUtility.CreateDirectory(Path.GetDirectoryName(persistentFilePath));

                        try
                        {
                            FileStream fs = File.Create(persistentFilePath);
                            fs.Write(fileBytes, 0, fileBytes.Length);
                            fs.Flush();
                            fs.Close();
                        }
                        catch (Exception e)
                        {
                            copyAllFileSuccess = false;
                            Debug.LogError(e);
                        }
                    }

                    wwwChecker.Dispose();
                    copyFileCount++;
                }

                bool executeSuccess = copyAllFileSuccess && (copyFileCount == filePathList.Count);
                if (executeSuccess)
                {
                    string persistentMarkFilePath = FileManager.GetPersistentDataPath(subPath) + Path.AltDirectorySeparatorChar + existMarkFile;
                    PathUtility.CreateDirectory(Path.GetDirectoryName(persistentMarkFilePath));

                    try
                    {
                        WriteFile(persistentMarkFilePath, newVersion.ToString());
                    }
                    catch (Exception e)
                    {
                        Debug.LogError(e);

                        bool reTryCountSuccess = false;
                        for (int i = 0; i < 3; i++)
                        {
                            Thread.Sleep(5 + 5 * i);

                            try
                            {
                                WriteFile(persistentMarkFilePath, newVersion.ToString());

                                reTryCountSuccess = true;
                                break;
                            }
                            catch (Exception ex)
                            {
                                Debug.LogException(ex);
                            }
                        }

                        if (!reTryCountSuccess)
                        {
                            copyAllFileSuccess = false;
                        }
                    }

                    if (!copyAllFileSuccess)
                    {
                        Debug.LogError("Execute file copy not success.");
                    }
                }
                else
                {
                    Debug.LogError("Execute file copy not success.");
                }
            }
            else
            {
                Debug.LogWarning("Version checked pass, not copy file from " + FileManager.GetStreamingAssetsPath(subPath) + "  to  " + FileManager.GetPersistentDataPath(subPath));
            }

            if (onFinishDelegate != null)
            {
                onFinishDelegate(copyAllFileSuccess);
            }
            yield return null;
        }

        public static void WriteFile(string path, string text)
        {
            FileStream fs = new FileStream(path, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            sw.Write(text);
            sw.Flush();
            fs.Close();
            fs.Close();
        }

    }
}

using System.Collections;
using System.IO;
using UnityEngine;

namespace LywGames
{
    public static class WWWUtility 
    {
        /** 1. 使用WWW获取网络或者本地文本、图片、音乐、AB包资源
         * Unity提供的WWW方法, 这个方法只能用来作读取, 主要是用在服务器上的, 可以从网络上下载图片、文件等其他信息, 给一个http链接即可。
         * 常用支持协议:(http:// https:// ftp:// file://)顾名思义最后一个可以用来读本地文件。
         * WWW读取一个本地文件, 前缀file:///, 例如: file:///C:/Users/liyangwei999/AppData/LocalLow/DefaultCompany/TestUnityPath/PersistentDataPath.txt。
         * 值得注意的是: Application.streamingAssetsPath的安卓路径是: jar:file:///data/app/com.lywgames.tup-1.apk!/assets 包含了file:///, 可以直接当本地文件路径使用WWW读取。
         **/
        public static void GetLocalTextureByWWW(string url, System.Action<Texture> onLoad, MonoBehaviour mb)
        {
            GetTextureByWWW(PathUtility.GetLocalFileUrl(url), onLoad, mb);
        }

        public static void GetTextureByWWW(string url, System.Action<Texture> onLoad, MonoBehaviour mb)
        {
            mb.StartCoroutine(GetTextureByWWWImp(url, onLoad));
        }

        public static void GetLocalSpriteByWWW(string url, System.Action<Sprite> onLoad, MonoBehaviour mb)
        {
            GetSpriteByWWW(PathUtility.GetLocalFileUrl(url), onLoad, mb);
        }

        public static void GetSpriteByWWW(string url, System.Action<Sprite> onLoad, MonoBehaviour mb)
        {
            System.Action<Texture> loadTexture = delegate (Texture texture)
            {
                Sprite sprite = null;

                if (texture != null)
                {
                    Texture2D texture2D = (Texture2D)texture;
                    Rect rect = new Rect(0, 0, texture.width, texture.height);
                    sprite = Sprite.Create(texture2D, rect, new Vector2(0.5f, 0.5f));
                }

                onLoad(sprite);
            };

            mb.StartCoroutine(GetTextureByWWWImp(url, loadTexture));
        }

        private static IEnumerator GetTextureByWWWImp(string url, System.Action<Texture> onLoad)
        {
            if (string.IsNullOrEmpty(url))
            {
                Debug.LogError("GetTextureByWWWImp URL is Empty.");
                yield return false;
            }

            WWW www = new WWW(url);
            yield return www;

            try
            {
                if (string.IsNullOrEmpty(www.error) == false)
                {
                    string errorLog = string.Format("Request {0} GetTextureByWWWImp Error:{1}", url, www.error);
                    Debug.LogError(errorLog);
                    onLoad(null);
                }
                else
                {
                    onLoad(www.texture);
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogError(ex.Message);
            }
        }

        public static void GetLocalAudioClipByWWW(string url, bool threeD, System.Action<AudioClip> onLoad, MonoBehaviour mb)
        {
            GetAudioClipByWWW(PathUtility.GetLocalFileUrl(url), threeD, onLoad, mb);
        }

        public static void GetAudioClipByWWW(string url, bool threeD, System.Action<AudioClip> onLoad, MonoBehaviour mb)
        {
            mb.StartCoroutine(GetAudioClipByWWWImp(url, threeD, onLoad));
        }

        private static IEnumerator GetAudioClipByWWWImp(string url, bool threeD, System.Action<AudioClip> onLoad)
        {
            if (string.IsNullOrEmpty(url))
            {
                Debug.LogError("GetAudioClipByWWWImp URL is Empty.");
                yield return false;
            }

            WWW www = new WWW(url);
            yield return www;

            try
            {
                if (string.IsNullOrEmpty(www.error) == false)
                {
                    string errorLog = string.Format("Request {0} GetAudioClipByWWWImp Error:{1}", url, www.error);
                    Debug.LogError(errorLog);
                    onLoad(null);
                }
                else
                {
                    onLoad(www.GetAudioClip(threeD));
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogError(ex.Message);
            }
        }

        public static void GetLocalTextByWWW(string url, System.Action<string> onLoad, MonoBehaviour mb)
        {
            GetTextByWWW(PathUtility.GetLocalFileUrl(url), onLoad, mb);
        }

        public static void GetTextByWWW(string url, System.Action<string> onLoad, MonoBehaviour mb)
        {
            mb.StartCoroutine(GetTextByWWWImp(url, onLoad));
        }

        private static IEnumerator GetTextByWWWImp(string url, System.Action<string> onLoad)
        {
            if (string.IsNullOrEmpty(url))
            {
                Debug.LogError("GetTextByWWWImp URL is Empty.");
                yield return false;
            }
            
            WWW www = new WWW(url);
            yield return www;
            
            try
            {
                if (string.IsNullOrEmpty(www.error) == false)
                {
                    string errorLog = string.Format("Request {0} GetTextByWWWImp Error:{1}", url, www.error);
                    Debug.LogError(errorLog);
                    onLoad(null);
                }
                else
                {
                    onLoad(www.text);
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogError(ex.Message);
            }
        }

        public static void GetLocalAbByWWW(string url, System.Action<AssetBundle> onLoad, MonoBehaviour mb)
        {
            GetAbByWWW(PathUtility.GetLocalFileUrl(url), onLoad, mb);
        }

        public static void GetAbByWWW(string url, System.Action<AssetBundle> onLoad, MonoBehaviour mb)
        {
            mb.StartCoroutine(GetAbByWWWImp(url, onLoad));
        }

        private static IEnumerator GetAbByWWWImp(string url, System.Action<AssetBundle> onLoad)
        {
            if (string.IsNullOrEmpty(url))
            {
                Debug.LogError("GetABByWWWImp URL is Empty.");
                yield return false;
            }

            WWW www = new WWW(url);
            yield return www;

            try
            {
                if (string.IsNullOrEmpty(www.error) == false)
                {
                    string errorLog = string.Format("Request {0} GetABByWWWImp Error:{1}", url, www.error);
                    Debug.LogError(errorLog);
                    onLoad(null);
                }
                else
                {
                    onLoad(www.assetBundle);
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogError(ex.Message);
            }
        }

        public static void GetNetAbObjByWWW(string url, System.Action<Object> onLoad, MonoBehaviour mb)
        {
            mb.StartCoroutine(GetAbByWWWImp(url, (ab) =>
            {
                if (ab != null)
                {
                    Object obj = ab.LoadAsset(PathUtility.GetFileNameWithoutExtension(url.Substring(url.LastIndexOf("/") + 1)));
                    ab.Unload(false);
                    ab = null;
                    onLoad(obj);
                }
            }));
        }

        public static IEnumerator GetFileImp(string url, string savePath, System.Action<string> onLoad)
        {
            if (string.IsNullOrEmpty(url))
            {
                Debug.LogError("GetFileImp URL is Empty.");
                yield return false;
            }

            WWW www = new WWW(url);
            yield return www;

            try
            {
                if (string.IsNullOrEmpty(www.error) == false)
                {
                    string errorLog = string.Format("Request {0} GetFileImp Error:{1}", url, www.error);
                    Debug.LogError(errorLog);
                    onLoad(errorLog);
                }
                else
                {
                    File.WriteAllBytes(savePath, www.bytes);
                    onLoad("download " + url + " to " + savePath + " success.");
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogError(ex.Message);
            }
        }

    }
}
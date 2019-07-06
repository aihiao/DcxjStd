using UnityEngine;

public class GameMain : MonoBehaviour
{
    /** 
    * 1. Application.dataPath 包含游戏数据文件夹的路径(apk的安装路径)
    *  PC: D:/GitHub/DcxjStd/TestUnityPath/Assets
    *  Android: /data/app/com.lywgames.tup-1.apk
    *  IOS: /Users/liyangwei/Library/Developer/CoreSimulator/Devices/174D0537-919D-4CAE-A989-4053A39F853B/data/Containers/Bundle/Application/AF3D0CEE-2941-4349-85C2-82E0B330C8C6/ProductName.app/Data
    *  
    * 2. Application.streamingAssetsPath 包含一个到StreamingAssets文件夹的路径(这路径运行时只能读取数据, 不能写数据)
    * PC: D:/GitHub/DcxjStd/TestUnityPath/Assets/StreamingAssets
    * Android: jar:file:///data/app/com.lywgames.tup-1.apk!/assets
    * IOS: /Users/liyangwei/Library/Developer/CoreSimulator/Devices/174D0537-919D-4CAE-A989-4053A39F853B/data/Containers/Bundle/Application/AF3D0CEE-2941-4349-85C2-82E0B330C8C6/ProductName.app/Data/Raw
    * 
    * 3. Application.temporaryCachePath 包含一个临时数据/缓存目录的路径
    * PC: C:/Users/LIYANG~1/AppData/Local/Temp/DefaultCompany/TestUnityPath
    * Android: /storage/emulated/0/Android/data/com.lywgames.tup/cache (外部私有缓存路径, APP卸载, 该路径下文件就不存在了)
    * IOS: /Users/liyangwei/Library/Developer/CoreSimulator/Devices/174D0537-919D-4CAE-A989-4053A39F853B/data/Containers/Data/Application/1F24D221-70C3-4B57-8F89-CD6083494688/Library/Caches
    * 
    * 4. Application.persistentDataPath 包含一个持久数据目录的路径
    * PC: C:/Users/liyangwei999/AppData/LocalLow/DefaultCompany/TestUnityPath
    * Android: /storage/emulated/0/Android/data/com.lywgames.tup/files (外部私有持久路径, APP卸载, 该路径下文件就不存在了)
    * IOS: /Users/liyangwei/Library/Developer/CoreSimulator/Devices/174D0537-919D-4CAE-A989-4053A39F853B/data/Containers/Data/Application/1F24D221-70C3-4B57-8F89-CD6083494688/Documents
    * 
    * 5. IOS APP的独立数据存储目录下有三个文件夹: Documents, Library和tmp。
    * (1). Documents目录,这个目录用于存储需要长期保存的数据, 比如我们的热更新内容就写在这里。需要注意的是, iCloud会自动备份此目录, 如果此目录下写入的内容较多, 审核的可能会被苹果拒掉。
    * (2). Library目录, 这个目录下有两个子目录, Caches和Preferences。 
    * (2.1). Caches是一个相对临时的目录, 适合存放下载缓存的临时文件, 空间不足时可能会被系统清除, Application.temporaryCachePath返回的就是此路径。我把热更新的临时文件写在这里, 等一个版本的所有内容更新完全后, 再把内容转移到Documents目录。 
    * (2.2). Preferences用于应用存储偏好设置, 用NSUserDefaults读取或设置。
    * (3). tmp目录, 临时目录, 存放应用运行时临时使用的数据。 
    * */
    void Start ()
    {
        DataPath();
        StreamingAssetsPath();
        PersistentDataPath();
        TemporaryCachePath();
    }

    public string DataPath()
    {
        string dataPath = Application.dataPath;
        Debug.LogError("---dataPath = " + dataPath);
        return dataPath;
    }

    public string StreamingAssetsPath()
    {
        string streamingAssetsPath = Application.streamingAssetsPath;
        Debug.LogError("---streamingAssetsPath = " + streamingAssetsPath);
        return streamingAssetsPath;
    }

    public string PersistentDataPath()
    {
        string persistentDataPath = Application.persistentDataPath;
        Debug.LogError("---persistentDataPath = " + persistentDataPath);
        return persistentDataPath;
    }

    public string TemporaryCachePath()
    {
        string temporaryCachePath = Application.temporaryCachePath;
        Debug.LogError("---temporaryCachePath = " + temporaryCachePath);
        return temporaryCachePath;
    }

}

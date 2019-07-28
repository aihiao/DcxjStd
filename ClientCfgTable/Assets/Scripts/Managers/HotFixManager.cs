using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using LywGames;

/// <summary>
/// 热更新管理器,这个类只能用于热更新下载文件用，不要用于其他文件的下载
/// 可以支持同时下载多个任务，最大任务数MAX_DOWNLOAD_TASK控制
/// 使用之前，一定要调用一下InitializeManager，清空缓存。
/// 填充完下载列表后，要调用一下SetDownloadListDone，表示本次下载任务已经全部添加到下载列表
/// </summary>
public class HotFixManager : AbsManager<HotFixManager>
{
	private List<DownloadTask> downloadingTasks = new List<DownloadTask>();	//正在下载的任务列表
	private List<DownloadTask> cacheTasks = new List<DownloadTask>();		//缓存下载队列
	private List<DownloadTask> downloadedTasks = new List<DownloadTask>();	//已经下载好的任务

	private readonly int MAX_DOWNLOAD_TASK = 5;	//允许下载的最大任务额度
	private bool IsBusy { get { return downloadingTasks.Count >= MAX_DOWNLOAD_TASK; } }	//如果正在下载的文件数量大于设置的上限额度则表示繁忙

	private bool setDownloadListDone = false;

	public Action downloadTaskDoneCallback = null;	//下载完成后的回调

	private bool hasInitAssetbundleManager = false;

	private bool enableUpdate = true;

	/// <summary>
	/// 用这个下载器的时候需要先把缓存清除掉
	/// </summary>
	public void InitializeManager()
	{
		downloadingTasks.Clear();
		cacheTasks.Clear();
		downloadedTasks.Clear();
		downloadTaskDoneCallback = null;
		hasInitAssetbundleManager = false;
		setDownloadListDone = false;
	}

	/// <summary>
	/// 下载文件
	/// </summary>
	/// <param name="url">文件网址</param>
	/// <param name="fileName">文件名字</param>
	/// <param name="needCompress">是否需要解压，true为需要</param>
	public void DownloadFile(string url, string fileName, bool needCompress, string savePath, DownloadTask.OnDownloadTaskEnd callback = null)
	{
		enableUpdate = true;
		if (IsBusy == false)	//如果下载数目没达到上限，则添加下载任务
		{
			DownloadTask task = new DownloadTask(url, fileName, needCompress, savePath, callback);
			task.StartDownload();
			downloadingTasks.Add(task);
		}
		else					//否则，添加到下载队列中
			cacheTasks.Add(new DownloadTask(url, fileName, needCompress, savePath, callback));
	}
	
	public void Update()
	{
		if (enableUpdate == false)
			return;

		for (int i = 0; i < downloadingTasks.Count; i++)
		{
			DownloadTask.DownloadStatus status = downloadingTasks[i].GetDownloadStatus();
			if (status == DownloadTask.DownloadStatus.DOWNLOADED)
			{
				downloadedTasks.Add(downloadingTasks[i]);
				downloadingTasks.RemoveAt(i);
				break;
			}
			else if(status == DownloadTask.DownloadStatus.COMPLETEFAILED)
			{
				enableUpdate = false;
				RequestManager.Instance.EnableInput = true;
				UiManager.Instance.ShowByName(UiPrefabNames.UiPnlMessage);
				UiManager.Instance.GetUi<UiPnlMessage>().Set("下载失败，请重新下载！", UiDialogBtn.Ok | UiDialogBtn.Close,
				(button, data) =>
				{
					if (button == AlertBtnType.Ok)
					{
						ClearAllTask();
						LoggerManager.Instance.Info("Download file failed, init game");
						ReConnectManager.Instance.HandleNetStateNotOk();
					}
					
				});
			}
		}

		if (IsBusy == true)	//如果繁忙状态，说明已经达到了最大的下载数量，不再添加任务
			return;


		if (setDownloadListDone && cacheTasks.Count == 0 && downloadingTasks.Count == 0 && downloadTaskDoneCallback != null)
		{
			// 上边这个判断条件太脆了， 在没有下载任务但是还没有完成全部下载的时候也在update？ 只是依靠 downloadTaskDoneCallback来区分这两种情况吗?
			// ----确实有可能在添加的过程中就会执行到此处，所以添加了一个标志量，全部添加完成下载任务的时候，需要设置一下下载任务完成SetDownloadListDone == true
			// 下载完成后需要重新加载asset menifest
			if (!hasInitAssetbundleManager)
			{
				Debug.LogWarning("Do Reload AssetBundleManager!");
				AssetBundleManager.UnloadAll();
				AssetBundleManager.IsInitialized = false;
				AssetBundleManager.Reset();
				GlobalManager.Instance.Add<AssetBundleManager>().StartCoroutine(AssetBundleManager.Instance.InitializeAsync());
				hasInitAssetbundleManager = true;
			}

			if (hasInitAssetbundleManager && AssetBundleManager.IsInitialized)
			{
				if (downloadTaskDoneCallback != null)
					downloadTaskDoneCallback.Invoke();
			}
			//enableUpdate = false;
		}
		if (cacheTasks.Count == 0)	//如果下载队列中数量为0，说明没有缓存下载任务
			return;

		cacheTasks[0].StartDownload();			//从缓存中再开始一个下载任务
		downloadingTasks.Add(cacheTasks[0]);	//缓存队列中有缓存，那么添加到下载列表中，然后在缓存中移除记录
		cacheTasks.RemoveAt(0);

	}

	/// <summary>
	/// 填充下载列表完毕的时候调用一下，调用完成这个后，不要再向列表中添加新的任务，以保证有效的回调函数执行
	/// 重新启用这个下载管理器的时候，需要调用InitializeManager。强制的使用规则
	/// </summary>
	public void SetDownloadListDone()
	{
		setDownloadListDone = true;
	}

	private void ClearAllTask()
	{
		downloadingTasks.Clear();
		cacheTasks.Clear();
		downloadedTasks.Clear();
	}

	public string GetDownloadProgress(int maxCount, out int currentCount)
	{
		currentCount = Math.Min(downloadedTasks.Count, maxCount);
		return string.Format("{0}/{1}", currentCount, maxCount);
	}

	public struct FileInfo
	{
		private string url;
		private string fileName;
		private bool needCompress;
		private string savePath;
		public string URL { get { return url; } }
		public string FileName { get { return fileName; } }
		public bool NeedCompress { get { return needCompress; } }
		public string SavePath { get { return savePath; } }
		public FileInfo(string url, string fileName, bool needCompress, string savePath)
		{
			this.url = url;
			this.fileName = fileName;
			this.needCompress = needCompress;
			this.savePath = savePath;
		}

		public override string ToString()
		{
			return string.Format("File info is : url-> {0} , fileName-> {1} , needComparess-> {2} , savePath-> {3} ,", URL, FileName, NeedCompress, SavePath);
		}

	}

	/// <summary>
	/// 下载任务管理器，管理每个下载任务，查看进度等
	/// </summary>
	public class DownloadTask
	{
		public DownloadTask(FileInfo fileInfo, OnDownloadTaskEnd callback = null)
		{
			this.fileInfo = fileInfo;
			DownloadEndCallback = callback;
		}

		public DownloadTask(string url, string fileName, bool needCompress,string savePath, OnDownloadTaskEnd callback = null)
		{
			this.fileInfo = new FileInfo(url, fileName, needCompress, savePath);
			DownloadEndCallback = callback;
		}

		public delegate void OnDownloadTaskEnd(string url);
		public OnDownloadTaskEnd DownloadEndCallback { get; set; }

		private FileInfo fileInfo = new FileInfo();
		public FileInfo Info { get { return fileInfo; } }	//只读，外部不可修改下载任务中的文件信息

		private int downloadTimeCounter = 0;			//下载次数计数器，超过MAX_DOWNLOAD_COUNT后就记做彻底失败
		private readonly int MAX_DOWNLOAD_COUNT = 5;	//最大的尝试下载的次数

		private WWWRequestChecker www = null;

		public void StartDownload()
		{
            LoggerManager.Instance.Info(fileInfo.ToString());
			downloadTimeCounter++;
			if (downloadTimeCounter <= MAX_DOWNLOAD_COUNT && www == null)//加上www == null判断，防止重复下载
				www = new WWWRequestChecker(fileInfo.URL);
		}

		/// <summary>
		/// 这个方法主要是想暴露给外部知道当前的下载进度。类内部通过Update()调用，捕获www的状态然后相应的保存或者其他的操作
		/// </summary>
		/// <returns></returns>
		public DownloadStatus GetDownloadStatus()
		{
			if (downloadTimeCounter > MAX_DOWNLOAD_COUNT)
				return DownloadStatus.COMPLETEFAILED;

			if (www.IsDone())
			{
				if (www.IsError == false)
				{
					if (fileInfo.NeedCompress)
					{
						string path = FileManager.GetPersistentDataPath(fileInfo.SavePath);
						string fullPath = string.Empty;
						if(path.EndsWith("/"))
							fullPath = path + fileInfo.FileName;
						else
							fullPath = path + "/" + fileInfo.FileName;
						
						if(ZipTool.Decompress(www.GetWWW().bytes, fullPath, false) == false)//如果解压失败
						{
							Debug.LogError(fileInfo.URL + " download failed, trying download again. downloadTimeCounter : " + downloadTimeCounter + "\n " + www.GetWWW().error);
							www = null;
							StartDownload();
							return DownloadStatus.FAILED;
						}
					}
					else
						SaveByteToFile();

					return DownloadStatus.DOWNLOADED;
				}
				else
				{
					Debug.LogError(fileInfo.URL + " download failed, trying download again. downloadTimeCounter : " + downloadTimeCounter + "\n " + www.GetWWW().error);
					www = null;	//类内部调用StartDownload方法要确保每次都是新开辟的www
					StartDownload();
					
					return DownloadStatus.FAILED;
				}
			}
			else
				return DownloadStatus.DOWNLOADING;
		}

		private bool downloadSuccess = false;
		public bool DownloadSuccess { get { return downloadSuccess; } }	//只有文件保存好了以后，才会标识下载成功，否则什么情况都有可能发生，不能保证下载成功

		private void SaveByteToFile()
		{
			string path = FileManager.GetPersistentDataPath(fileInfo.SavePath);
			string fullPath = string.Empty;
			if (path.EndsWith("/"))
				fullPath = path + fileInfo.FileName;
			else
				fullPath = path + "/" + fileInfo.FileName;

			if (Directory.Exists(path) == false)
				Directory.CreateDirectory(path);

			//处理文件这很容易出现问题，所以用下try捕捉下错误
			try
			{
				ClientCommon.ConfigDataBase.Instance.DbAccessorFactory.ReleaseAll();
				ClientCommon.ConfigDataBase.Instance.ReleaseAll(true);

				Stream stream = File.Create(fullPath);
				stream.Write(www.GetWWW().bytes, 0, www.GetWWW().bytes.Length);
				stream.Close();

				www.Dispose();	//下载成功，释放内存

				if (DownloadEndCallback != null)
					DownloadEndCallback(fileInfo.URL);
					
				downloadSuccess = true;
				
			}
			catch (Exception e)
			{
				Debug.LogError("Save file error occured :" + e.ToString() + "\n trying download again. " + downloadTimeCounter);
				www = null;
				StartDownload();
			}
		}

		public enum DownloadStatus
		{
			UNKNOWN = 0,
			DOWNLOADING,	//正在下载
			DOWNLOADED,		//下载完成
			FAILED,			//下载失败，可以重试下载
			COMPLETEFAILED	//彻底失败（尝试重下n次以后还不行）
		}
	}
}

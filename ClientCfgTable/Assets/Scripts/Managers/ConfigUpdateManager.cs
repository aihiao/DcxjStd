using System.IO;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClientCommon;

public class ConfigUpdateManager : AbsManager<ConfigUpdateManager>
{
    public enum UpdatePeriod
    {
        UnKnown = 0,
        Login = 1,
        GamePlay = 2,
    }

    private List<string> urls = new List<string>();
    private UiPnlMessage showingDlg = null;

    private UpdatePeriod updateTime = UpdatePeriod.UnKnown;
    public UpdatePeriod UpdateTime
    {
        get { return updateTime; }
        set { updateTime = value; }
    }

    private BaseRequest preRequest = null;
    public BaseRequest PreRequest
    {
        get { return preRequest; }
        set { preRequest = value; }
    }

    public void AddUrls(List<string> newUrls)
    {
        if (newUrls == null || newUrls.Count <= 0)
        {
            return;
        }

        for (int index = 0; index < newUrls.Count; index++)
        {
            if (urls.Contains(newUrls[index]))
            {
                continue;
            }

            urls.Add(newUrls[index]);
        }
    }

    public override void OnUpdate()
    {
        if (urls.Count > 0 && showingDlg == null)
        {
            UiManager.Instance.ShowByName(UiPrefabNames.UiPnlMessage);
            showingDlg = UiManager.Instance.GetUi<UiPnlMessage>();
            showingDlg.Set(ConfigDataBase.TextConfig.Get("UIConfigNeedUpdate").Content, UiDialogBtn.Ok, OnDlgBtnClick, false);
        }
    }

    private void OnDlgBtnClick(AlertBtnType abt, object value)
    {
        if (abt == AlertBtnType.Ok)
        {
            showingDlg.OKBtn.gameObject.SetActive(false);
            StartCoroutine("DownloadConfig");
        }
    }

    [Obfuscation(Exclude = true, Feature = "renaming")]
    private IEnumerator DownloadConfig()
    {
        Debug.Log("start download");
        ConfigDataBase.Instance.ReleaseAll(true);

        while (urls.Count > 0)
        {
            WWW www = new WWW(urls[0]);
            yield return www;
            Debug.Log(www.url + " done !");

            string tbName = GetTBNameByUrl(urls[0]);
            string fileName = ConfigDataBase.Instance.GetDbNameByTableName(tbName) + "." + Defines.ConfigFileExtension;
            string filePath = ConfigDataBase.Instance.GetTbPath(tbName) + "/" + fileName;

            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));

            Stream sw = File.Open(filePath, FileMode.Truncate);
            sw.Write(www.bytes, 0, www.bytes.Length);
            sw.Close();

            urls.RemoveAt(0);
        }

        OnDownloadComplete();
    }

    private void OnDownloadComplete()
    {
        showingDlg.Hide();
        showingDlg = null;
    }

    private string GetTBNameByUrl(string url)
    {
        return (url.Split('-'))[1];
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Effect system to provide effect in menu or battle. 
/// </summary>
public class FxManager : AbsManager<FxManager>
{
    private float tmSclDrt; // Time scale duration.
    private bool tmScl; // Flag of time scale.

    public override void OnUpdate()
    {
        UpdateTimeScale();
    }

    #region FxPool Management
    /// <summary>
    /// 如果想在创建后更改位置（一般目的是在指定位置播放声音），则应该调用CreateFxNonStart。
    /// </summary>
    public FXController CreateFx(string filePath)
    {
        FXController fx = CreateFxNonStart(filePath);
        if (fx)
            fx.Start();

        return fx;
    }

    /// <summary>
    /// 使用这个接口创建的特效无法自动播放第二次，第二次播放需啊哟手动调用.Start，慎用
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    public FXController CreateFxNonStart(string filePath)
    {
        GameObject go = PoolManager.Instance.Spawm(filePath, ClientCommon.AssetType.PFX, ClientCommon.AssetType.PFX.ToString());
        if (AssertHelper.Check(go != null, filePath + " not exist!") == false)
            return null;

        FXController fx = go.GetComponent<FXController>();

        if (fx != null)
            fx.GetComponent<FXController>().ResetFX(false);

        return fx;
    }

    public void StartFx(UnityEngine.GameObject fxObject)
    {
        if (fxObject.GetComponent<FXController>())
        {
            fxObject.GetComponent<FXController>().ResetFX(false);
            fxObject.GetComponent<FXController>().Start();
        }
    }

    public void StartFx(FXController fxController)
    {
        if (fxController)
            fxController.Start();
    }

    //TODO:需要添加参数控制是否立即播放，这关系到声音在什么位置出现
    public FXController CreateFxAndBuildParent(string filePath)
    {
        GameObject obj = PoolManager.Instance.Spawm(filePath, ClientCommon.AssetType.PFX, ClientCommon.AssetType.PFX.ToString());

        // obj.name = System.IO.Path.GetFileName(filePath); // 注意，不能更改名字
        FXController fx = obj.GetComponent<FXController>();
        if (fx == null)
        {
            Transform parentTrans = new GameObject(obj.name).transform;
            ObjectUtility.AttachToParentAndResetLocalPosAndRotation(parentTrans, obj.transform);

            // Reset root transform
            parentTrans.localPosition = Vector3.zero;
            parentTrans.localRotation = Quaternion.identity;
            parentTrans.localScale = Vector3.one;

            fx = parentTrans.gameObject.AddComponent<FXController>();
            fx.loop = true;
            fx.autoDestroy = true;

            obj = parentTrans.gameObject;
        }

        if (fx)
            fx.Start();
        return fx;
    }
    #endregion

    #region Time scaler
    // Scale game time.
    public void ScaleTime(float scale, float duration)
    {
        if (scale < 0 || duration < 0)
            return;

        tmScl = true;
        Time.timeScale = scale;
        tmSclDrt = Time.realtimeSinceStartup + duration;
    }

    // Resume game time.
    public void ResumeTimeScale()
    {
        tmSclDrt = 0;
    }

    public float GetScaleTime()
    {
        return Time.timeScale;
    }

    private void UpdateTimeScale()
    {
        if (tmScl)
        {
            if (tmSclDrt < Time.realtimeSinceStartup)
            {
                Time.timeScale = 1.0f;
                tmScl = false;
            }
        }
    }
    #endregion

    public FXController PlayFX(string fxName, GameObject parent, bool attachedToParent, bool autoDestroy, bool isResetLocalPos, bool startImmediately, bool setLayerAsParent = true)
    {
        //	string fxPath = PathUtility.Combine(baseFxPath, fxName);
        FXController fx = CreateFxNonStart(fxName);

        //	AssertHelper.Check(fx != null, fxName + " not exist!");

        if (fx == null)
            return null;

        // Attach to parent
        if (attachedToParent)
        {
            if (isResetLocalPos)
                ObjectUtility.AttachToParentAndResetLocalPosAndRotation(parent.transform, fx.Root);
            else
                ObjectUtility.AttachToParentAndKeepLocalTrans(parent.transform, fx.Root);
        }
        else
        {
            if (isResetLocalPos)
                ObjectUtility.UnifyWorldTrans(parent.transform, fx.Root);
        }

        // Set layer
        if (setLayerAsParent)
            ObjectUtility.SetObjectLayer(fx.gameObject, parent.layer);

        if (autoDestroy)
        {
            // Set auto destroy flag to FX script.
            FXController pfxScp = fx.GetComponentInChildren<FXController>();

            if (pfxScp != null)
            {
                pfxScp.autoDestroy = true;
                pfxScp.loop = false;
            }
        }

        if (startImmediately)
            fx.Start();

        return fx;
    }

}

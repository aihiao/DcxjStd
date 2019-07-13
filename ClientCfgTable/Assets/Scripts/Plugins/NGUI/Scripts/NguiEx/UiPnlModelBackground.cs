using UnityEngine;

public class UiPnlModelBackground : MonoBehaviour
{
    private static int modelBackgroundShowingCount = 0;
    public static int ModelBackgroundShowingCount
    {
        get { return modelBackgroundShowingCount; }
    }

    public static bool IsModelShowing
    {
        get { return modelBackgroundShowingCount > 0; }
    }

    public UISprite spriteBack;

    private void Start()
    {
        if (spriteBack != null)
        {
            UIRoot root = GameObject.FindObjectOfType<UIRoot>();
            if (root != null)
            {
                float s = (float)root.activeHeight / Screen.height;
                int height = Mathf.CeilToInt(Screen.height * s);
                int width = Mathf.CeilToInt(Screen.width * s);
                spriteBack.width = width;
                spriteBack.height = height;
            }
        }
    }

    private void OnEnable()
    {
        modelBackgroundShowingCount++;
        BackgroundMainCameraHider.DisableShow();
    }

    private void OnDisable()
    {
        modelBackgroundShowingCount--;
        BackgroundMainCameraHider.ReEnableShow();
    }

}

public class BackgroundMainCameraHider
{
    private static float backUpfarClipPlane = 200f;
    public static void DisableShow()
    {
        var cam = Camera.main;
        if (cam != null)
        {
            if (cam.farClipPlane > 1f)
            {
                backUpfarClipPlane = cam.farClipPlane;
            }
            cam.farClipPlane = 0.4f;
        }
    }

    public static void ReEnableShow()
    {
        var cam = Camera.main;
        if (cam != null)
        {
            cam.farClipPlane = Mathf.Max(backUpfarClipPlane, 200f);
        }
    }

}

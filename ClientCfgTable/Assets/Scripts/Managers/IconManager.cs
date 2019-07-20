#define DELAY_LOAD_ICON_TEXTUER

using System.Collections.Generic;
using UnityEngine;
using ClientCommon;

public class IconManager : AbsManager<IconManager>
{
    public class TextureData
    {
        public string textureName;
        public int referenceCount;
        public Texture texture;
    }

    private Dictionary<string, TextureData> textureDic = new Dictionary<string, TextureData>();

    public bool IsIconTextureLoaded(string textureName)
    {
        return textureDic.ContainsKey(textureName) ? true : false;
    }

    public bool SetUpControlIcon(UITexture texture, string textureName)
    {
        if (texture == null)
        {
            return false;
        }
        if (string.IsNullOrEmpty(textureName))
        {
            texture.mainTexture = null;
            return false;
        }

        SetUpIcon(texture, textureName, AssetType.Icon);

        return true;
    }

    public bool SetAvatarIconById(UITexture texture, int id)
    {
        MeshAssembly mesh = ConfigDataBase.MeshAssemblyConfig.Get(id);
        if (mesh == null)
        {
            LoggerManager.Instance.Error("Can not find mesh assembly in config data base, id is " + id);
            return false;
        }
        if (string.IsNullOrEmpty(mesh.Icon))
        {
            LoggerManager.Instance.Error("Can not find Icon in mesh assembly db, id is " + id);
            return false;
        }

        return SetUpControlIcon(texture, mesh.Icon);
    }

    public bool SetUpIcon(UITexture texture, string textureName, int assetType)
    {
        if (texture == null || string.IsNullOrEmpty(textureName))
        {
            return false;
        }

        string lowerName = textureName.ToLower();
        if (!textureDic.ContainsKey(lowerName))
        {
#if DELAY_LOAD_ICON_TEXTUER
			LoadIconTexture((int)assetType, textureName);
			if (textureDic.ContainsKey(lowerName) == false)
			{
                LoggerManager.Instance.Error("can't load texture {0}", textureName); ;
				return false;
			}
#else
            return false;
#endif
        }
        if (texture == null)
        {
            LoggerManager.Instance.Error("can't load texture {0}", textureName);
            return false;
        }

        // Save old icon for release
        Texture oldMat = texture.mainTexture;

        // Setup new icon
        texture.mainTexture = textureDic[lowerName].texture;

        // Add reference count
        textureDic[lowerName].referenceCount++;

        // Release old icon
        ReleaseIcon(oldMat);

        return true;
    }

    public void LoadIconTexture(int assetType, string textureName)
    {
        string lowerName = textureName.ToLower();
        if (textureDic.ContainsKey(lowerName))
        {
            return;
        }

        Texture texture = ResourceManager.Instance.LoadAsset<Texture2D>(assetType, lowerName, true);

        TextureData materialData = new TextureData();
        materialData.textureName = lowerName;
        materialData.referenceCount = 0;
        materialData.texture = texture;
        if (texture == null)
        {
            LoggerManager.Instance.Error("-_-没有加载到图片 : " + textureName);
            UnityEngine.Object obj = ResourceManager.Instance.LoadAsset(assetType, lowerName, true, true);
            if (obj == null)
            {
                LoggerManager.Instance.Error("-_-没有加载到图片 2nd!!! : " + textureName);
            }
            else
            {
                texture = obj as Texture;
                LoggerManager.Instance.Error("obj is not null ");
                if (texture == null)
                {
                    LoggerManager.Instance.Error("texture is null ");
                }
                else
                {
                    LoggerManager.Instance.Error("texture is not null ");
                }
            }
        }
        if (texture != null)
        {
            textureDic.Add(lowerName, materialData);
        }
    }

    public void ReleaseIcon(Texture texture)
    {
        foreach (var kvp in textureDic)
        {
            if (kvp.Value.texture == texture)
            {
                kvp.Value.referenceCount--;
                break;
            }
        }
    }

    public void ReleaseIcon(string textureName)
    {
        foreach (var kvp in textureDic)
        {
            if (kvp.Key == textureName)
            {
                kvp.Value.referenceCount--;
                break;
            }
        }
    }

    public void DestroyUnUseIcon()
    {
        List<string> deletingMaterials = new List<string>();
        foreach (var kvp in textureDic)
        {
            if (kvp.Value.referenceCount == 0)
            {
                deletingMaterials.Add(kvp.Key);
            }
        }
        foreach (var name in deletingMaterials)
        {
            TextureData textureData = textureDic[name];
            Resources.UnloadAsset(textureData.texture);
            textureDic.Remove(name);
        }

#if UNITY_EDITOR
        if (deletingMaterials.Count != 0)
        {
            LoggerManager.Instance.Info("Destroy unused icon : {0}", deletingMaterials.Count);
        }
#endif
    }

    public void DisposeAll()
    {
        foreach (var kvp in textureDic)
        {
            Resources.UnloadAsset(kvp.Value.texture);
        }
        textureDic.Clear();
    }

}
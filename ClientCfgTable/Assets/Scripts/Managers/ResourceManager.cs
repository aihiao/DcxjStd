using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : AbsManager<ResourceManager>
{

    public T InstantiateAsset<T>(int assetType, string filePath, bool cache = false) where T : UnityEngine.Object
    {
        return null as T;
    }
}

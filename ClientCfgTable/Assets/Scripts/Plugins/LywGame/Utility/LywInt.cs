public struct LywInt
{
    static int key = 38796541;
    int mValue;

    public static void SetKey(int genKey)
    {
        key = genKey;
    }

#if UNITY_EDITOR
    int debugValue;
    int initedFlag;// = false;
#endif

    LywInt(int v)
    {
        mValue = v ^ key;
#if UNITY_EDITOR
        debugValue = v;
        initedFlag = int.MaxValue;
#endif
    }

    public static implicit operator int(LywInt v)
    {
        int res = v.mValue ^ key;
#if UNITY_EDITOR
        if (v.debugValue != res && v.initedFlag == int.MaxValue)
            LoggerManager.Instance.Error(string.Format("value incurrect,  orignal:{0}  mixed:{1}  res:{2} key:{3}", v.debugValue, v.mValue, res, key));
#endif
        return res;
    }

    public static implicit operator LywInt(int v)
    {
        return new LywInt(v);
    }
}

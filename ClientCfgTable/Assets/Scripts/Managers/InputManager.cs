using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 输入事件监听器, 每个事件的意义参照EasyTouch说明
/// </summary>
abstract class InputHandler
{
    public enum EInputHandlePriority
    {
        FIRST  = 1,
        COMMON = 100,
        LAST   = 999,
    };
    /// <summary>
    /// 优先级, 值约小越优先,比如 1 优先于 12, 如果不关心他的处理顺序请填LAST
    /// </summary>
    public abstract int Priority
    {
        get;
    }  
	public virtual void OnCancel(Gesture gesture) { }
	public virtual void OnTouchStart(Gesture gesture) { }

	/// <returns>true 表示不继续向其他lisnter传递</returns>
	public virtual bool OnTouchDown(Gesture gesture) { return false; }
	
    /// <summary>
    /// 
    /// </summary>
    /// <param name="gesture"></param>
    /// <returns>true 表示不继续向其他lisnter传递</returns>
    public virtual bool OnTouchUp(Gesture gesture) { return false;}
	public virtual void OnSimpleTap(Gesture gesture) { }
	public virtual void OnDoubleTap(Gesture gesture) { }
	public virtual void OnLongTapStart(Gesture gesture) { }
	public virtual void OnLongTap(Gesture gesture) { }
	public virtual void OnLongTapEnd(Gesture gesture) { }
	public virtual void OnDragStart(Gesture gesture) { }
	public virtual void OnDrag(Gesture gesture) { }
	public virtual void OnDragEnd(Gesture gesture) { }
	public virtual void OnSwipeStart(Gesture gesture) { }
	public virtual void OnSwipe(Gesture gesture) { }
	public virtual void OnSwipeEnd(Gesture gesture) { }
	public virtual void OnTouchStart2Fingers(Gesture gesture) { }
	public virtual void OnTouchDown2Fingers(Gesture gesture) { }
	public virtual void OnTouchUp2Fingers(Gesture gesture) { }
	public virtual void OnSimpleTap2Fingers(Gesture gesture) { }
	public virtual void OnDoubleTap2Fingers(Gesture gesture) { }
	public virtual void OnLongTapStart2Fingers(Gesture gesture) { }
	public virtual void OnLongTap2Fingers(Gesture gesture) { }
	public virtual void OnLongTapEnd2Fingers(Gesture gesture) { }
	public virtual void OnTwist(Gesture gesture) { }
	public virtual void OnTwistEnd(Gesture gesture) { }
	public virtual void OnPinchIn(Gesture gesture) { }
	public virtual void OnPinchOut(Gesture gesture) { }
	public virtual void OnPinchEnd(Gesture gesture) { }
	public virtual void OnDragStart2Fingers(Gesture gesture) { }
	public virtual void OnDrag2Fingers(Gesture gesture) { }
	public virtual void OnDragEnd2Fingers(Gesture gesture) { }
	public virtual void OnSwipeStart2Fingers(Gesture gesture) { }
	public virtual void OnSwipe2Fingers(Gesture gesture) { }
	public virtual void OnSwipeEnd2Fingers(Gesture gesture) { }
}

/// <summary>
/// 输入管理器, 支持用户自定义输入处理, 基于EasyTouch支持nGUI输入事件过滤, 被nGUI捕获的输入不会通知到用户处理器, 
/// </summary>
class InputManager : AbsManager<InputManager>
{
	private EasyTouch easyTouch = null;
	private List<InputHandler> inputListenerList = new List<InputHandler>();

	/// <summary>
	/// 初始化, 需要输入定义好的EasyTouch预设实体路径
	/// </summary>
	/// <param name="parameters"></param>
	public override void Initialize(params object[] parameters)
	{
		string easyTouchObjectPath = parameters[0] as string;
		var easyTouchObject = ResourceManager.Instance.InstantiateAsset<GameObject>(ClientCommon.AssetType.Other, "EasyTouch");
		if (easyTouchObject == null)
			return;

		easyTouch = easyTouchObject.GetComponent<EasyTouch>();
		if (easyTouch == null)
			return;

		GameObject.DontDestroyOnLoad(easyTouchObject);
		RegisterInputDelegates();
	}

	public override void Dispose()
	{
		UnregisterInputDelegates();
		Object.Destroy(easyTouch.gameObject);
	}

	public void SetInputEnable(bool enable)
	{
		easyTouch.enable = enable;
		UiShell.Instance.ActiveCameraEvent(enable);
	}
	
	public void SetSceneInputEnable(bool enable)
	{
		easyTouch.enable = enable;
	}


    /// <summary>
    /// 注册输入时间处理器
    /// </summary>
    public void RegisterInputListener(InputHandler listener)
	{
		if (inputListenerList.Contains(listener))
			return;

        bool inserted = false;
        for (int i = 0; i < inputListenerList.Count; i++)
        {
            if (inputListenerList[i].Priority >= listener.Priority)
            {
                inputListenerList.Insert(i, listener);
                inserted = true;
                break;
            }
        }
        if (!inserted)
            inputListenerList.Add(listener);
	}

	/// <summary>
	/// 注销输入时间处理器
	/// </summary>
	public void UnregisterInputListener(InputHandler listener)
	{
		inputListenerList.Remove(listener);
	}

	#region InterceptiveCamera Support
	/// <summary>
	/// 增加可以拦截输入事件的摄像机, 如果输入可以捕获到指定摄像机下面的collider, 这个输入事件就不会传递到管理器中
	/// 开启|关闭拦截输入功能
	/// </summary>
	public void EnableInterceptiveCamera(bool enable)
	{
		easyTouch.enabledNGuiMode = enable ? easyTouch.nGUICameras.Count != 0 : false;
	}

	/// <summary>
	/// 增加可以拦截输入的摄像机
	/// </summary>
	public void AddInterceptiveCamera(Camera camera)
	{
		if (easyTouch.nGUICameras.Contains(camera) == false)
			easyTouch.nGUICameras.Add(camera);
		easyTouch.enabledNGuiMode = true;
	}	

	/// <summary>
	/// 删除可以拦截输入的摄像机	
	/// </summary>
	public void RemoveInterceptiveCamera(Camera camera)
	{
		easyTouch.nGUICameras.Remove(camera);
		easyTouch.enabledNGuiMode = easyTouch.nGUICameras.Count != 0;
	}

	/// <summary>
	/// 清除已设置的所有拦截输入摄像机
	/// </summary>
	public void ClearInterceptiveCamera()
	{
		easyTouch.nGUICameras.Clear();
		easyTouch.enabledNGuiMode = false;
	}

	/// <summary>
	/// 设置拦截摄像机可以捕获的Layer层
	/// </summary>
	public void SetInterceptiveCameraLayerMask(int layerMaskValue)
	{
		/// TODO : 是否需要对每个摄像机单独设置LayerMask
		easyTouch.nGUILayers.value = layerMaskValue;
	}

	/// <summary>
	/// 获取拦截摄像机可以捕获的Layer层
	/// </summary>
	public int GetInterceptiveCameraLayerMask()
	{
		return easyTouch.nGUILayers.value;
	}
	#endregion

	private void RegisterInputDelegates()
	{
		EasyTouch.On_Cancel += OnCancel;
		EasyTouch.On_TouchStart += OnTouchStart;
		EasyTouch.On_TouchDown += OnTouchDown;
		EasyTouch.On_TouchUp += OnTouchUp;
		EasyTouch.On_SimpleTap += OnSimpleTap;
		EasyTouch.On_DoubleTap += OnDoubleTap;
		EasyTouch.On_LongTapStart += OnLongTapStart;
		EasyTouch.On_LongTap += OnLongTap;
		EasyTouch.On_LongTapEnd += OnLongTapEnd;
		EasyTouch.On_DragStart += OnDragStart;
		EasyTouch.On_Drag += OnDrag;
		EasyTouch.On_DragEnd += OnDragEnd;
		EasyTouch.On_SwipeStart += OnSwipeStart;
		EasyTouch.On_Swipe += OnSwipe;
		EasyTouch.On_SwipeEnd += OnSwipeEnd;
		EasyTouch.On_TouchStart2Fingers += OnTouchStart2Fingers;
		EasyTouch.On_TouchDown2Fingers += OnTouchDown2Fingers;
		EasyTouch.On_TouchUp2Fingers += OnTouchUp2Fingers;
		EasyTouch.On_SimpleTap2Fingers += OnSimpleTap2Fingers;
		EasyTouch.On_DoubleTap2Fingers += OnDoubleTap2Fingers;
		EasyTouch.On_LongTapStart2Fingers += OnLongTapStart2Fingers;
		EasyTouch.On_LongTap2Fingers += OnLongTap2Fingers;
		EasyTouch.On_LongTapEnd2Fingers += OnLongTapEnd2Fingers;
		EasyTouch.On_Twist += OnTwist;
		EasyTouch.On_TwistEnd += OnTwistEnd;
		EasyTouch.On_PinchIn += OnPinchIn;
		EasyTouch.On_PinchOut += OnPinchOut;
		EasyTouch.On_PinchEnd += OnPinchEnd;
		EasyTouch.On_DragStart2Fingers += OnDragStart2Fingers;
		EasyTouch.On_Drag2Fingers += OnDrag2Fingers;
		EasyTouch.On_DragEnd2Fingers += OnDragEnd2Fingers;
		EasyTouch.On_SwipeStart2Fingers += OnSwipeStart2Fingers;
		EasyTouch.On_Swipe2Fingers += OnSwipe2Fingers;
		EasyTouch.On_SwipeEnd2Fingers += OnSwipeEnd2Fingers;
	}

	private void UnregisterInputDelegates()
	{
		EasyTouch.On_Cancel -= OnCancel;
		EasyTouch.On_TouchStart -= OnTouchStart;
		EasyTouch.On_TouchDown -= OnTouchDown;
		EasyTouch.On_TouchUp -= OnTouchUp;
		EasyTouch.On_SimpleTap -= OnSimpleTap;
		EasyTouch.On_DoubleTap -= OnDoubleTap;
		EasyTouch.On_LongTapStart -= OnLongTapStart;
		EasyTouch.On_LongTap -= OnLongTap;
		EasyTouch.On_LongTapEnd -= OnLongTapEnd;
		EasyTouch.On_DragStart -= OnDragStart;
		EasyTouch.On_Drag -= OnDrag;
		EasyTouch.On_DragEnd -= OnDragEnd;
		EasyTouch.On_SwipeStart -= OnSwipeStart;
		EasyTouch.On_Swipe -= OnSwipe;
		EasyTouch.On_SwipeEnd -= OnSwipeEnd;
		EasyTouch.On_TouchStart2Fingers -= OnTouchStart2Fingers;
		EasyTouch.On_TouchDown2Fingers -= OnTouchDown2Fingers;
		EasyTouch.On_TouchUp2Fingers -= OnTouchUp2Fingers;
		EasyTouch.On_SimpleTap2Fingers -= OnSimpleTap2Fingers;
		EasyTouch.On_DoubleTap2Fingers -= OnDoubleTap2Fingers;
		EasyTouch.On_LongTapStart2Fingers -= OnLongTapStart2Fingers;
		EasyTouch.On_LongTap2Fingers -= OnLongTap2Fingers;
		EasyTouch.On_LongTapEnd2Fingers -= OnLongTapEnd2Fingers;
		EasyTouch.On_Twist -= OnTwist;
		EasyTouch.On_TwistEnd -= OnTwistEnd;
		EasyTouch.On_PinchIn -= OnPinchIn;
		EasyTouch.On_PinchOut -= OnPinchOut;
		EasyTouch.On_PinchEnd -= OnPinchEnd;
		EasyTouch.On_DragStart2Fingers -= OnDragStart2Fingers;
		EasyTouch.On_Drag2Fingers -= OnDrag2Fingers;
		EasyTouch.On_DragEnd2Fingers -= OnDragEnd2Fingers;
		EasyTouch.On_SwipeStart2Fingers -= OnSwipeStart2Fingers;
		EasyTouch.On_Swipe2Fingers -= OnSwipe2Fingers;
		EasyTouch.On_SwipeEnd2Fingers -= OnSwipeEnd2Fingers;
	}

	#region EasyTouch Input Delegate
	private void OnCancel(Gesture gesture)
	{
		for (int i = 0; i < inputListenerList.Count; ++i)
			inputListenerList[i].OnCancel(gesture);
	}

	private void OnTouchStart(Gesture gesture)
	{
		for (int i = 0; i < inputListenerList.Count; ++i)
			inputListenerList[i].OnTouchStart(gesture);
	}

	private void OnTouchDown(Gesture gesture)
	{
		for (int i = 0; i < inputListenerList.Count; ++i)
		{
			bool shouldStop = inputListenerList[i].OnTouchDown(gesture);
			if (shouldStop)
				break;
		}
	}

	public void FakeOnTouchDown(Gesture gesture)
	{
		for (int i = 0; i < inputListenerList.Count; ++i)
		{
			bool shouldStop = inputListenerList[i].OnTouchDown(gesture);
			if (shouldStop)
				break;
		}
	}

	private void OnTouchUp(Gesture gesture)
	{
		if (UICamera.isOverUI)
			return; 
		
        for (int i = 0; i < inputListenerList.Count; ++i)
        {
            bool shouldStop = inputListenerList[i].OnTouchUp(gesture);
            if (shouldStop)
                break;
        }

	}

	private void OnSimpleTap(Gesture gesture)
	{
		for (int i = 0; i < inputListenerList.Count; ++i)
			inputListenerList[i].OnSimpleTap(gesture);
	}

	private void OnDoubleTap(Gesture gesture)
	{
		for (int i = 0; i < inputListenerList.Count; ++i)
			inputListenerList[i].OnDoubleTap(gesture);
	}

	private void OnLongTapStart(Gesture gesture)
	{
		for (int i = 0; i < inputListenerList.Count; ++i)
			inputListenerList[i].OnLongTapStart(gesture);
	}

	private void OnLongTap(Gesture gesture)
	{
		for (int i = 0; i < inputListenerList.Count; ++i)
			inputListenerList[i].OnLongTap(gesture);
	}

	private void OnLongTapEnd(Gesture gesture)
	{
		for (int i = 0; i < inputListenerList.Count; ++i)
			inputListenerList[i].OnLongTapEnd(gesture);
	}

	private void OnDragStart(Gesture gesture)
	{
		for (int i = 0; i < inputListenerList.Count; ++i)
			inputListenerList[i].OnDragStart(gesture);
	}

	private void OnDrag(Gesture gesture)
	{
		for (int i = 0; i < inputListenerList.Count; ++i)
			inputListenerList[i].OnDrag(gesture);
	}

	private void OnDragEnd(Gesture gesture)
	{
		for (int i = 0; i < inputListenerList.Count; ++i)
			inputListenerList[i].OnDragEnd(gesture);
	}

	private void OnSwipeStart(Gesture gesture)
	{
		for (int i = 0; i < inputListenerList.Count; ++i)
			inputListenerList[i].OnSwipeStart(gesture);
	}

	private void OnSwipe(Gesture gesture)
	{
		for (int i = 0; i < inputListenerList.Count; ++i)
			inputListenerList[i].OnSwipe(gesture);
	}

	private void OnSwipeEnd(Gesture gesture)
	{
		for (int i = 0; i < inputListenerList.Count; ++i)
			inputListenerList[i].OnSwipeEnd(gesture);
	}

	private void OnTouchStart2Fingers(Gesture gesture)
	{
		for (int i = 0; i < inputListenerList.Count; ++i)
			inputListenerList[i].OnTouchStart2Fingers(gesture);
	}

	private void OnTouchDown2Fingers(Gesture gesture)
	{
		for (int i = 0; i < inputListenerList.Count; ++i)
			inputListenerList[i].OnTouchDown2Fingers(gesture);
	}

	private void OnTouchUp2Fingers(Gesture gesture)
	{
		for (int i = 0; i < inputListenerList.Count; ++i)
			inputListenerList[i].OnTouchUp2Fingers(gesture);
	}

	private void OnSimpleTap2Fingers(Gesture gesture)
	{
		for (int i = 0; i < inputListenerList.Count; ++i)
			inputListenerList[i].OnSimpleTap2Fingers(gesture);
	}

	private void OnDoubleTap2Fingers(Gesture gesture)
	{
		for (int i = 0; i < inputListenerList.Count; ++i)
			inputListenerList[i].OnDoubleTap2Fingers(gesture);
	}

	private void OnLongTapStart2Fingers(Gesture gesture)
	{
		for (int i = 0; i < inputListenerList.Count; ++i)
			inputListenerList[i].OnLongTapStart2Fingers(gesture);
	}

	private void OnLongTap2Fingers(Gesture gesture)
	{
		for (int i = 0; i < inputListenerList.Count; ++i)
			inputListenerList[i].OnLongTap2Fingers(gesture);
	}

	private void OnLongTapEnd2Fingers(Gesture gesture)
	{
		for (int i = 0; i < inputListenerList.Count; ++i)
			inputListenerList[i].OnLongTapEnd2Fingers(gesture);
	}

	private void OnTwist(Gesture gesture)
	{
		for (int i = 0; i < inputListenerList.Count; ++i)
			inputListenerList[i].OnTwist(gesture);
	}

	private void OnTwistEnd(Gesture gesture)
	{
		for (int i = 0; i < inputListenerList.Count; ++i)
			inputListenerList[i].OnTwistEnd(gesture);
	}

	private void OnPinchIn(Gesture gesture)
	{
		for (int i = 0; i < inputListenerList.Count; ++i)
			inputListenerList[i].OnPinchIn(gesture);
	}

	private void OnPinchOut(Gesture gesture)
	{
		for (int i = 0; i < inputListenerList.Count; ++i)
			inputListenerList[i].OnPinchOut(gesture);
	}

	private void OnPinchEnd(Gesture gesture)
	{
		for (int i = 0; i < inputListenerList.Count; ++i)
			inputListenerList[i].OnPinchEnd(gesture);
	}

	private void OnDragStart2Fingers(Gesture gesture)
	{
		for (int i = 0; i < inputListenerList.Count; ++i)
			inputListenerList[i].OnDragStart2Fingers(gesture);
	}

	private void OnDrag2Fingers(Gesture gesture)
	{
		for (int i = 0; i < inputListenerList.Count; ++i)
			inputListenerList[i].OnDrag2Fingers(gesture);
	}

	private void OnDragEnd2Fingers(Gesture gesture)
	{
		for (int i = 0; i < inputListenerList.Count; ++i)
			inputListenerList[i].OnDragEnd2Fingers(gesture);
	}

	private void OnSwipeStart2Fingers(Gesture gesture)
	{
		for (int i = 0; i < inputListenerList.Count; ++i)
			inputListenerList[i].OnSwipeStart2Fingers(gesture);
	}

	private void OnSwipe2Fingers(Gesture gesture)
	{
		for (int i = 0; i < inputListenerList.Count; ++i)
			inputListenerList[i].OnSwipe2Fingers(gesture);
	}

	private void OnSwipeEnd2Fingers(Gesture gesture)
	{
		for (int i = 0; i < inputListenerList.Count; ++i)
			inputListenerList[i].OnSwipeEnd2Fingers(gesture);
	}
	#endregion
}
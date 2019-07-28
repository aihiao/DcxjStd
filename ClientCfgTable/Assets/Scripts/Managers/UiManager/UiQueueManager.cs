using System;
using System.Collections.Generic;

// 需要排序显示的UI管理器
public class UiQueueManager : AbsManager<UiQueueManager>
{
	private readonly List<Type> _needQueueList = new List<Type> { 
        typeof(UiPnlGetSkillCheats),
		typeof(UiPnlGetMentalCheats),
        typeof(UiPnlGetOwnSkill),
        typeof(UiPnlGetTitleTips),
        typeof(UiPnlLevelUp)
    }; //需要排序的界面 这些界面打开不通过UIManager，而是在这里判断 索引代表优先级

	private readonly Dictionary<Type, Type> _onlyShowOneDic = new Dictionary<Type, Type>
    {
        { typeof(UiPnlGetTitleTips),typeof(UiPnlBattleResultSuccess) },
        { typeof(UiPnlGetOwnSkill),typeof(UiPnlBattleResultSuccess) }
    };  //互斥显示的界面  如果value已经显示 则key不需要显示

	private Type _curShowType { get; set; }   //当前显示的界面  在这缓存 避免反复查找状态
	private int? _curShowData = null;   //保存参数 因为传的是可变参数 很难保存 根据这里用到的几个界面 只要保存一个int就可以了 所以写死在这
	private void _setCurShowInfo(Type t, int? data)   //设置当前显示的界面信息并显示
	{
		_curShowType = t;
		_curShowData = data;
		_removeShowDic(t);
		UiManager.Instance.ShowByName(t.Name, data);
	}

	private bool _ifShowNewFunc = false;    //是否需要显示功能解锁界面 这界面特殊  只在主城显示
	private bool _needShowNext = true; //如果需要打开的界面优先级比打开的界面高 那需要先关闭打开的 但是不需要再关闭时查找显示列表  用变量控制

	//对外接口 需要排序显示的界面都通过这个显示
	public void ShowQueue(Type t, params object[] dataList)
	{
		if (GameStateMachineManager.Instance.GetCurrentStateType() !=
			GameStateBase.GameStateType.CentryCity && GameStateMachineManager.Instance.GetCurrentStateType() != GameStateBase.GameStateType.Dungeon)
			return;
		if (_curShowType == t)
			return;
		if (!_canShow(t))
			return;
		int? data = dataList.Length == 0 ? null : (int?)dataList[0];
		if (_curShowType == null)
		{
			_setCurShowInfo(t, data);
			return;
		}
		int curPriority = _needQueueList.IndexOf(_curShowType);
		int showPriority = _needQueueList.IndexOf(t);
		if (curPriority < showPriority) //显示当前 并保存上一个
		{
			_needShowNext = false;
			UiRelations.Instance.GetUi(_curShowType).Hide();
			_addToShowDic(_curShowType, _curShowData);
			_setCurShowInfo(t, data);
		}
		else
		{
			_addToShowDic(t, data);
		}
	}

	public void ShowNext()  //界面关闭时 通过这个查找是否有在排序的界面需要显示
	{
		if (!_needShowNext)
		{
			_needShowNext = true;
			return;
		}
		_curShowType = null;
		if (_toShowUIList.Count == 0)
			return;
		_setCurShowInfo(_toShowUIList[0], _toShowUIDic[_toShowUIList[0]]);
	}

	private bool _canShow(Type t)
	{
		if (t == typeof(UiPnlLevelUp))
		{
			if (GameStateMachineManager.Instance.GetCurrentStateType() ==
				GameStateBase.GameStateType.CentryCity)
				return true;
			return false;
		}
		if (_onlyShowOneDic.ContainsKey(t))
		{
			Type type = _onlyShowOneDic[t];
			return !UiRelations.Instance.GetIsShowing(type);
		}
		return true;
	}

	#region 需要显示的界面  数据和添加删除操作
	private Dictionary<Type, int?> _toShowUIDic = new Dictionary<Type, int?>();//需要排队显示的界面 用于多个提示界面同时出现时 按一定优先级显示
	private List<Type> _toShowUIList = new List<Type>();    //用于排序 

	private void _addToShowDic(Type T, int? data)
	{
		if (_toShowUIList.Contains(T))
			return;
		_toShowUIDic.Add(T, data);
		int index = _needQueueList.IndexOf(T);
		for (int i = 0; i < _toShowUIList.Count; i++)
		{
			int temp = _needQueueList.IndexOf(_toShowUIList[i]);
			if (temp < index)
			{
				index = i;
				break;
			}
			index = i;
		}
		if (_toShowUIList.Count == 0)
			index = 0;
		_toShowUIList.Insert(index, T);
	}

	public void _removeShowDic(Type t)
	{
		if (_toShowUIDic.ContainsKey(t))
			_toShowUIDic.Remove(t);
		if (_toShowUIList.Contains(t))
			_toShowUIList.Remove(t);
	}
	#endregion 需要显示的界面 数据和添加删除操作
	public override void Dispose()
	{
		_curShowType = null;
		_curShowData = null;
		_toShowUIList.Clear();
	}
}

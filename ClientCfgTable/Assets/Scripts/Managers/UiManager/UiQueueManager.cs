using System;
using System.Collections.Generic;

// ��Ҫ������ʾ��UI������
public class UiQueueManager : AbsManager<UiQueueManager>
{
	private readonly List<Type> _needQueueList = new List<Type> { 
        typeof(UiPnlGetSkillCheats),
		typeof(UiPnlGetMentalCheats),
        typeof(UiPnlGetOwnSkill),
        typeof(UiPnlGetTitleTips),
        typeof(UiPnlLevelUp)
    }; //��Ҫ����Ľ��� ��Щ����򿪲�ͨ��UIManager�������������ж� �����������ȼ�

	private readonly Dictionary<Type, Type> _onlyShowOneDic = new Dictionary<Type, Type>
    {
        { typeof(UiPnlGetTitleTips),typeof(UiPnlBattleResultSuccess) },
        { typeof(UiPnlGetOwnSkill),typeof(UiPnlBattleResultSuccess) }
    };  //������ʾ�Ľ���  ���value�Ѿ���ʾ ��key����Ҫ��ʾ

	private Type _curShowType { get; set; }   //��ǰ��ʾ�Ľ���  ���⻺�� ���ⷴ������״̬
	private int? _curShowData = null;   //������� ��Ϊ�����ǿɱ���� ���ѱ��� ���������õ��ļ������� ֻҪ����һ��int�Ϳ����� ����д������
	private void _setCurShowInfo(Type t, int? data)   //���õ�ǰ��ʾ�Ľ�����Ϣ����ʾ
	{
		_curShowType = t;
		_curShowData = data;
		_removeShowDic(t);
		UiManager.Instance.ShowByName(t.Name, data);
	}

	private bool _ifShowNewFunc = false;    //�Ƿ���Ҫ��ʾ���ܽ������� ���������  ֻ��������ʾ
	private bool _needShowNext = true; //�����Ҫ�򿪵Ľ������ȼ��ȴ򿪵Ľ���� ����Ҫ�ȹرմ򿪵� ���ǲ���Ҫ�ٹر�ʱ������ʾ�б�  �ñ�������

	//����ӿ� ��Ҫ������ʾ�Ľ��涼ͨ�������ʾ
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
		if (curPriority < showPriority) //��ʾ��ǰ ��������һ��
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

	public void ShowNext()  //����ر�ʱ ͨ����������Ƿ���������Ľ�����Ҫ��ʾ
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

	#region ��Ҫ��ʾ�Ľ���  ���ݺ����ɾ������
	private Dictionary<Type, int?> _toShowUIDic = new Dictionary<Type, int?>();//��Ҫ�Ŷ���ʾ�Ľ��� ���ڶ����ʾ����ͬʱ����ʱ ��һ�����ȼ���ʾ
	private List<Type> _toShowUIList = new List<Type>();    //�������� 

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
	#endregion ��Ҫ��ʾ�Ľ��� ���ݺ����ɾ������
	public override void Dispose()
	{
		_curShowType = null;
		_curShowData = null;
		_toShowUIList.Clear();
	}
}

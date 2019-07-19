namespace ClientCommon
{
	[DbSubColumnClass("dungeon", "dungeon_drop_info")]
	sealed public class DungeonDropInfo
	{
		private int _drop_id = 0;
		[DbSplitField(1)]
		public int DropId { get { return _drop_id; } set { _drop_id = value; } }

		private int _is_first_reward = 0;
		[DbSplitField(2)]
		public int IsFirstReward { get { return _is_first_reward; } set { _is_first_reward = value; } }

		private string _count = "";
		[DbSplitField(3)]
		public string Count { get { return _count; } set { _count = value; } }

#if UNITY_EDITOR
		public override string ToString()
		{
			string result = "";
			result += _drop_id.ToString() + ";";
			result += _is_first_reward.ToString() + ";";
			result += _count.ToString() + ";";
			return result;
		}
#endif
	}
}

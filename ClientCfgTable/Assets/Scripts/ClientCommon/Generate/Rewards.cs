namespace ClientCommon
{
	[DbSubColumnClass("faction_tree_mission", "rewards")]
	sealed public class Rewards
	{
		private int _item_id = 0;
		[DbSplitField(1)]
		public int ItemId { get { return _item_id; } set { _item_id = value; } }

		private int _item_count = 0;
		[DbSplitField(2)]
		public int ItemCount { get { return _item_count; } set { _item_count = value; } }

#if UNITY_EDITOR
		public override string ToString()
		{
			string result = "";
			result += _item_id.ToString() + ";";
			result += _item_count.ToString() + ";";
			return result;
		}
#endif
	}
}

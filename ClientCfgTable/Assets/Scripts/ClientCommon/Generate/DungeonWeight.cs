namespace ClientCommon
{
	[DbSubColumnClass("league_boss", "dungeon_weight")]
    public sealed class DungeonWeight
	{
		private int _dungeon_id = 0;
		[DbSplitField(1)]
		public int DungeonId { get { return _dungeon_id; } set { _dungeon_id = value; } }

		private int _weight = 0;
		[DbSplitField(2)]
		public int Weight { get { return _weight; } set { _weight = value; } }

#if UNITY_EDITOR
		public override string ToString()
		{
			string result = "";
			result += _dungeon_id.ToString() + ";";
			result += _weight.ToString() + ";";
			return result;
		}
#endif
	}
}

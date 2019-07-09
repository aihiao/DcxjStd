namespace ClientCommon
{
	sealed public class MonsterList
	{
		private int _monster_type = 0;
		[DbColumn(false, "monster_type", typeof(MonsterType))]
		public int MonsterType { get { return _monster_type; } set { _monster_type = value; } }

		private int _monster_value = 0;
		[DbColumn(false, "monster_value")]
		public int MonsterValue { get { return _monster_value; } set { _monster_value = value; } }

	}
}

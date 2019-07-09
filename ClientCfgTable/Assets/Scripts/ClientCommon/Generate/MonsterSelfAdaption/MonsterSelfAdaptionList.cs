namespace ClientCommon
{
	sealed public class MonsterSelfAdaptionList
	{
		private int _monster_self_adaption_type = 0;
		[DbColumn(false, "monster_self_adaption_type", typeof(MonsterSelfAdaptionType))]
		public int MonsterSelfAdaptionType { get { return _monster_self_adaption_type; } set { _monster_self_adaption_type = value; } }

		private int _monster_self_adaption_value = 0;
		[DbColumn(false, "monster_self_adaption_value")]
		public int MonsterSelfAdaptionValue { get { return _monster_self_adaption_value; } set { _monster_self_adaption_value = value; } }

	}
}

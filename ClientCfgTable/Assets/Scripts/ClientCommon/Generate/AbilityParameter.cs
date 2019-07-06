namespace ClientCommon
{
	[DbTable("editor_ability_sub_ability_parameter", "AbilityParameter", "", "")]
	sealed public class AbilityParameter
	{
		private int _ability_id = 0;
		[DbColumn(false, "ability_id", true, typeof(Ability), "id")]
		public int AbilityId { get { return _ability_id; } set { _ability_id = value; } }

		private int _index = 0;
		[DbColumn(false, "index")]
		public int Index { get { return _index; } set { _index = value; } }

		private string _formula = "";
		[DbColumn(false, "formula")]
		public string Formula { get { return _formula; } set { _formula = value; } }

		private double _value = 0;
		[DbColumn(false, "value")]
		public double Value { get { return _value; } set { _value = value; } }

		private bool _abandoned = false;
		[DbColumn(false, "abandoned")]
		public bool Abandoned { get { return _abandoned; } set { _abandoned = value; } }

	}
}

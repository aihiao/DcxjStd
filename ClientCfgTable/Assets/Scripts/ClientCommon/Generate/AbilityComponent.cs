namespace ClientCommon
{
	[DbTable("editor_ability_sub_ability_component", "AbilityComponent", "", "")]
	sealed public class AbilityComponent
	{
		private int _ability_id = 0;
		[DbColumn(false, "ability_id", true, typeof(Ability), "id")]
		public int AbilityId { get { return _ability_id; } set { _ability_id = value; } }

		private int _id = 0;
		[DbColumn(false, "id")]
		public int Id { get { return _id; } set { _id = value; } }

		private string _component_name = "";
		[DbColumn(false, "component_name")]
		public string ComponentName { get { return _component_name; } set { _component_name = value; } }

		private bool _abandoned = false;
		[DbColumn(false, "abandoned")]
		public bool Abandoned { get { return _abandoned; } set { _abandoned = value; } }

		private int _version = 0;
		[DbColumn(false, "version")]
		public int Version { get { return _version; } set { _version = value; } }

	}
}

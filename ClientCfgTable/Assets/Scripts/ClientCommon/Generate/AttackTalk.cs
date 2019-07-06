namespace ClientCommon
{
	[DbSubColumnClass("monster", "attack_talk")]
	sealed public class AttackTalk
	{
		private string _content = "";
		[DbSplitField(1)]
		public string Content { get { return _content; } set { _content = value; } }

		private int _rate = 0;
		[DbSplitField(2)]
		public int Rate { get { return _rate; } set { _rate = value; } }

		private int _time = 0;
		[DbSplitField(3)]
		public int Time { get { return _time; } set { _time = value; } }

#if UNITY_EDITOR
		public override string ToString()
		{
			string result = "";
			result += _content.ToString() + ";";
			result += _rate.ToString() + ";";
			result += _time.ToString() + ";";
			return result;
		}
#endif
	}
}

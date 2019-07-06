namespace ClientCommon
{
	[DbSubColumnClass("treasure_reel", "gain_data")]
	sealed public class GainData
	{
		private int _gain_type = 0;
		[DbSplitField(1)]
		public int GainType { get { return _gain_type; } set { _gain_type = value; } }

		private string _gain_desc = "";
		[DbSplitField(2)]
		public string GainDesc { get { return _gain_desc; } set { _gain_desc = value; } }

		private string _gain_para = "";
		[DbSplitField(3)]
		public string GainPara { get { return _gain_para; } set { _gain_para = value; } }

#if UNITY_EDITOR
		public override string ToString()
		{
			string result = "";
			result += _gain_type.ToString() + ";";
			result += _gain_desc.ToString() + ";";
			result += _gain_para.ToString() + ";";
			return result;
		}
#endif
	}
}

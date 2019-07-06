namespace ClientCommon
{
	sealed public class AttributeList
	{
		private int _attribute_type = 0;
		[DbColumn(false, "attribute_type", typeof(AttributeType))]
		public int AttributeType { get { return _attribute_type; } set { _attribute_type = value; } }

		private float _attribute_value = 0;
		[DbColumn(false, "attribute_value", null, true)]
		public float AttributeValue { get { return _attribute_value; } set { _attribute_value = value; } }

		private float _attribute_base = 0;
		[DbColumn(false, "attribute_base", null, true)]
		public float AttributeBase { get { return _attribute_base; } set { _attribute_base = value; } }

		private float _attribute_growth = 0;
		[DbColumn(false, "attribute_growth", null, true)]
		public float AttributeGrowth { get { return _attribute_growth; } set { _attribute_growth = value; } }

	}
}

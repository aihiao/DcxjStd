namespace ClientCommon
{
    public sealed class AttributeList
	{
		private int attributeType = 0;
		[DbColumn(false, "attribute_type", typeof(AttributeType))]
		public int AttributeType { get { return attributeType; } set { attributeType = value; } }

		private float attributeValue = 0;
		[DbColumn(false, "attribute_value", null, true)]
		public float AttributeValue { get { return attributeValue; } set { attributeValue = value; } }

		private float attributeBase = 0;
		[DbColumn(false, "attribute_base", null, true)]
		public float AttributeBase { get { return attributeBase; } set { attributeBase = value; } }

		private float attributeGrowth = 0;
		[DbColumn(false, "attribute_growth", null, true)]
		public float AttributeGrowth { get { return attributeGrowth; } set { attributeGrowth = value; } }

	}
}

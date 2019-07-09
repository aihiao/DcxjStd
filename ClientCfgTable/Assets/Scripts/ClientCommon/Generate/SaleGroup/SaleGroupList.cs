namespace ClientCommon
{
	sealed public class SaleGroupList
	{
		private string _sale_group_type = "";
		[DbColumn(false, "sale_group_type")]
		public string SaleGroupType { get { return _sale_group_type; } set { _sale_group_type = value; } }

		private int _sale_group_id = 0;
		[DbColumn(false, "sale_group_id")]
		public int SaleGroupId { get { return _sale_group_id; } set { _sale_group_id = value; } }

		private int _sale_group_sell_type = 0;
		[DbColumn(false, "sale_group_sell_type")]
		public int SaleGroupSellType { get { return _sale_group_sell_type; } set { _sale_group_sell_type = value; } }

		private int _sale_group_sell_number = 0;
		[DbColumn(false, "sale_group_sell_number")]
		public int SaleGroupSellNumber { get { return _sale_group_sell_number; } set { _sale_group_sell_number = value; } }

		private int _sale_group_currency_type = 0;
		[DbColumn(false, "sale_group_currency_type")]
		public int SaleGroupCurrencyType { get { return _sale_group_currency_type; } set { _sale_group_currency_type = value; } }

		private int _sale_group_currency_number = 0;
		[DbColumn(false, "sale_group_currency_number")]
		public int SaleGroupCurrencyNumber { get { return _sale_group_currency_number; } set { _sale_group_currency_number = value; } }

	}
}

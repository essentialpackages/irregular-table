using EssentialPackages.UI.IrregularTables.Data;

namespace EssentialPackages.UI.IrregularTables
{
	public class CustomTableEditor : TableEditor
	{
		private static int _id = 0;
		
		protected new void Awake()
		{
			base.Awake();
			// Test
			CreateCustomRow("player1", "gamepad", "nothing", "5", "Has no items");
		}
		
		public void CreateCustomRow(string playerNo, string controls, string defaultText, string lives, string description)
		{
			var firstUuid = _id++.ToString();
			var secondUuid = _id++.ToString();
			var thirdUuid = _id++.ToString();
			AddItemData(firstUuid, TableCellType.StaticText, new []{playerNo, controls});
			AddItemData(secondUuid, TableCellType.DynamicText, new []{defaultText});
			AddItemData(thirdUuid, TableCellType.StaticText, new []{lives, description});

			var id = _id++.ToString();
			AddItemData(id, TableCellType.Row, new []{firstUuid, secondUuid, thirdUuid});
			
			GetRootData()?.Refs.Add(id);

			var parent = GetRootItem();
			FillTable(new[]{id}, parent, 1);
		}
	}
}

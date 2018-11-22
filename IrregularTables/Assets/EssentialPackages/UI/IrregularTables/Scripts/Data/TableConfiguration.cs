using UnityEngine;

namespace EssentialPackages.UI.IrregularTables.Data
{
	public class TableConfiguration : MonoBehaviour
	{
		[SerializeField] private TableData _tableData;
		[SerializeField] private Transform _tableBody;
		[SerializeField] private TableStyle _style; 

		public TableData TableData => _tableData;
		public Transform Transform => _tableBody;
		public TableStyle TableStyle => _style;
	}
}

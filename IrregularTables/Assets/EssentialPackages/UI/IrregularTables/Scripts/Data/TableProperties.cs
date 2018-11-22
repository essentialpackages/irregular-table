using System;
using EssentialPackages.UI.IrregularTables.Interfaces;
using UnityEngine;

namespace EssentialPackages.UI.IrregularTables.Data
{
	[Serializable]
	public class TableProperties
	{
		[SerializeField] private TableData _tableData;
		[SerializeField] private Transform _tableBody;
		[SerializeField] private TableStyle _style; 

		public ITableData TableData => _tableData;
		public Transform TableBody => _tableBody;
		public ITableStyle TableStyle => _style;
	}
}

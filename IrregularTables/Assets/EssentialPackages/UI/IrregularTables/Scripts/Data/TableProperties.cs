using System;
using EssentialPackages.UI.IrregularTables.Interfaces;
using UnityEngine;

namespace EssentialPackages.UI.IrregularTables.Data
{
	[Serializable]
	public class TableProperties : ITableProperties
	{
		[SerializeField] private Transform _tableBody;
		[SerializeField] private string _rootId;
		[SerializeField] private TableStyle _style;
		[SerializeField] private TableData _tableData;

		public ITableData TableData => _tableData;
		public Transform TableBody => _tableBody;
		public ITableStyle TableStyle => _style;
		public string RootId => _rootId;
	}
}

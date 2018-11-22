using UnityEngine;

namespace EssentialPackages.UI.IrregularTables.Interfaces
{
	public interface ITableProperties {

		ITableData TableData { get; }
		Transform TableBody { get; }
		ITableStyle TableStyle { get; }
	}
}

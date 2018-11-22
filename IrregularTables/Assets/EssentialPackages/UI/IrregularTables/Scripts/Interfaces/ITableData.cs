using System.Collections.Generic;
using EssentialPackages.UI.IrregularTables.Data;

namespace EssentialPackages.UI.IrregularTables.Interfaces
{
	public interface ITableData
	{
		void AddCell(string id, TableCellType type, ICollection<string> refs);
		TableCell GetRootCell();
		TableCell FindCell(string id);
		IEnumerable<TableCell> FindCells(ICollection<string> ids);
	}
}

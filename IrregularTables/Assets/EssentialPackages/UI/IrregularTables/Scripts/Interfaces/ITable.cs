using EssentialPackages.UI.IrregularTables.Data;
using UnityEngine;

namespace Essential.Core.UI.Table.Interfaces
{
    public interface ITable
    {
        GameObject CreateItem(TableCellType type, Transform parent);
    }
}

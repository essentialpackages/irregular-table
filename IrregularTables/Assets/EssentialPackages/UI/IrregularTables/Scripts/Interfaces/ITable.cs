using EssentialPackages.UI.IrregularTables.Data;
using UnityEngine;

namespace EssentialPackages.UI.IrregularTables.Interfaces
{
    public interface ITable
    {
        GameObject CreateItem(TableCellType type, Transform parent);
    }
}

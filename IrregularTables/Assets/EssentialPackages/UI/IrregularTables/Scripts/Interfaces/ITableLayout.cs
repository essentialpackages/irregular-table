using System;
using System.Collections.Generic;
using EssentialPackages.UI.IrregularTables.Data;
using UnityEngine;

namespace EssentialPackages.UI.IrregularTables.Interfaces
{
    public interface ITableLayout
    {
        void ExpandTable(IEnumerable<TableCell> cells, Transform parent, int depth,
            Action<ICollection<string>, Transform, int> hasChildren);
    }
}
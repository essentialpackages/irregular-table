using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Essential.Core.UI.Table.Data
{
    [Serializable]
    public class TableData
    {
        [SerializeField] private List<TableCell> _body;
        
        public void AddCell(string id, TableCellType type, ICollection<string> refs)
        {
            _body.Add(new TableCell()
            {
                Id = id,
                Type = type,
                Refs = refs
            });
        }

        public TableCell GetRootCell()
        {
            return (_body.Count == 0)? null : _body[0];
        }

        public TableCell FindCell(string id)
        {
            return string.IsNullOrEmpty(id) ? null : _body.FirstOrDefault(element => element.Id == id);
        }
        
        public IEnumerable<TableCell> FindCells(ICollection<string> ids)
        {
            return ids.Count == 0 ? null : _body.Where(element => ids.Contains(element.Id));
        }
    }
}

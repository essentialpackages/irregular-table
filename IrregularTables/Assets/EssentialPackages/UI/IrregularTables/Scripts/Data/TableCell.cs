using System;
using System.Collections.Generic;
using UnityEngine;

namespace Essential.Core.UI.Table.Data
{
	[Serializable]
	public class TableCell : TableCellBase<string>
	{
		[SerializeField] private TableCellType _type;
		[SerializeField] private List<string> _refs = new List<string>();
		
		public TableCellType Type
		{
			get { return _type; }
			set { _type = value; }
		}
		
		public ICollection<string> Refs
		{
			get { return _refs; }
			set { _refs.Clear(); _refs.AddRange(value); }
		}
	}
}

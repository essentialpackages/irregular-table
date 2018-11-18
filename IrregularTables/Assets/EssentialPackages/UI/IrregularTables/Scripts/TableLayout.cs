using System;
using System.Collections.Generic;
using Essential.Core.UI.Table.Data;
using Essential.Core.UI.Table.Interfaces;
using TMPro;
using UnityEngine;

namespace Essential.Core.UI.Table
{
	public class TableLayout
	{
		private ITable Table { get; }
		private ITextRegistry<TMP_Text> TextRegistry { get; }

		public TableLayout(ITable table)
		{
			Table = table;
			TextRegistry = new TextMeshProTextRegistry();
		}
	
		public void ExpandTable(IEnumerable<TableCell> cells, Transform parent, int depth, Action<ICollection<string>, Transform, int> hasChildren)
		{
			foreach (var cell in cells)
			{
				switch (cell.Type)
				{
					case TableCellType.Empty:
					{
						for(var index = 0; index < cell.Refs.Count; ++index)
						{
							Table.CreateItem(cell.Type, parent);
						}
						break;
					}
					case TableCellType.StaticText:
					{
						foreach (var content in cell.Refs)
						{
							var text = Table.CreateItem(cell.Type, parent).GetComponent<TMP_Text>();
							text.text = content;
						}
						break;
					}
					case TableCellType.DynamicText:
					{
						foreach (var content in cell.Refs)
						{
							var text = Table.CreateItem(cell.Type, parent).GetComponent<TMP_Text>();
							text.text = content;
							TextRegistry.Register(cell.Id, text);
						}
						break;
					}
					case TableCellType.Row:
					case TableCellType.Column:
					{
						var layoutElement = Table.CreateItem(cell.Type, parent).transform;
						hasChildren(cell.Refs, layoutElement, depth + 1);
						break;
					}
					default:
					{
						throw new ArgumentOutOfRangeException();
					}	
				}
			}
		}
	}
}

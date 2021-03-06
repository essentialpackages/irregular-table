﻿using System;
using System.Collections.Generic;
using EssentialPackages.UI.IrregularTables.Data;
using EssentialPackages.UI.IrregularTables.Interfaces;
using EssentialPackages.UI.TextAdapters.Interfaces;
using UnityEngine;

namespace EssentialPackages.UI.IrregularTables
{
	public class TableLayout : ITableLayout
	{
		private ITable Table { get; }
		private ITextRegistry<ITextComponent> TextRegistry { get; }

		public TableLayout(ITable table, ITextRegistry<ITextComponent> textRegistry)
		{
			if (table == null)
			{
				throw new ArgumentNullException(nameof(table));
			}

			if (textRegistry == null)
			{
				throw new ArgumentNullException(nameof(textRegistry));
			}
			
			Table = table;
			TextRegistry = textRegistry;
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
							var text = Table.CreateItem(cell.Type, parent).GetComponent<ITextComponent>();
							text.Text = content;
						}
						break;
					}
					case TableCellType.DynamicText:
					{
						foreach (var content in cell.Refs)
						{
							var text = Table.CreateItem(cell.Type, parent).GetComponent<ITextComponent>();
							text.Text = content;
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

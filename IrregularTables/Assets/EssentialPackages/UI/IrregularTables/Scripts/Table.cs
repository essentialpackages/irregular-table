using System;
using EssentialPackages.UI.IrregularTables.Data;
using EssentialPackages.UI.IrregularTables.Interfaces;
using UnityEngine;
using Object = UnityEngine.Object;

namespace EssentialPackages.UI.IrregularTables
{
	public class Table : ITable
	{
		private ITableStyle Style { get; }
		
		public Table(ITableStyle style)
		{
			Style = style;
		}

		public GameObject CreateItem(TableCellType type, Transform parent)
		{
			switch (type)
			{
				case TableCellType.Empty:
				{
					return Object.Instantiate(Style.Empty, parent);
				}
				case TableCellType.StaticText:
				case TableCellType.DynamicText:
				{
					return Object.Instantiate(Style.Text, parent);
				}
				case TableCellType.Row:
				{
					return Object.Instantiate(Style.Row, parent);
				}
				case TableCellType.Column:
				{
					return Object.Instantiate(Style.Column, parent);
				}
				default:
				{
					throw new ArgumentOutOfRangeException();
				}	
			}
		}
	}
}

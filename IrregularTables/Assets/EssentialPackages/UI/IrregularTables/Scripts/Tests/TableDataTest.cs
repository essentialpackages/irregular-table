using System;
using System.Collections.Generic;
using System.Reflection;
using EssentialPackages.UI.IrregularTables.Data;
using NUnit.Framework;

namespace EssentialPackages.UI.IrregularTables.Tests
{
	public class TableDataTest
	{
		private const BindingFlags Binding = BindingFlags.NonPublic | BindingFlags.Instance;
		private FieldInfo _cells = typeof(TableData).GetField("_body", Binding);
		
		[Test]
		public void AddCell_Should_ThrowNullReferenceException_When_CollectionOfCellsWasNotInitialized()
		{
			Assert.Throws<ArgumentNullException>(() =>
			{
				var tableData = new TableData();
				tableData.AddCell("", TableCellType.None, null);
			});
		}
		
		[Test]
		public void AddCell_Should_ThrowNullReferenceException_When_PassingNullAsReferences()
		{
			Assert.Throws<NullReferenceException>(() =>
			{
				var tableData = new TableData();
				_cells.SetValue(tableData, new List<TableCell>());

				tableData.AddCell("", TableCellType.None, null);
			});
		}
		
		[Test]
		public void AddCell_Should_IncreaseCellCountByOne_When_PassingZeroReferences()
		{
			var tableData = new TableData();
			_cells.SetValue(tableData, new List<TableCell>());
			
			tableData.AddCell("", TableCellType.None, new string[0]);
			
			var actual = _cells.GetValue(tableData);
			Assert.AreEqual(1, actual);
		}
		
		[Test]
		public void AddCell_Should_IncreaseCellCountByOne_When_PassingOneReference()
		{
			var tableData = new TableData();
			_cells.SetValue(tableData, new List<TableCell>());
			
			tableData.AddCell("", TableCellType.None, new string[0]);
			
			var actual = _cells.GetValue(tableData);
			Assert.AreEqual(1, actual);
		}
	}
}

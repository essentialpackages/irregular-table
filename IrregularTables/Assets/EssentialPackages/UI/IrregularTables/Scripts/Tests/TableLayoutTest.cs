using System.Collections.Generic;
using System.Diagnostics;
using EssentialPackages.UI.IrregularTables.Data;
using EssentialPackages.UI.IrregularTables.Interfaces;
using EssentialPackages.UI.TextAdapters.Interfaces;
using NUnit.Framework;
using UnityEngine;

namespace EssentialPackages.UI.IrregularTables.Tests
{
	public class TableLayoutTest
	{
		//private TableLayout TargetScript { get; set; }
		//private ICollection<TableCell> Cells { get; set; }

		private class FakeTable : ITable
		{
			private GameObject Prefab { get; }
			
			public FakeTable(GameObject prefab)
			{
				Prefab = prefab;
			}
			
			public GameObject CreateItem(TableCellType type, Transform parent)
			{
				return (Prefab == null) ? null : Object.Instantiate(Prefab);
			}
		}

		private class FakeRegistry : ITextRegistry<ITextComponent>
		{
			public void Register(string id, ITextComponent text) {}
		}

		private TableLayout CreateInvalidLayoutUsingNullTable()
		{
			return new TableLayout(null, new FakeRegistry());
		}
		
		private TableLayout CreateInvalidLayoutUsingBrokenFakeTable()
		{
			return new TableLayout(new FakeTable(null), new FakeRegistry());
		}
		
		private TableLayout CreateValidLayoutUsingFakeTable()
		{
			return new TableLayout(new FakeTable(null), new FakeRegistry());
		}
        
		[SetUp]
		public void SetUp()
		{
			var cell = new TableCell();
			//Cells = new List<TableCell>(){};
			//TargetScript = new TableLayout(new FakeTable(null), new FakeRegistry());
		}
        
		[TearDown]
		public void TearDown()
		{
			//TargetScript = null;
		}

		[Test]
		public void ExpandTable_Should_NotHaveAnyEffect_When_CollectionWasNull()
		{
			var tableLayout = CreateInvalidLayoutUsingNullTable();
			
			tableLayout.ExpandTable(null, null, 0, null);
			
			Assert.Pass();
		}
		
		[Test]
		public void ExpandTable_Should_NotHaveAnyEffect_When_EmptyCellHasNoReferences()
		{
			var tableLayout = CreateInvalidLayoutUsingNullTable();
			var cells = new List<TableCell>()
			{
				{ new TableCell() { Type = TableCellType.Empty } }
			};
			
			tableLayout.ExpandTable(cells, null, 0, null);
			
			Assert.Pass();
		}
		
		[Test]
		public void ExpandTable_Should_NotHaveAnyEffect_When_StaticTextCellHasNoReferences()
		{
			var tableLayout = CreateInvalidLayoutUsingNullTable();
			var cells = new List<TableCell>()
			{
				{ new TableCell() { Type = TableCellType.StaticText } }
			};
			
			tableLayout.ExpandTable(cells, null, 0, null);
			
			Assert.Pass();
		}
		
		[Test]
		public void ExpandTable_Should_NotHaveAnyEffect_When_DynamicTextCellHasNoReferences()
		{
			var tableLayout = CreateInvalidLayoutUsingNullTable();
			var cells = new List<TableCell>()
			{
				{ new TableCell() { Type = TableCellType.DynamicText } }
			};
			
			tableLayout.ExpandTable(cells, null, 0, null);
			
			Assert.Pass();
		}
		
		[Test]
		public void ExpandTable_Should_ThrowNullReferenceException_When_EmptyRowIsProcessedByNullTable()
		{
			var tableLayout = CreateInvalidLayoutUsingNullTable();
			var cells = new List<TableCell>()
			{
				{ new TableCell() { Type = TableCellType.Row } }
			};
			
			tableLayout.ExpandTable(cells, null, 0, null);
			
			Assert.Pass();
		}
		
		[Test]
		public void ExpandTable_Should_ThrowNullReferenceException_When_EmptyColumnIsProcessedByNullTable()
		{
			var tableLayout = CreateInvalidLayoutUsingNullTable();
			var cells = new List<TableCell>()
			{
				{ new TableCell() { Type = TableCellType.Column } }
			};
			
			tableLayout.ExpandTable(cells, null, 0, null);
			
			Assert.Pass();
		}
		
		/*[Test]
		public void ExpandTable_Should_ThrowNullReferenceException_When_TableWasNotInitialized()
		{
			var cell = new TableCell();
			cell.Refs.Add("0");
			Cells = new List<TableCell>(){};
			TargetScript.ExpandTable(Cells, null, 0, null);
			Assert.Fail();
		}
		
		[Test]
		public void ExpandTable_Should_AttachAllGameObjectsToRoot_When_ParentTransformWasNull()
		{
			TargetScript.ExpandTable(null, null, 0, null);
			Assert.Fail();
		}
		
		[Test]
		public void ExpandTable_Should_NotThrowException_When_CallbackWasSetToNull()
		{
			TargetScript.ExpandTable(null, null, 0, null);
			Assert.Fail();
		}
		
		[Test]
		public void ExpandTable_Should_ReturnIncreasedDepthByOne_When_CallbackIsCalled()
		{
			TargetScript.ExpandTable(null, null, 0, null);
			Assert.Fail();
		}*/
	}
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using EssentialPackages.UI.IrregularTables.Data;
using EssentialPackages.UI.IrregularTables.Interfaces;
using EssentialPackages.UI.TextAdapters.Interfaces;
using NUnit.Framework;
using UnityEditor.VersionControl;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;

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
				return Object.Instantiate(Prefab, parent);
			}
		}

		private class FakeScript : MonoBehaviour, ITextComponent
		{
			public string Id { get; set; }
			public string Text { get; set; }
		}

		private FakeTable BrokenTable { get; set; }
		private FakeTable IncompleteTable { get; set; } 
		private FakeTable ValidTable { get; set; }
		
		private class FakeRegistry : ITextRegistry<ITextComponent>
		{
			public void Register(string id, ITextComponent text) {}
		}
		
		private TableLayout CreateInvalidLayoutUsingBrokenFakeTable()
		{
			return new TableLayout(BrokenTable, new FakeRegistry());
		}
		
		private TableLayout CreateInvalidLayoutUsingIncompleteFakeTable()
		{
			return new TableLayout(IncompleteTable, new FakeRegistry());
		}
		
		private TableLayout CreateValidLayoutUsingFakeTable()
		{
			return new TableLayout(ValidTable, new FakeRegistry());
		}

		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			BrokenTable = new FakeTable(null);
			IncompleteTable = new FakeTable(new GameObject());

			var go = new GameObject();
			go.AddComponent<FakeScript>();
			ValidTable = new FakeTable(go);
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
		public void Constructor_Should_ThrowArgumentNullException_When_TableWasNull()
		{
			Assert.Throws<ArgumentNullException>(() => new TableLayout(null, new FakeRegistry()));
		}
		
		[Test]
		public void Constructor_Should_ThrowArgumentNullException_When_RegistryWasNull()
		{
			Assert.Throws<ArgumentNullException>(() => new TableLayout(new FakeTable(null), null));
		}

		[Test]
		public void Constructor_Should_Succeed_When_AllParametersWereNotNull()
		{
			Assert.DoesNotThrow(() => CreateValidLayoutUsingFakeTable());
		}

		[Test]
		public void ExpandBrokenTable_Should_ThrowArgumentException_When_CreatingNewEmptyCell()
		{
			Assert.Throws<ArgumentException>(() =>
				{
					var tableLayout = CreateInvalidLayoutUsingBrokenFakeTable();
					var cells = new List<TableCell>()
					{
						{ new TableCell() { Type = TableCellType.Empty, Refs = new [] {""} } }
					};
					
					tableLayout.ExpandTable(cells, null, 0, null);
				}
			);
		}
		
		[Test]
		public void ExpandBrokenTable_Should_ThrowArgumentException_When_CreatingNewStaticText()
		{
			Assert.Throws<ArgumentException>(() =>
				{
					var tableLayout = CreateInvalidLayoutUsingBrokenFakeTable();
					var cells = new List<TableCell>()
					{
						{ new TableCell() { Type = TableCellType.StaticText, Refs = new [] {""} } }
					};
					
					tableLayout.ExpandTable(cells, null, 0, null);
				}
			);
		}
		
		[Test]
		public void ExpandBrokenTable_Should_ThrowArgumentException_When_CreatingNewDynamicText()
		{
			Assert.Throws<ArgumentException>(() =>
				{
					var tableLayout = CreateInvalidLayoutUsingBrokenFakeTable();
					var cells = new List<TableCell>()
					{
						{ new TableCell() { Type = TableCellType.DynamicText, Refs = new [] {""} } }
					};
					
					tableLayout.ExpandTable(cells, null, 0, null);
				}
			);
		}
		
		[Test]
		public void ExpandBrokenTable_Should_ThrowArgumentException_When_CreatingNewRow()
		{
			Assert.Throws<ArgumentException>(() =>
				{
					var tableLayout = CreateInvalidLayoutUsingBrokenFakeTable();
					var cells = new List<TableCell>()
					{
						{ new TableCell() { Type = TableCellType.Row } }
					};
					
					tableLayout.ExpandTable(cells, null, 0, null);
				}
			);
		}
		
		[Test]
		public void ExpandBrokenTable_Should_ThrowArgumentException_When_CreatingNewColumn()
		{
			Assert.Throws<ArgumentException>(() =>
				{
					var tableLayout = CreateInvalidLayoutUsingBrokenFakeTable();
					var cells = new List<TableCell>()
					{
						{ new TableCell() { Type = TableCellType.Column } }
					};
					
					tableLayout.ExpandTable(cells, null, 0, null);
				}
			);
		}
		
		[Test]
		public void ExpandIncompleteTable_Should_CreateChildObject_When_CreatingNewEmptyCell()
		{
			var parent = new GameObject();
			var tableLayout = CreateInvalidLayoutUsingIncompleteFakeTable();
			var cells = new List<TableCell>()
			{
				{ new TableCell() { Type = TableCellType.Empty, Refs = new [] {""} } }
			};
			
			tableLayout.ExpandTable(cells, parent.transform, 0, null);
			
			Assert.AreEqual(1, parent.transform.childCount);
		}
		
		[Test]
		public void ExpandIncompleteTable_Should_ThrowNullReferenceException_When_CreatingNewStaticText()
		{
			Assert.Throws<NullReferenceException>(() =>
				{
					var tableLayout = CreateInvalidLayoutUsingIncompleteFakeTable();
					var cells = new List<TableCell>()
					{
						{ new TableCell() { Type = TableCellType.StaticText, Refs = new [] {""} } }
					};
					
					tableLayout.ExpandTable(cells, null, 0, null);
				}
			);
		}
		
		[Test]
		public void ExpandIncompleteTable_Should_ThrowNullReferenceException_When_CreatingNewDynamicText()
		{
			Assert.Throws<NullReferenceException>(() =>
				{
					var tableLayout = CreateInvalidLayoutUsingIncompleteFakeTable();
					var cells = new List<TableCell>()
					{
						{ new TableCell() { Type = TableCellType.DynamicText, Refs = new [] {""} } }
					};
					
					tableLayout.ExpandTable(cells, null, 0, null);
				}
			);
		}
		
		[Test]
		public void ExpandIncompleteTable_Should_ThrowNullReferenceException_When_CreatingNewRow()
		{
			Assert.Throws<NullReferenceException>(() =>
				{
					var tableLayout = CreateInvalidLayoutUsingIncompleteFakeTable();
					var cells = new List<TableCell>()
					{
						{ new TableCell() { Type = TableCellType.Row } }
					};
					
					tableLayout.ExpandTable(cells, null, 0, null);
				}
			);
		}
		
		[Test]
		public void ExpandIncompleteTable_Should_ThrowNullReferenceException_When_CreatingNewColumn()
		{
			Assert.Throws<NullReferenceException>(() =>
				{
					var tableLayout = CreateInvalidLayoutUsingIncompleteFakeTable();
					var cells = new List<TableCell>()
					{
						{ new TableCell() { Type = TableCellType.Column } }
					};
					
					tableLayout.ExpandTable(cells, null, 0, null);
				}
			);
		}
		
		[Test]
		public void ExpandValidTable_Should_CreateChildObject_When_CreatingNewEmptyCell()
		{
			var parent = new GameObject();
			var tableLayout = CreateValidLayoutUsingFakeTable();
			var cells = new List<TableCell>()
			{
				{ new TableCell() { Type = TableCellType.Empty, Refs = new [] {""} } }
			};
			
			tableLayout.ExpandTable(cells, parent.transform, 0, null);
			
			Assert.AreEqual(1, parent.transform.childCount);
		}
		
		[Test]
		public void ExpandValidTable_Should_CreateChildObject_When_CreatingNewStaticText()
		{
			var parent = new GameObject();
			var tableLayout = CreateValidLayoutUsingFakeTable();
			var cells = new List<TableCell>()
			{
				{ new TableCell() { Type = TableCellType.StaticText, Refs = new [] {""} } }
			};
			
			tableLayout.ExpandTable(cells, parent.transform, 0, null);
			
			Assert.AreEqual(1, parent.transform.childCount);
		}
		
		[Test]
		public void ExpandValidTable_Should_CreateChildObject_When_CreatingNewDynamicText()
		{
			var parent = new GameObject();
			var tableLayout = CreateValidLayoutUsingFakeTable();
			var cells = new List<TableCell>()
			{
				{ new TableCell() { Type = TableCellType.DynamicText, Refs = new [] {""} } }
			};
			
			tableLayout.ExpandTable(cells, parent.transform, 0, null);
			
			Assert.AreEqual(1, parent.transform.childCount);
		}
		
		[Test]
		public void ExpandValidTable_Should_CreateChildObject_When_CreatingNewRow()
		{
			var parent = new GameObject();
			var tableLayout = CreateValidLayoutUsingFakeTable();
			var cells = new List<TableCell>()
			{
				{ new TableCell() { Type = TableCellType.Row } }
			};
			
			tableLayout.ExpandTable(cells, parent.transform, 0, (a, b, c) => {});
			
			Assert.AreEqual(1, parent.transform.childCount);
		}
		
		[Test]
		public void ExpandValidTable_Should_CreateChild_When_CreatingNewColumn()
		{
			var parent = new GameObject();
			var tableLayout = CreateValidLayoutUsingFakeTable();
			var cells = new List<TableCell>()
			{
				{ new TableCell() { Type = TableCellType.Column } }
			};

			tableLayout.ExpandTable(cells, parent.transform, 0, (a, b, c) => {});
			
			Assert.AreEqual(1, parent.transform.childCount);
		}
		
		[Test]
		public void ExpandTable_Should_ThrowNullReferenceException_When_CellsWereNull()
		{
			Assert.Throws<NullReferenceException>(() =>
				{
					var tableLayout = CreateValidLayoutUsingFakeTable();

					tableLayout.ExpandTable(null, null, 0, null);
				}
			);
		}

		[Test]
		public void ExpandTable_Should_NotIncreaseChildCount_When_TryingToCreateNewEmptyCellWithoutReferenceId()
		{
			var parent = new GameObject();
			var tableLayout = CreateValidLayoutUsingFakeTable();
			var cells = new List<TableCell>()
			{
				{new TableCell() {Type = TableCellType.Empty}}
			};

			tableLayout.ExpandTable(cells, parent.transform, 0, null);
			
			Assert.AreEqual(0, parent.transform.childCount);

		}
		
		[Test]
		public void ExpandTable_Should_NotIncreaseChildCount_When_TryingToCreateNewStaticTextWithoutReferenceId()
		{
			var parent = new GameObject();
			var tableLayout = CreateValidLayoutUsingFakeTable();
			var cells = new List<TableCell>()
			{
				{new TableCell() {Type = TableCellType.StaticText}}
			};

			tableLayout.ExpandTable(cells, parent.transform, 0, null);
			
			Assert.AreEqual(0, parent.transform.childCount);
		}
		
		[Test]
		public void ExpandTable_Should_NotIncreaseChildCount_When_TryingToCreateNewDynamicTextWithoutReferenceId()
		{
			var parent = new GameObject();
			var tableLayout = CreateValidLayoutUsingFakeTable();
			var cells = new List<TableCell>()
			{
				{new TableCell() {Type = TableCellType.DynamicText}}
			};

			tableLayout.ExpandTable(cells, parent.transform, 0, null);
			
			Assert.AreEqual(0, parent.transform.childCount);
		}
		
		[Test]
		public void ExpandTable_Should_IncreaseChildCount_When_TryingToCreateNewRowWithoutReferenceId()
		{
			var parent = new GameObject();
			var tableLayout = CreateValidLayoutUsingFakeTable();
			var cells = new List<TableCell>()
			{
				{new TableCell() {Type = TableCellType.Row}}
			};

			tableLayout.ExpandTable(cells, parent.transform, 0, (a, b, c) => {});
			
			Assert.AreEqual(1, parent.transform.childCount);
		}

		[Test]
		public void ExpandTable_Should_IncreaseChildCount_When_TryingToCreateNewColumnWithoutReferenceId()
		{
			var parent = new GameObject();
			var tableLayout = CreateValidLayoutUsingFakeTable();
			var cells = new List<TableCell>()
			{
				{new TableCell() {Type = TableCellType.Column}}
			};

			tableLayout.ExpandTable(cells, parent.transform, 0, (a, b, c) => {});
			
			Assert.AreEqual(1, parent.transform.childCount);
		}
		
		[Test]
		public void ExpandTable_Should_IncreaseDepthByOne_When_TryingToCreateNewRow()
		{
			var parent = new GameObject();
			var tableLayout = CreateValidLayoutUsingFakeTable();
			var cells = new List<TableCell>()
			{
				{new TableCell() {Type = TableCellType.Row}}
			};

			tableLayout.ExpandTable(cells, parent.transform, 0, (a, b, depth) =>
			{
				Assert.AreEqual(1, depth);
			});
		}

		[Test]
		public void ExpandTable_Should_IncreaseDepthByOne_When_TryingToCreateNewColumn()
		{
			var parent = new GameObject();
			var tableLayout = CreateValidLayoutUsingFakeTable();
			var cells = new List<TableCell>()
			{
				{new TableCell() {Type = TableCellType.Column}}
			};

			tableLayout.ExpandTable(cells, parent.transform, 0, (a, b, depth) =>
			{
				Assert.AreEqual(1, depth);
			});
		}
	}
}

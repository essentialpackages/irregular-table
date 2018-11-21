using System;
using System.Reflection;
using EssentialPackages.UI.IrregularTables.Data;
using EssentialPackages.UI.IrregularTables.Interfaces;
using NUnit.Framework;
using UnityEngine;

namespace EssentialPackages.UI.IrregularTables.Tests
{
	public class TableTest
	{
		private Table ScriptUnderTest { get; set; }
		private GameObject DummyParent { get; set; }
		private GameObject DummyChild { get; set; }

		private class FakeTableStyle : ITableStyle
		{
			public GameObject Empty { get; }
			public GameObject Text { get; }
			public GameObject Row { get; }
			public GameObject Column { get; }

			public FakeTableStyle(GameObject prefab)
			{
				Empty = prefab;
				Text = prefab;
				Row = prefab;
				Column = prefab;
			}
		}
	
		[OneTimeSetUp]
		public void OneTimeSetUp()
		{			
			DummyParent = new GameObject();
			DummyChild= new GameObject();
		}
		
		private TableStyle CreateIncompleteTableStyle()
		{
			return ScriptableObject.CreateInstance<TableStyle>();;
		}

		private Table CreateBrokenTable()
		{
			var style = new FakeTableStyle(DummyChild);
			var table = new Table(style);
			
			var type = typeof(Table);
			var bakedFieldInfo = type.GetField("<Style>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance);
			bakedFieldInfo?.SetValue(table, null);

			return table;
		}
		
		private Table CreateIncompleteTable()
		{
			var style = new FakeTableStyle(null);
			var table = new Table(style);
			
			return table;
		}

		private Table CreateValidTable()
		{
			var style = new FakeTableStyle(DummyChild);
			return new Table(style);
		}
		
		[Test]
		public void Constructor_Should_ThrowArgumentNullException_When_StyleWasNull()
		{
			Assert.Throws<ArgumentNullException>(() => ScriptUnderTest = new Table(null) );
		}
		
		[Test]
		public void Constructor_Should_Succeed_When_StyleWasNotNull()
		{
			Assert.DoesNotThrow(() => CreateBrokenTable() );
		}

		[Test]
		public void CreateItem_Should_ThrowArgumentOutOfRangeException_When_CellTypeWasNone()
		{
			Assert.Throws<ArgumentOutOfRangeException>(() =>
			{
				var table = CreateValidTable();

				table.CreateItem(TableCellType.None, DummyParent.transform);
			});
		}
		
		[Test]
		public void CreateItem_Should_ThrowNullReferenceException_When_BrokenTableTriesToInstantiateEmptyCell()
		{
			Assert.Throws<NullReferenceException>(() =>
			{
				var table = CreateBrokenTable();

				table.CreateItem(TableCellType.Empty, DummyParent.transform);
			});
		}
		
		[Test]
		public void CreateItem_Should_ThrowNullReferenceException_When_BrokenTableTriesToInstantiateStaticText()
		{
			Assert.Throws<NullReferenceException>(() =>
			{
				var table = CreateBrokenTable();

				table.CreateItem(TableCellType.StaticText, DummyParent.transform);
			});
		}
		
		[Test]
		public void CreateItem_Should_ThrowNullReferenceException_When_BrokenTableTriesToInstantiateDynamicText()
		{
			Assert.Throws<NullReferenceException>(() =>
			{
				var table = CreateBrokenTable();

				table.CreateItem(TableCellType.DynamicText, DummyParent.transform);
			});
		}
		
		[Test]
		public void CreateItem_Should_ThrowNullReferenceException_When_BrokenTableTriesToInstantiateRow()
		{
			Assert.Throws<NullReferenceException>(() =>
			{
				var table = CreateBrokenTable();

				table.CreateItem(TableCellType.Row, DummyParent.transform);
			});
		}
		
		[Test]
		public void CreateItem_Should_ThrowNullReferenceException_When_BrokenTableTriesToInstantiateColumn()
		{
			Assert.Throws<NullReferenceException>(() =>
			{
				var table = CreateBrokenTable();

				table.CreateItem(TableCellType.Column, DummyParent.transform);
			});
		}
		
		[Test]
		public void CreateItem_Should_ThrowArgumentException_When_InvalidTableTriesToInstantiateEmptyCell()
		{
			Assert.Throws<ArgumentException>(() =>
			{
				var table = CreateIncompleteTable();

				table.CreateItem(TableCellType.Empty, DummyParent.transform);
			});
		}
		
		[Test]
		public void CreateItem_Should_ThrowArgumentException_When_InvalidTableTriesToInstantiateStaticText()
		{
			Assert.Throws<ArgumentException>(() =>
			{
				var table = CreateIncompleteTable();

				table.CreateItem(TableCellType.StaticText, DummyParent.transform);
			});
		}
		
		[Test]
		public void CreateItem_Should_ThrowArgumentException_When_InvalidTableTriesToInstantiateDynamicText()
		{
			Assert.Throws<ArgumentException>(() =>
			{
				var table = CreateIncompleteTable();

				table.CreateItem(TableCellType.DynamicText, DummyParent.transform);
			});
		}
		
		[Test]
		public void CreateItem_Should_ThrowArgumentException_When_InvalidTableTriesToInstantiateRow()
		{
			Assert.Throws<ArgumentException>(() =>
			{
				var table = CreateIncompleteTable();

				table.CreateItem(TableCellType.Row, DummyParent.transform);
			});
		}
		
		[Test]
		public void CreateItem_Should_ThrowArgumentException_When_InvalidTableTriesToInstantiateColumn()
		{
			Assert.Throws<ArgumentException>(() =>
			{
				var table = CreateIncompleteTable();

				table.CreateItem(TableCellType.Column, DummyParent.transform);
			});
		}
		
		[Test]
		public void CreateItem_Should_NotReturnNull_When_ValidTableTriesToInstantiateEmptyCell()
		{
			var table = CreateValidTable();

			var result = table.CreateItem(TableCellType.Empty, DummyParent.transform);
			
			Assert.IsNotNull(result);
		}
		
		[Test]
		public void CreateItem_Should_NotReturnNull_When_ValidTableTriesToInstantiateStaticText()
		{
			var table = CreateValidTable();

			var result = table.CreateItem(TableCellType.StaticText, DummyParent.transform);
			
			Assert.IsNotNull(result);
		}
		
		[Test]
		public void CreateItem_Should_NotReturnNull_When_ValidTableTriesToInstantiateDynamicText()
		{
			var table = CreateValidTable();

			var result = table.CreateItem(TableCellType.DynamicText, DummyParent.transform);
			
			Assert.IsNotNull(result);
		}
		
		[Test]
		public void CreateItem_Should_NotReturnNull_When_ValidTableTriesToInstantiateRow()
		{
			var table = CreateValidTable();

			var result = table.CreateItem(TableCellType.Row, DummyParent.transform);
			
			Assert.IsNotNull(result);
		}
		
		[Test]
		public void CreateItem_Should_NotReturnNull_When_ValidTableTriesToInstantiateColumn()
		{
			var table = CreateValidTable();

			var result = table.CreateItem(TableCellType.Column, DummyParent.transform);
			
			Assert.IsNotNull(result);
		}
		
		[Test]
		public void CreateItem_Should_NotThrowException_When_ParentWasNull()
		{
			Assert.DoesNotThrow(() =>
			{
				var table = CreateValidTable();

				table.CreateItem(TableCellType.Empty, null);
			});
		}
	}
}

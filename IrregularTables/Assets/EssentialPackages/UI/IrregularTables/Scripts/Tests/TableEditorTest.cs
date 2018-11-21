using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using EssentialPackages.UI.IrregularTables.Data;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Object = UnityEngine.Object;

namespace EssentialPackages.UI.IrregularTables.Tests
{
	public class TableEditorTest
	{
		private TableEditor ScriptUnderTest { get; set; }
		private GameObject GameObjectUnderTest { get; set; }
		private Type Type { get; set; }

		private BindingFlags BindingFlags { get; set; }
		private MethodInfo FillTable { get; set; }
		private MethodInfo GetRootItem { get; set; }
		private MethodInfo GetRootData { get; set; }
		private MethodInfo AddItemData { get; set; }

		private TableData CreateDummyTableData()
		{
			var tableData = new TableData();
			var type = typeof(TableData);
			var fieldInfo = type.GetField("_body", BindingFlags);
			fieldInfo.SetValue(tableData, new List<TableCell>());
			return tableData;
		}
		
		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			Type = typeof(TableEditor);
			
			BindingFlags = BindingFlags.NonPublic | BindingFlags.Instance;
			
			FillTable = Type.GetMethod(nameof(FillTable), BindingFlags);
			GetRootItem = Type.GetMethod("GetRootItem", BindingFlags);
			GetRootData = Type.GetMethod(nameof(GetRootData), BindingFlags);
			AddItemData = Type.GetMethod(nameof(AddItemData), BindingFlags);
		}
        
		[SetUp]
		public void SetUp()
		{
			/*var typeName = GetType().ToString();
			GameObjectUnderTest = new GameObject(typeName);
			ScriptUnderTest = GameObjectUnderTest.AddComponent<TableEditor>();

			var tableData = CreateDummyTableData();
			
			const BindingFlags bindingFlags = BindingFlags.NonPublic | BindingFlags.Instance;
			var fieldInfo = Type.GetField("_tableData", bindingFlags);
			fieldInfo.SetValue(ScriptUnderTest, tableData);
			fieldInfo = Type.GetField("_tableBody", bindingFlags);
			fieldInfo.SetValue(ScriptUnderTest, new GameObject().transform);
			fieldInfo = Type.GetField("_style", bindingFlags);
			fieldInfo.SetValue(ScriptUnderTest, ScriptableObject.CreateInstance<TableStyle>());*/
		}
        
		[TearDown]
		public void TearDown()
		{
			ScriptUnderTest = null;
			Object.Destroy(GameObjectUnderTest);
		}

		/*[UnityTest]
		public IEnumerator Awake_ThrowArgumentNullException_When_RootTransformWasNull()
		{
			Assert.Throws<ArgumentNullException>(() =>
				{
					var typeName = GetType().ToString();
					GameObjectUnderTest = new GameObject(typeName);
					ScriptUnderTest = GameObjectUnderTest.AddComponent<TableEditor>();
				}
			);
			yield return null;
		}*/
		
		[UnityTest]
		public IEnumerator Awake_ThrowArgumentNullException_When_StyleWasNull()
		{
			var typeName = GetType().ToString();
			GameObjectUnderTest = new GameObject(typeName);
			GameObjectUnderTest.SetActive(false);
			ScriptUnderTest = GameObjectUnderTest.AddComponent<TableEditor>();

			var tableData = CreateDummyTableData();
			
			const BindingFlags bindingFlags = BindingFlags.NonPublic | BindingFlags.Instance;
			var fieldInfo = Type.GetField("_tableData", bindingFlags);
			fieldInfo.SetValue(ScriptUnderTest, tableData);
			
			fieldInfo = Type.GetField("_tableBody", bindingFlags);
			fieldInfo.SetValue(ScriptUnderTest, new GameObject().transform);
			
			LogAssert.Expect(LogType.Exception, new Regex(@"ArgumentNullException:.*[\s\S].*_style"));
			
			GameObjectUnderTest.SetActive(true);
			
			yield return null;
		}

		/*[UnityTest]
		public IEnumerator GetRootItem_Should_ThrowNullReferenceException_When_RootTransformWasNull()
		{
			
		}*/

		/*[UnityTest]
		public IEnumerator GetRootItem_Should_ThrowNullReferenceException_When_RootTransformWasNull()
		{
			Assert.Throws<NullReferenceException>(() =>
				{
					GetRootItem.Invoke(ScriptUnderTest, null);
				}
			);
			yield return null;
		}*/
	}
}

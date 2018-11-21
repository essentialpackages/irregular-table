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
		
		private static readonly Type Type = typeof(TableEditor);
		
		private const BindingFlags Binding = BindingFlags.NonPublic | BindingFlags.Instance;
		private readonly FieldInfo _tableData = Type.GetField("_tableData", Binding);
		private readonly FieldInfo _tableBody = Type.GetField("_tableBody", Binding);
		private readonly FieldInfo _style = Type.GetField("_style", Binding);
		private MethodInfo _fillTable = Type.GetMethod("FillTable", Binding);
		private MethodInfo _getRootItem = Type.GetMethod("GetRootItem", Binding);
		private MethodInfo _getRootData = Type.GetMethod("GetRootData", Binding);
		private MethodInfo _addItemData = Type.GetMethod("AddItemData", Binding);

		private TableData CreateDummyTableData()
		{
			var tableData = new TableData();
			var type = typeof(TableData);
			var fieldInfo = type.GetField("_body", Binding);
			fieldInfo.SetValue(tableData, new List<TableCell>());
			return tableData;
		}

		private GameObject CreateInactiveGameObject()
		{
			var typeName = GetType().ToString();
			var go = new GameObject(typeName);
			go.SetActive(false);
			return go;
		}

		private TableEditor CreateFullyInitializedScript()
		{
			var gameObjectUnderTest = CreateInactiveGameObject();
			var scriptUnderTest = gameObjectUnderTest.AddComponent<TableEditor>();
			var tableData = CreateDummyTableData();
			
			_tableData.SetValue(scriptUnderTest, tableData);
			_tableBody.SetValue(scriptUnderTest, new GameObject().transform);
			_style.SetValue(scriptUnderTest, ScriptableObject.CreateInstance<TableStyle>());
			
			gameObjectUnderTest.SetActive(true);

			return scriptUnderTest;
		}
		
		[OneTimeSetUp]
		public void OneTimeSetUp()
		{

		}
        
		[SetUp]
		public void SetUp()
		{

		}
        
		[TearDown]
		public void TearDown()
		{
			ScriptUnderTest = null;
			Object.Destroy(GameObjectUnderTest);
		}
		
		[UnityTest]
		public IEnumerator Awake_ThrowArgumentNullException_When_TableBodyWasNull()
		{
			var gameObjectUnderTest = CreateInactiveGameObject();
			gameObjectUnderTest.AddComponent<TableEditor>();
			
			LogAssert.Expect(LogType.Exception, new Regex(@"ArgumentNullException:.*[\s\S].*_tableBody"));
			
			gameObjectUnderTest.SetActive(true);
			
			yield return null;
		}
		
		[UnityTest]
		public IEnumerator Awake_ThrowArgumentNullException_When_StyleWasNull()
		{
			var gameObjectUnderTest = CreateInactiveGameObject();
			var scriptUnderTest = gameObjectUnderTest.AddComponent<TableEditor>();
	
			_tableBody.SetValue(scriptUnderTest, new GameObject().transform);
			
			LogAssert.Expect(LogType.Exception, new Regex(@"ArgumentNullException:.*[\s\S].*_style"));
			
			gameObjectUnderTest.SetActive(true);
			
			yield return null;
		}

		[UnityTest]
		public IEnumerator Awake_Should_ThrowNullReferenceException_When_TableDataWasNull()
		{
			var gameObjectUnderTest = CreateInactiveGameObject();
			var scriptUnderTest = gameObjectUnderTest.AddComponent<TableEditor>();

			_tableBody.SetValue(scriptUnderTest, new GameObject().transform);
			_style.SetValue(scriptUnderTest, ScriptableObject.CreateInstance<TableStyle>());
			
			LogAssert.Expect(LogType.Exception, new Regex(@"NullReferenceException:"));
			
			gameObjectUnderTest.SetActive(true);
			
			yield return null; 
		}
		
		[UnityTest]
		public IEnumerator Awake_Should_Succeed_When_AllFieldsWereInitialized()
		{
			var script = CreateFullyInitializedScript();
			script.gameObject.SetActive(true);
			
			Assert.Pass();
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

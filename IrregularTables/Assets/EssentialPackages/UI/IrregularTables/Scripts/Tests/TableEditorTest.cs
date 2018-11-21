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
	
			var fieldInfo = Type.GetField("_tableBody", BindingFlags);
			fieldInfo.SetValue(scriptUnderTest, new GameObject().transform);
			
			fieldInfo = Type.GetField("_style", BindingFlags);
			fieldInfo.SetValue(scriptUnderTest, ScriptableObject.CreateInstance<TableStyle>());

			var tableData = CreateDummyTableData();
			fieldInfo = Type.GetField("_tableData", BindingFlags);
			fieldInfo.SetValue(scriptUnderTest, tableData);
			
			gameObjectUnderTest.SetActive(true);

			return scriptUnderTest;
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
	
			var fieldInfo = Type.GetField("_tableBody", BindingFlags);
			fieldInfo.SetValue(scriptUnderTest, new GameObject().transform);
			
			LogAssert.Expect(LogType.Exception, new Regex(@"ArgumentNullException:.*[\s\S].*_style"));
			
			gameObjectUnderTest.SetActive(true);
			
			yield return null;
		}

		[UnityTest]
		public IEnumerator Awake_Should_ThrowNullReferenceException_When_TableDataWasNull()
		{
			var gameObjectUnderTest = CreateInactiveGameObject();
			var scriptUnderTest = gameObjectUnderTest.AddComponent<TableEditor>();
	
			var fieldInfo = Type.GetField("_tableBody", BindingFlags);
			fieldInfo.SetValue(scriptUnderTest, new GameObject().transform);
			
			fieldInfo = Type.GetField("_style", BindingFlags);
			fieldInfo.SetValue(scriptUnderTest, ScriptableObject.CreateInstance<TableStyle>());
			
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

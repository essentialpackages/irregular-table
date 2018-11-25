using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using EssentialPackages.UI.IrregularTables.Data;
using EssentialPackages.UI.IrregularTables.Interfaces;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Object = UnityEngine.Object;

namespace EssentialPackages.UI.IrregularTables.Tests
{
	public class TableEditorTest
	{		
		private static readonly Type Type = typeof(TableEditor);
		
		private const BindingFlags Binding = BindingFlags.NonPublic | BindingFlags.Instance;
		private readonly FieldInfo _tableData = typeof(TableProperties).GetField("_tableData", Binding);
		private readonly FieldInfo _tableBody = typeof(TableProperties).GetField("_tableBody", Binding);
		private readonly FieldInfo _style = typeof(TableProperties).GetField("_style", Binding);
		private readonly FieldInfo _properties = Type.GetField("_properties", Binding);
		private readonly MethodInfo _getRootItem = Type.GetMethod("GetRootItem", Binding);

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

			var props = new TableProperties();
			_properties.SetValue(scriptUnderTest, props);
			_tableData.SetValue(props, tableData);
			_tableBody.SetValue(props, new GameObject().transform);
			_style.SetValue(props, ScriptableObject.CreateInstance<TableStyle>());
			
			
			gameObjectUnderTest.SetActive(true);

			return scriptUnderTest;
		}
		
		[UnityTest]
		public IEnumerator Awake_Should_ThrowNullReferenceException_When_PropertiesNotSet()
		{
			var gameObjectUnderTest = CreateInactiveGameObject();

			var scriptUnderTest = gameObjectUnderTest.AddComponent<TableEditor>();
			_properties.SetValue(scriptUnderTest, null);

			scriptUnderTest.gameObject.SetActive(true);

			LogAssert.Expect(LogType.Exception, new Regex(@"NullReferenceException:"));
			
			yield return null;
		}
		
		[UnityTest]
		public IEnumerator Awake_Should_ThrowArgumentNullException_When_TableBodyWasNull()
		{
			var gameObjectUnderTest = CreateInactiveGameObject();
			var scriptUnderTest = gameObjectUnderTest.AddComponent<TableEditor>();
			
			var props = new TableProperties();
			_properties.SetValue(scriptUnderTest, props);
			LogAssert.Expect(LogType.Exception, new Regex(@"ArgumentNullException:.*[\s\S].*TableBody"));
			
			gameObjectUnderTest.SetActive(true);
			
			yield return null;
		}
		
		[UnityTest]
		public IEnumerator Awake_Should_ThrowArgumentNullException_When_StyleWasNull()
		{
			var gameObjectUnderTest = CreateInactiveGameObject();
			var scriptUnderTest = gameObjectUnderTest.AddComponent<TableEditor>();
	
			var props = new TableProperties();
			_properties.SetValue(scriptUnderTest, props);
			_tableBody.SetValue(props, new GameObject().transform);
			
			LogAssert.Expect(LogType.Exception, new Regex(@"ArgumentNullException:.*[\s\S].*TableStyle"));
			
			gameObjectUnderTest.SetActive(true);
			
			yield return null;
		}

		[UnityTest]
		public IEnumerator Awake_Should_ThrowNullReferenceException_When_TableDataWasNull()
		{
			var gameObjectUnderTest = CreateInactiveGameObject();
			var scriptUnderTest = gameObjectUnderTest.AddComponent<TableEditor>();

			var props = new TableProperties();
			_properties.SetValue(scriptUnderTest, props);
			_tableBody.SetValue(props, new GameObject().transform);
			_style.SetValue(props, ScriptableObject.CreateInstance<TableStyle>());
			
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
		
		[UnityTest]
		public IEnumerator GetRootItem_Should_ReturnNull_When_TableBodyHasNoChildren()
		{
			var script = CreateFullyInitializedScript();
			script.gameObject.SetActive(true);
			
			var result = _getRootItem.Invoke(script, null) as Transform;

			Assert.IsNull(result);
			yield return null; 
		}
		
		[UnityTest]
		public IEnumerator GetRootItem_Should_ReturnFirstChildTransform_When_TableBodyHasAtLeastOneChild()
		{
			/*var script = CreateFullyInitializedScript();
			script.gameObject.SetActive(true);*/
			var gameObjectUnderTest = CreateInactiveGameObject();
			var scriptUnderTest = gameObjectUnderTest.AddComponent<TableEditor>();
			var tableData = CreateDummyTableData();

			var props = new TableProperties();
			_properties.SetValue(scriptUnderTest, props);
			_tableData.SetValue(props, tableData);
			_tableBody.SetValue(props, new GameObject().transform);
			_style.SetValue(props, ScriptableObject.CreateInstance<TableStyle>());
			
			
			gameObjectUnderTest.SetActive(true);

			new GameObject().transform.parent = _tableBody.GetValue(props) as Transform;
			
			var result = _getRootItem.Invoke(scriptUnderTest, null) as Transform;

			Assert.IsNotNull(result);
			yield return null; 
		}
	}
}

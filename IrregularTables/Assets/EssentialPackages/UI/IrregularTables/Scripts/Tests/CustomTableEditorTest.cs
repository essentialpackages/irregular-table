using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using EssentialPackages.UI.IrregularTables.Data;
using EssentialPackages.UI.IrregularTables.Interfaces;
using EssentialPackages.UI.TextAdapters.Interfaces;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace EssentialPackages.UI.IrregularTables.Tests
{
	public class CustomTableEditorTest
	{
		private static readonly Type Type = typeof(CustomTableEditor);
		
		private const BindingFlags Binding = BindingFlags.NonPublic | BindingFlags.Instance;
		private readonly FieldInfo _tableData = typeof(TableProperties).GetField("_tableData", Binding);
		private readonly FieldInfo _tableBody = typeof(TableProperties).GetField("_tableBody", Binding);
		private readonly FieldInfo _style = typeof(TableProperties).GetField("_style", Binding);
		private readonly FieldInfo _properties = Type.BaseType.GetField("_properties", Binding);
		private readonly MethodInfo _createCustomRow = Type.GetMethod("CreateCustomRow", Binding);
		private readonly FieldInfo _rowElement = typeof(TableStyle).GetField("_rowElement", Binding);
		private readonly FieldInfo _textElement = typeof(TableStyle).GetField("_textElement", Binding);
		private readonly FieldInfo _body = typeof(TableData).GetField("_body", Binding);

		private GameObject CreateEmptyGameObject()
		{
			return new GameObject();
		}
		
		private TableStyle CreateFakeTableStyle()
		{
			var tableStyle = ScriptableObject.CreateInstance<TableStyle>();
			var go = CreateEmptyGameObject();
			
			_rowElement.SetValue(tableStyle, go);
			_textElement.SetValue(tableStyle, new GameObject("", typeof (FakeScript)));
			
			return tableStyle;
		}

		private class FakeScript : MonoBehaviour, ITextComponent
		{
			public string Id { get; set; }
			public string Text { get; set; }
		}
		
		private TableData CreateDummyTableData()
		{
			var tableData = new TableData();
			_body.SetValue(tableData, new List<TableCell>());
			return tableData;
		}

		private GameObject CreateInactiveGameObject()
		{
			var typeName = GetType().ToString();
			var go = new GameObject(typeName);
			go.SetActive(false);
			return go;
		}
		
		private CustomTableEditor CreateFullyInitializedScript()
		{
			var gameObjectUnderTest = CreateInactiveGameObject();

			var scriptUnderTest = gameObjectUnderTest.AddComponent<CustomTableEditor>();
			var tableData = CreateDummyTableData();

			var tableStyle = CreateFakeTableStyle();
			
			// This is another good example for my tutorial about good & bad code design!
			// I would like to work with a FakeTableStyle implementing ITableStyle, like in TableTest.cs
			// It would reduce coupling and makes testing the condition within the unit test below much easier.
			// Unfortunately I have used [SerializeField] private TableStyle _style; within TableEditor.cs
			// When I would use an interface instead, I could connect the Editor with a custom style within the
			// Inspector. So how to solve this dilemma?
			// All serialized fields could be outsourced to an Data-only MonoBehaviour. It could implement an
			// interface and all its getter function could also return interfaces.
			// I will evaluate this hypothesis in the next few days!
			var go = CreateEmptyGameObject();
			
			var props = new TableProperties();
			_tableData.SetValue(props, tableData);
			_tableBody.SetValue(props, go.transform);
			_style.SetValue(props, tableStyle);
			_properties.SetValue(scriptUnderTest, props);

			return scriptUnderTest;
		}
		
		[UnityTest]
		public IEnumerator Awake_Should_ThrowNullReferenceException_When_PropertiesNotSet()
		{
			var gameObjectUnderTest = CreateInactiveGameObject();

			var scriptUnderTest = gameObjectUnderTest.AddComponent<CustomTableEditor>();
			_properties.SetValue(scriptUnderTest, null);

			scriptUnderTest.gameObject.SetActive(true);

			LogAssert.Expect(LogType.Exception, new Regex(@"NullReferenceException:"));
			
			yield return null;
		}
		
		[UnityTest]
		public IEnumerator Awake_Should_ThrowArgumentNullException_When_TableBodyWasNull()
		{
			var gameObjectUnderTest = CreateInactiveGameObject();
			var scriptUnderTest = gameObjectUnderTest.AddComponent<CustomTableEditor>();
			
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
			var scriptUnderTest = gameObjectUnderTest.AddComponent<CustomTableEditor>();
	
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
			var scriptUnderTest = gameObjectUnderTest.AddComponent<CustomTableEditor>();

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
			var scriptUnderTest = CreateFullyInitializedScript();

			scriptUnderTest.gameObject.SetActive(true);
			
			Assert.Pass();
			yield return null;
		}
		
		[UnityTest]
		public IEnumerator CreateCustomRow_Should_AddFourElementsToTable_When_EmptyStringsWerePassed()
		{
			var scriptUnderTest = CreateFullyInitializedScript();

			scriptUnderTest.gameObject.SetActive(true);
			
			var propertyObject = _properties.GetValue(scriptUnderTest);
			var tableData = _tableData.GetValue(propertyObject);
			var cells = _body.GetValue(tableData) as List<TableCell>;
	
			var last = cells.Count;
			
			scriptUnderTest.CreateCustomRow("", "", "", "", "");
			
			Assert.AreEqual(last + 4, cells.Count);
			
			yield return null;
		}
	}
}

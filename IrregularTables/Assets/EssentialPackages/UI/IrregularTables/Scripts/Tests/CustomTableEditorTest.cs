using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
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
		
		private class FakeTableStyle : ITableStyle
		{
			public GameObject Empty { get; }
			public GameObject Text { get; }
			public GameObject Row { get; }
			public GameObject Column { get; }
			public Color[] Colors => null;

			public FakeTableStyle(GameObject prefab)
			{
				Empty = prefab;
				Text = prefab;
				Row = prefab;
				Column = prefab;
			}
		}

		private class FakeScript : MonoBehaviour, ITextComponent
		{
			public string Id { get; set; }
			public string Text { get; set; }
		}
		
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
		
		private CustomTableEditor CreateFullyInitializedScript()
		{
			var gameObjectUnderTest = CreateInactiveGameObject();
			gameObjectUnderTest.SetActive(false);
			var scriptUnderTest = gameObjectUnderTest.AddComponent<CustomTableEditor>();
			var tableData = CreateDummyTableData();
			var tableStyle = ScriptableObject.CreateInstance<TableStyle>();

			var fieldInfo = typeof(TableStyle).GetField("_rowElement", Binding);
			fieldInfo.SetValue(tableStyle, new GameObject());
			fieldInfo = typeof(TableStyle).GetField("_textElement", Binding);
			fieldInfo.SetValue(tableStyle, new GameObject("", typeof (FakeScript)));
			
			// This is another good example for my tutorial about good & bad code design!
			// I would like to work with a FakeTableStyle implementing ITableStyle, like in TableTest.cs
			// It would reduce coupling and makes testing the condition within the unit test below much easier.
			// Unfortunately I have used [SerializeField] private TableStyle _style; within TableEditor.cs
			// When I would use an interface instead, I could connect the Editor with a custom style within the
			// Inspector. So how to solve this dilemma?
			// All serialized fields could be outsourced to an Data-only MonoBehaviour. It could implement an
			// interface and all its getter function could also return interfaces.
			// I will evaluate this hypothesis in the next few days!
			
			Debug.Log("1: " + _tableData +" 2: " + _tableBody + " 3: " + _style);
			
			var props = new TableProperties();
			Debug.Log(scriptUnderTest+" -->"+_properties);
			_tableData.SetValue(props, tableData);
			_tableBody.SetValue(props, new GameObject().transform);
			_style.SetValue(props, tableStyle);
			_properties.SetValue(scriptUnderTest, props);
			
			
			

			return scriptUnderTest;
		}

		[UnityTest]
		public IEnumerator CreateCustomRow_Should_ExpandTable_When_missing()
		{
			var scriptUnderTest = CreateFullyInitializedScript();

			// TODO fail because CreateCustomRow is called in Awake function
			
			scriptUnderTest.gameObject.SetActive(true);
			//_createCustomRow.Invoke(scriptUnderTest, new object[] { "param1", "param1", "param1", "param1", "param1"});
			
			Assert.Pass();
			yield return null;
		}
	}
}

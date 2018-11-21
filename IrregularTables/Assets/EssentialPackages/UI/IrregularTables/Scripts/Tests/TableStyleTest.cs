using System;
using System.Reflection;
using NUnit.Framework;
using UnityEngine;
using Assert = UnityEngine.Assertions.Assert;

namespace EssentialPackages.UI.IrregularTables.Tests
{
	public class TableStyleTest
	{
		private TableStyle ScriptUnderTest { get; set; }
		private Type Type { get; set; }

		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			Type = typeof(TableStyle);
		}

		[SetUp]
		public void SetUp()
		{
			ScriptUnderTest = ScriptableObject.CreateInstance<TableStyle>();
		}
        
		[TearDown]
		public void TearDown()
		{
			ScriptUnderTest = null;
		}
		
		[Test]
		public void GetEmpty_Should_ReturnNull_When_FieldWasNotConfigured()
		{
			Assert.IsNull(ScriptUnderTest.Empty);
		}
		
		[Test]
		public void GetText_Should_ReturnNull_When_FieldWasNotConfigured()
		{
			Assert.IsNull(ScriptUnderTest.Text);
		}
		
		[Test]
		public void GetRow_Should_ReturnNull_When_FieldWasNotConfigured()
		{
			Assert.IsNull(ScriptUnderTest.Row);
		}
		
		[Test]
		public void GetColumn_Should_ReturnNull_When_FieldWasNotConfigured()
		{
			Assert.IsNull(ScriptUnderTest.Column);
		}
		
		[Test]
		public void GetColors_Should_ReturnNull_When_FieldWasNotConfigured()
		{
			Assert.IsNull(ScriptUnderTest.Colors);
		}
		
		[Test]
		public void GetEmpty_Should_NotReturnNull_When_FieldWasConfigured()
		{
			var fieldInfo = Type.GetField("_emptyElement", BindingFlags.NonPublic | BindingFlags.Instance);
			fieldInfo?.SetValue(ScriptUnderTest, new GameObject());
			
			Assert.IsNotNull(ScriptUnderTest.Empty);
		}
		
		[Test]
		public void GetText_Should_NotReturnNull_When_FieldWasConfigured()
		{
			var fieldInfo = Type.GetField("_textElement", BindingFlags.NonPublic | BindingFlags.Instance);
			fieldInfo?.SetValue(ScriptUnderTest, new GameObject());
			
			Assert.IsNotNull(ScriptUnderTest.Text);
		}
		
		[Test]
		public void GetRow_Should_NotReturnNull_When_FieldWasConfigured()
		{
			var fieldInfo = Type.GetField("_rowElement", BindingFlags.NonPublic | BindingFlags.Instance);
			fieldInfo?.SetValue(ScriptUnderTest, new GameObject());
			
			Assert.IsNotNull(ScriptUnderTest.Row);
		}
		
		[Test]
		public void GetColumn_Should_NotReturnNull_When_FieldWasConfigured()
		{
			var fieldInfo = Type.GetField("_columnElement", BindingFlags.NonPublic | BindingFlags.Instance);
			fieldInfo?.SetValue(ScriptUnderTest, new GameObject());
			
			Assert.IsNotNull(ScriptUnderTest.Column);
		}
		
		[Test]
		public void GetColors_Should_NotReturnNull_When_FieldWasConfigured()
		{
			var fieldInfo = Type.GetField("_rowColors", BindingFlags.NonPublic | BindingFlags.Instance);
			fieldInfo?.SetValue(ScriptUnderTest, new Color[0]);
			
			Assert.IsNotNull(ScriptUnderTest.Colors);
		}
	}
}

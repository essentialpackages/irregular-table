using System;
using System.Collections;
using System.Reflection;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace EssentialPackages.UI.IrregularTables.Tests
{
    public class TableDecoratorTest
    {
        private TableDecorator ScriptUnderTest { get; set; }
        private GameObject GameObjectUnderTest { get; set; }
        private TableStyle TableStyle { get; set; }

        private GameObject AddChildrenToDummyGameObject()
        {
            var child = new GameObject();
            child.GetComponent<Transform>().parent = GameObjectUnderTest.transform;
            return child;
        }
        
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            TableStyle = ScriptableObject.CreateInstance<TableStyle>();

            var type = typeof(TableStyle);
            var fieldInfo = type.GetField("_rowColors", BindingFlags.NonPublic | BindingFlags.Instance);
            fieldInfo?.SetValue(TableStyle, new []
            {
                Color.black, Color.white  
            });
        }
        
        [SetUp]
        public void SetUp()
        {
            var typeName = GetType().ToString();
            GameObjectUnderTest = new GameObject(typeName);
            ScriptUnderTest = GameObjectUnderTest.AddComponent<TableDecorator>();
        }
        
        [TearDown]
        public void TearDown()
        {
            ScriptUnderTest = null;
            Object.Destroy(GameObjectUnderTest);
        }
        
        [UnityTest]
        public IEnumerator UpdateColors_Should_ThrowNullReferenceException_When_RootTransformWasNull()
        {
            Assert.Throws<NullReferenceException>(() =>
                {
                    ScriptUnderTest.UpdateColors(null, TableStyle);
                }
            );
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator UpdateColors_Should_ThrowNullReferenceException_When_TableStyleWasNull()
        {
            Assert.Throws<NullReferenceException>(() =>
                {
                    ScriptUnderTest.UpdateColors(GameObjectUnderTest.transform, null);
                }
            );
            yield return null;
        }

        [UnityTest]
        public IEnumerator UpdateColors_Should_NotHaveAnyEffect_When_NoChildreCouldBeFound()
        {
            ScriptUnderTest.UpdateColors(GameObjectUnderTest.transform, TableStyle);
            
            Assert.Pass();
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator UpdateColors_Should_NotHaveAnyEffect_When_NoImagesWereAttachedToChildren()
        {
            AddChildrenToDummyGameObject();
            ScriptUnderTest.UpdateColors(GameObjectUnderTest.transform, TableStyle);
            
            Assert.Pass();
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator UpdateColors_Should_ChangeImageColors_When_ImagesWereAttachedToChildren()
        {
            var child = AddChildrenToDummyGameObject();
            var image = child.AddComponent<Image>();
            image.color = Color.gray;
            
            ScriptUnderTest.UpdateColors(GameObjectUnderTest.transform, TableStyle);

            var expected = TableStyle.Colors[0];
            Assert.AreEqual(image.color, expected);
            
            yield return null;
        }
    }
}

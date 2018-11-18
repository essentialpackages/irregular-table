using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace EssentialPackages.UI.IrregularTables.Tests
{
    public class TableDecoratorTest
    {
        private GameObject DummyGameObject { get; set; }
        private GameObject ChildGameObject { get; set; }
        private TableDecorator TargetScript { get; set; }
        private TableStyle TableStyle { get; set; }
        private Image ImageComponent { get; set; }

        private void AddChildrenToDummyGameObject()
        {
            ChildGameObject = new GameObject();
            ChildGameObject.GetComponent<Transform>().parent = DummyGameObject.transform;
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
            DummyGameObject = new GameObject(typeName);
            TargetScript = DummyGameObject.AddComponent<TableDecorator>();
        }
        
        [TearDown]
        public void TearDown()
        {
            ImageComponent = null;
            Object.Destroy(ChildGameObject);
            
            TargetScript = null;
            Object.Destroy(DummyGameObject);
        }
        
        [UnityTest]
        public IEnumerator UpdateColors_Should_NotHaveAnyEffect_When_RootTransformWasNull()
        {
            TargetScript.UpdateColors(null, TableStyle);
            
            Assert.Pass();
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator UpdateColors_Should_NotHaveAnyEffect_When_TableStyleWasNull()
        {
            TargetScript.UpdateColors(DummyGameObject.transform, null);
            
            Assert.Pass();
            yield return null;
        }

        [UnityTest]
        public IEnumerator UpdateColors_Should_NotHaveAnyEffect_When_NoChildreCouldBeFound()
        {
            TargetScript.UpdateColors(DummyGameObject.transform, TableStyle);
            
            Assert.Pass();
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator UpdateColors_Should_NotHaveAnyEffect_When_NoImagesWereAttachedToChildren()
        {
            AddChildrenToDummyGameObject();
            TargetScript.UpdateColors(DummyGameObject.transform, TableStyle);
            
            Assert.Pass();
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator UpdateColors_Should_ChangeImageColors_When_ImagesWereAttachedToChildren()
        {
            AddChildrenToDummyGameObject();
            ImageComponent = ChildGameObject.AddComponent<Image>();
            ImageComponent.color = Color.gray;
            
            TargetScript.UpdateColors(DummyGameObject.transform, TableStyle);

            var expected = TableStyle.Colors[0];
            Assert.AreEqual(ImageComponent.color, expected);
            
            yield return null;
        }
    }
}

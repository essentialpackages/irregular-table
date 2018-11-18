using System.Collections;
using System.Reflection;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace EssentialPackages.UI.IrregularTables.Tests
{
    public class TableDecoratorTest
    {
        private GameObject DummyGameObject { get; set; }
        private TableDecorator TargetScript { get; set; }
        private TableStyle TableStyle { get; set; }
        
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
            TargetScript = null;
            Object.Destroy(DummyGameObject);
        }
        
        [UnityTest]
        public IEnumerator UpdateColors_Should_NotHaveAnyEffect_When_RootTransformWasNull() {
            Assert.Fail();
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator UpdateColors_Should_NotHaveAnyEffect_When_TableStyleWasNull() {
            Assert.Fail();
            yield return null;
        }

        [UnityTest]
        public IEnumerator UpdateColors_Should_NotHaveAnyEffect_When_NoChildreCouldBeFound() {
            Assert.Fail();
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator UpdateColors_Should_NotHaveAnyEffect_When_NoImagesWereAttachedToChildren() {
            Assert.Fail();
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator UpdateColors_Should_ChangeImageColors_When_ImagesWereAttachedToChildren() {
            Assert.Fail();
            yield return null;
        }
    }
}

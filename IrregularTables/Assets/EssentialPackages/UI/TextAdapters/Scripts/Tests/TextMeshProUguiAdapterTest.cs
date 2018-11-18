using System.Collections;
using System.Reflection;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace EssentialPackages.UI.TextAdapters.Tests
{
    public class TextMeshProUguiAdapterTest
    {
        private GameObject DummyGameObject { get; set; }
        private TextMeshProUguiAdapter TargetScript { get; set; }
        private TextMeshProUGUI TextComponent { get; set; }
        private string Id { get; set; }
        private string TextContent { get; set; }
        
        private void AttachTargetScriptToDummyGameObject()
        {
            TargetScript = DummyGameObject.AddComponent<TextMeshProUguiAdapter>();
            // When working in the editor, this serialized field would be initialized with an empty string
            TargetScript.Id = "";
        }
        
        private void AttachTextComponentToDummyGameObject()
        {
            TextComponent = DummyGameObject.AddComponent<TextMeshProUGUI>();
            // When working in the editor, this serialized field would be initialized with an empty string
            TextComponent.text = "";
            
            var type = typeof(TextMeshProUguiAdapter);
            var fieldInfo = type.GetField("_text", BindingFlags.NonPublic | BindingFlags.Instance);
            fieldInfo?.SetValue(TargetScript, TextComponent);
        }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Id = "0";
            TextContent = "Lorem Ipsum";
        }
        
        [SetUp]
        public void SetUp()
        {
            var typeName = GetType().ToString();
            DummyGameObject = new GameObject(typeName);
            AttachTargetScriptToDummyGameObject();
        }

        [TearDown]
        public void TearDown()
        {
            TextComponent = null;
            Object.Destroy(DummyGameObject);
        }

        [UnityTest]
        public IEnumerator GetId_Should_ReturnEmptyString_When_IdWasNotSetBefore()
        {
            Assert.IsEmpty(TargetScript.Id);
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator GetId_Should_ReturnNonEmptyString_When_SetIdWasCalledBefore()
        {
            TargetScript.Id = Id;
            Assert.AreEqual(TargetScript.Id, Id);
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator GetText_Should_ReturnNull_When_TextComponentWasNotConnectedBefore()
        {
            var text = TargetScript.Text;
            Assert.Pass();
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator SetText_Should_NotChangeAnything_When_TextComponentWasNotConnectedBefore()
        {
            TargetScript.Text = TextContent;
            Assert.Pass();
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator GetText_Should_ReturnTextContent_When_TextComponentWasConnectedBefore()
        {
            AttachTextComponentToDummyGameObject();
            TextComponent.text = TextContent;
            
            var text = TargetScript.Text;
            
            Assert.AreEqual(text, TextContent);
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator SetText_Should_ChangeTextContent_When_TextComponentWasConnectedBefore()
        {
            AttachTextComponentToDummyGameObject();
            
            TargetScript.Text = TextContent;
            
            Assert.AreEqual(TextComponent.text, TextContent);
            yield return null;
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using Essential.Core.UI.Table.Interfaces;
using TMPro;
using UnityEngine;

namespace Essential.Core.UI.Table
{
	public abstract class TextRegistry<T> : ITextRegistry<T>
	{
		private readonly Dictionary<string, T> _textStore = new Dictionary<string, T>();

		public void Register(string id, T text)
		{
			if (_textStore.ContainsKey(id))
			{
				Debug.LogWarning("Key already in use: " + id);
				return;
			}
			
			_textStore.Add(id, text);
		}

		protected T FindText(string id)
		{
			return _textStore.FirstOrDefault(keyValuePair => keyValuePair.Key == id).Value;
		}

		public abstract void UpdateText(string id, string content);
	}

	public class TextMeshProTextRegistry : TextRegistry<TMP_Text>
	{
		public override void UpdateText(string id, string content)
		{
			var text = FindText(id);
			if (text == null)
			{
				return;
			}

			text.text = content;
		}
	}
}

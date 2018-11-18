using System.Collections.Generic;
using System.Linq;
using EssentialPackages.UI.IrregularTables.Interfaces;
using UnityEngine;

namespace EssentialPackages.UI.IrregularTables.TextRegistration
{
	public abstract class TextRegistryBase<T> : ITextRegistry<T>
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
}

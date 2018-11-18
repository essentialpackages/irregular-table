using UnityEngine;
using UnityEngine.UI;

namespace EssentialPackages.UI.TextAdapters
{
	public class TextAdapter : TextAdapterBase
	{
		[SerializeField] private Text _text;

		public override string Text {
			get
			{
				return (_text == null) ? null : _text.text;
			}
			set
			{
				if (_text != null) _text.text = value;
			}
		}
	}
}

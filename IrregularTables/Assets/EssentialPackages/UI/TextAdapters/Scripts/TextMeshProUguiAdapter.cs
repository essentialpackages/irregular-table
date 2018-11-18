using TMPro;
using UnityEngine;

namespace EssentialPackages.UI.TextAdapters
{
	public class TextMeshProUguiAdapter : TextAdapterBase
	{
		[SerializeField] private TextMeshProUGUI _text;

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

using EssentialPackages.UI.TextAdapters.Interfaces;
using UnityEngine;

namespace EssentialPackages.UI.TextAdapters
{
	public abstract class TextAdapterBase : MonoBehaviour, ITextComponent
	{
		[SerializeField] private string _id;

		public string Id
		{
			get { return _id; }
			set { _id = value; }
		}

		public abstract string Text { get; set; }
	}
}

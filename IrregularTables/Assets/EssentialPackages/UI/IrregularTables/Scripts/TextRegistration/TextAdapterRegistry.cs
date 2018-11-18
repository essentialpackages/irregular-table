using EssentialPackages.UI.TextAdapters.Interfaces;

namespace EssentialPackages.UI.IrregularTables.TextRegistration
{
	public class TextAdapterRegistry : TextRegistryBase<ITextComponent>
	{
		public override void UpdateText(string id, string content)
		{
			var text = FindText(id);
			if (text == null)
			{
				return;
			}

			text.Text = content;
		}
	}
}

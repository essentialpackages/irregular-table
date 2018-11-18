namespace EssentialPackages.UI.IrregularTables.Interfaces
{
	public interface ITextRegistry<in T>
	{
		void Register(string id, T text);
	}
}

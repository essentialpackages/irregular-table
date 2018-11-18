namespace Essential.Core.UI.Table.Interfaces
{
	public interface ITextRegistry<in T>
	{
		void Register(string id, T text);
	}
}

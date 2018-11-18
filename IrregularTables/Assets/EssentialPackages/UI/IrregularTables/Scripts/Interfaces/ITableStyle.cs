using UnityEngine;

namespace EssentialPackages.UI.IrregularTables.Interfaces
{
	public interface ITableStyle
	{
		GameObject Empty { get; }
		GameObject Text { get; }
		GameObject Row { get; }
		GameObject Column { get; }
	}
}

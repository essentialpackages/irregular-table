using UnityEngine;

namespace Essential.Core.UI.Table.Interfaces
{
	public interface ITableStyle
	{
		GameObject Empty { get; }
		GameObject Text { get; }
		GameObject Row { get; }
		GameObject Column { get; }
	}
}

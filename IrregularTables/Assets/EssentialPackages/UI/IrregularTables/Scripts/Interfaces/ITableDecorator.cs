using UnityEngine;

namespace EssentialPackages.UI.IrregularTables.Interfaces
{
	public interface ITableDecorator
	{
		void UpdateColors(Transform rootElement, ITableStyle style);
	}
}

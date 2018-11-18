using UnityEngine;

namespace Essential.Core.UI.Table.Interfaces
{
	public interface ITableDecorator
	{
		void UpdateColors(Transform rootElement, TableStyle style);
	}
}

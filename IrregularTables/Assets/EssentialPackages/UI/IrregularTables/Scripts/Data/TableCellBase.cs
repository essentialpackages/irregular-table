using System;
using UnityEngine;

namespace EssentialPackages.UI.IrregularTables.Data
{
	[Serializable]
	public abstract class TableCellBase<T>
	{
		[SerializeField] private T _id;

		public T Id
		{
			get { return _id; }
			set { _id = value; }
		}
	}
}

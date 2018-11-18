using Essential.Core.UI.Table.Interfaces;
using UnityEngine;

namespace EssentialPackages.UI.IrregularTables
{
    [CreateAssetMenu(fileName = "TableStyle", menuName = "Essential/UI/Table/TableStyle", order = 1)]
    public class TableStyle : ScriptableObject, ITableStyle
    {
        [SerializeField] private GameObject _emptyElement;
        [SerializeField] private GameObject _textElement;
        [SerializeField] private GameObject _rowElement;
        [SerializeField] private GameObject _columnElement;

        [SerializeField] private Color[] _rowColors;

        public GameObject Empty => _emptyElement;
        public GameObject Text => _textElement;
        public GameObject Row => _rowElement;
        public GameObject Column => _columnElement;

        public Color[] Colors => _rowColors;
    }
}

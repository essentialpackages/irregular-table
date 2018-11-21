using System;
using System.Collections.Generic;
using EssentialPackages.UI.IrregularTables.Data;
using EssentialPackages.UI.IrregularTables.Interfaces;
using EssentialPackages.UI.IrregularTables.TextRegistration;
using UnityEngine;

namespace EssentialPackages.UI.IrregularTables
{
    public class TableEditor : MonoBehaviour
    {
        private const int MaxDepth = 10;

        [SerializeField] private TableData _tableData;
        [SerializeField] private Transform _tableBody;
        [SerializeField] private TableStyle _style;

        private ITableDecorator Decorator { get; set; }
        private TableLayout TableLayout { get; set; }

        protected void Awake()
        {
            Initialize();

            Debug.Log(JsonUtility.ToJson(_tableData));
           
            FillTable(new[] {"1"}, _tableBody, 0);
            Decorator?.UpdateColors(GetRootItem(), _style);
        }

        private void Initialize()
        {
            if (_tableBody == null)
            {
                throw new ArgumentNullException(nameof(_tableBody));
            }
            
            if (_style == null)
            {
                throw new ArgumentNullException(nameof(_style));
            }
            
            var table = new Table(_style);
            var textRegistry = new TextAdapterRegistry();
            TableLayout = new TableLayout(table, textRegistry);
            Decorator = GetComponent<ITableDecorator>();
        }

        protected void FillTable(ICollection<string> refs, Transform parent, int depth)
        {
            if (depth > MaxDepth)
            {
                Debug.LogWarning("Max Depth Reached");
                return;
            }

            var cells = _tableData.FindCells(refs);
            TableLayout.ExpandTable(cells, parent, depth + 1, FillTable);
        }

        protected Transform GetRootItem()
        {
            return (_tableBody.childCount == 0) ? null : _tableBody.GetChild(0);
        }
        
        protected TableCell GetRootData()
        {
            return _tableData.GetRootCell();
        }

        protected void AddItemData(string id, TableCellType type, ICollection<string> refs)
        {
            _tableData.AddCell(id, type, refs);
        }
    }
}
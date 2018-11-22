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

        [SerializeField] private TableProperties _properties;

        private ITableDecorator Decorator { get; set; }
        private TableLayout TableLayout { get; set; }

        protected void Awake()
        {
            Initialize();

            Debug.Log(JsonUtility.ToJson(_properties.TableData));
           
            FillTable(new[] {"1"}, _properties.TableBody, 0);
            Decorator?.UpdateColors(GetRootItem(), _properties.TableStyle);
        }

        private void Initialize()
        {
            var body = _properties.TableBody;
            if (body == null)
            {
                throw new ArgumentNullException(nameof(body));
            }

            var style = _properties.TableStyle;
            if (style == null)
            {
                throw new ArgumentNullException(nameof(style));
            }
            
            var table = new Table(style);
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

            var cells = _properties.TableData.FindCells(refs);
            TableLayout.ExpandTable(cells, parent, depth + 1, FillTable);
        }

        protected Transform GetRootItem()
        {
            return (_properties.TableBody.childCount == 0) ? null : _properties.TableBody.GetChild(0);
        }
        
        protected TableCell GetRootData()
        {
            return _properties.TableData.GetRootCell();
        }

        protected void AddItemData(string id, TableCellType type, ICollection<string> refs)
        {
            _properties.TableData.AddCell(id, type, refs);
        }
    }
}
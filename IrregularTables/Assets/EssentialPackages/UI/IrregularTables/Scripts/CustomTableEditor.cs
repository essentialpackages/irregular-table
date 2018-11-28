using System;
using System.Collections;
using EssentialPackages.UI.IrregularTables.Data;
using UnityEngine;
using Random = System.Random;

namespace EssentialPackages.UI.IrregularTables
{
    /// <summary>
    /// Example class about how tables should be extended.
    /// </summary>
    public class CustomTableEditor : TableEditor
    {
        [SerializeField] private TableCellType _type = TableCellType.Row;
        
        protected new void Awake()
        {
            base.Awake();
        }

        private IEnumerator Start()
        {
            var random = new Random();
            
            while(true)
            {
                CreateCustomRow(
                    random.Next().ToString(),
                    random.Next().ToString(),
                    random.Next().ToString(),
                    random.Next().ToString(),
                    random.Next().ToString());
                yield return new WaitForSeconds(1f);
            }
        }
        
        /// <summary>
        /// Example method which creates three groups with overall five columns.
        /// </summary>
        /// <example>
        /// CreateCustomRow("Lorem", "ipsum", "dolor", "sit", "amet");
        /// </example>
        /// <param name="column1">Column 1</param>
        /// <param name="column2">Column 2</param>
        /// <param name="column3">Column 3</param>
        /// <param name="column4">Column 4</param>
        /// <param name="column5">Column 5</param>
        public void CreateCustomRow(string column1, string column2, string column3, string column4,
            string column5)
        {
            var groupId1 = Guid.NewGuid().ToString();
            var groupId2 = Guid.NewGuid().ToString();
            var groupId3 = Guid.NewGuid().ToString();
            AddItemData(groupId1, TableCellType.StaticText, new[] {column1, column2});
            AddItemData(groupId2, TableCellType.DynamicText, new[] {column3});
            AddItemData(groupId3, TableCellType.StaticText, new[] {column4, column5});

            var rowId = Guid.NewGuid().ToString();
            AddItemData(rowId, _type, new[] {groupId1, groupId2, groupId3});

            GetRootData()?.Refs.Add(rowId);

            var parent = GetRootItem();
            FillTable(new[] {rowId}, parent, 1);
        }
    }
}
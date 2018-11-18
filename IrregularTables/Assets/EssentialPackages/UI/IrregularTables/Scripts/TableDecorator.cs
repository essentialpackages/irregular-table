using EssentialPackages.UI.IrregularTables.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace EssentialPackages.UI.IrregularTables
{
    public class TableDecorator : MonoBehaviour, ITableDecorator
    {
        public void UpdateColors(Transform rootElement, TableStyle style)
        {
            var counter = 0;
            var colors = style.Colors;
            foreach (Transform child in rootElement)
            {
                var image = child.GetComponent<Image>();
                if(image != null) image.color = colors[counter++ % colors.Length];
            }
        }
    }
}

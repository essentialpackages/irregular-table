using Essential.Core.UI.Table.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Essential.Core.UI.Table
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

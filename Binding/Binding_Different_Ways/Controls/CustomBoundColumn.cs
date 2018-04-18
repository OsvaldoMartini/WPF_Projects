using System.Windows;
using System.Windows.Controls;

namespace Binding.Different.Ways.Controls
{
    public class CustomBoundColumn : DataGridBoundColumn
    {
        public string TemplateName { get; set; }

        protected override FrameworkElement GenerateElement(DataGridCell cell, object dataItem)
        {
            var binding = new System.Windows.Data.Binding(((System.Windows.Data.Binding) Binding).Path.Path);
            binding.Source = dataItem;

            var content = new ContentControl();
            content.ContentTemplate = (DataTemplate) cell.FindResource(TemplateName);
            content.SetBinding(ContentControl.ContentProperty, binding);
            return content;
        }

        protected override FrameworkElement GenerateEditingElement(DataGridCell cell, object dataItem)
        {
            return GenerateElement(cell, dataItem);
        }
    }
}
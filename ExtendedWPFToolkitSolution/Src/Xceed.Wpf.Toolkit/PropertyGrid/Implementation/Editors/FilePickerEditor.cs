﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Xceed.Wpf.Toolkit.PropertyGrid.Editors
{
    // https://docs.microsoft.com/en-us/dotnet/api/microsoft.win32.openfiledialog?f1url=https%3A%2F%2Fmsdn.microsoft.com%2Fquery%2Fdev15.query%3FappId%3DDev15IDEF1%26l%3DEN-US%26k%3Dk(Microsoft.Win32.OpenFileDialog);k(SolutionItemsProject);k(SolutionItemsProject);k(TargetFrameworkMoniker-.NETFramework,Version%3Dv4.0);k(DevLang-csharp)%26rd%3Dtrue&view=netframework-4.8
    // https://docs.microsoft.com/en-us/windows/uwp/files/quickstart-using-file-and-folder-pickers
    public class FilePickerEditor : Xceed.Wpf.Toolkit.PropertyGrid.Editors.ITypeEditor
    {
        PropertyGridEditorTextBox textBox;
        public FrameworkElement ResolveEditor(Xceed.Wpf.Toolkit.PropertyGrid.PropertyItem propertyItem)
        {
            Grid grd = new Grid();
            var cd1 = new ColumnDefinition();
            cd1.Width = new GridLength(1, GridUnitType.Star);
            var cd2 = new ColumnDefinition();
            cd2.Width = new GridLength(1, GridUnitType.Auto);
            grd.ColumnDefinitions.Add(cd1);
            grd.ColumnDefinitions.Add(cd2);

            textBox = new PropertyGridEditorTextBox();
            textBox.Watermark = "Select file";
            var _binding = new Binding("Value"); //bind to the Value property of the PropertyItem
            _binding.Source = propertyItem;
            _binding.ValidatesOnExceptions = true;
            _binding.ValidatesOnDataErrors = true;
            _binding.Mode = propertyItem.IsReadOnly ? BindingMode.OneWay : BindingMode.TwoWay;
            BindingOperations.SetBinding(textBox, PropertyGridEditorTextBox.TextProperty, _binding);

            Button b = new Button();
            b.Content = "...";
            b.Click += B_Click;

            Grid.SetColumn(textBox, 0);
            Grid.SetColumn(b, 1);

            grd.Children.Add(textBox);
            grd.Children.Add(b);
            return grd;
        }

        private void B_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = ""; // Default file name
            dlg.DefaultExt = ".*"; // Default file extension
            dlg.Filter = "Any file (.*)|*.*"; // Filter files by extension
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                textBox.Text = dlg.FileName;
            }
        }
    }
}

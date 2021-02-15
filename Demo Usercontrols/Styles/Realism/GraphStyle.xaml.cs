using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace Demo_Usercontrols.Styles.Realism
{
	public class HorzLineConv : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			TreeViewItem item = (TreeViewItem)value;
			ItemsControl ic = ItemsControl.ItemsControlFromItemContainer(item);
			int index = ic.ItemContainerGenerator.IndexFromContainer(item);

			if ((string)parameter == "left")
			{
				if (index == 0)	// Either left most or single item
					return (int)0;
				else
					return (int)3;
			}
			else // assume "right"
			{
				if (index == ic.Items.Count - 1)	// Either right most or single item
					return (int)0;
				else
					return (int)3;
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}

	public class VertLineConv : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			TreeViewItem item = (TreeViewItem)value;
			ItemsControl ic = ItemsControl.ItemsControlFromItemContainer(item);
			int index = ic.ItemContainerGenerator.IndexFromContainer(item);

			if ((string)parameter == "top")
			{
				if (ic is TreeView)
					return 0;
				else
					return 3;
			}
			else // assume "bottom"
			{
				if (item.HasItems == false)
					return 0;
				else
					return 3;
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}

	public class DoubleMultiplierConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var originalValue = (double)value;
			var format = new NumberFormatInfo { NumberDecimalSeparator = "." };
			var multiplier = System.Convert.ToDouble(parameter, format);
			return originalValue * multiplier;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}

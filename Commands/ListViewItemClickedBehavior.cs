using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;

namespace BookingApp.Commands
{
    public class ListViewItemClickedBehavior
    {
        public static readonly DependencyProperty CommandProperty =
        DependencyProperty.RegisterAttached("Command", typeof(ICommand), typeof(ListViewItemClickedBehavior), new PropertyMetadata(null, OnCommandChanged));

        public static ICommand GetCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(CommandProperty);
        }

        public static void SetCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(CommandProperty, value);
        }

        private static void OnCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var listViewItem = d as ListViewItem;
            if (listViewItem != null)
            {
                if ((e.NewValue != null) && (e.OldValue == null))
                {
                    listViewItem.PreviewMouseLeftButtonUp += ListViewItem_PreviewMouseLeftButtonUp;
                }
                else if ((e.NewValue == null) && (e.OldValue != null))
                {
                    listViewItem.PreviewMouseLeftButtonUp -= ListViewItem_PreviewMouseLeftButtonUp;
                }
            }
        }

        private static void ListViewItem_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var listViewItem = sender as ListViewItem;
            var command = GetCommand(listViewItem);

            if (command != null && command.CanExecute(listViewItem.DataContext))
            {
                command.Execute(listViewItem.DataContext);
            }
        }
    }
}

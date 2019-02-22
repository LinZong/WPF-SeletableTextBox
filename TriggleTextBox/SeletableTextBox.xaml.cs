using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Linq;

namespace TriggleTextBox
{
    /// <summary>
    /// SeletableTextBox.xaml 的交互逻辑
    /// </summary>
    public partial class SeletableTextBox : UserControl
    {
        public IEnumerable<string> SeletableItems
        {
            get { return (IEnumerable<string>)GetValue(SeletableItemsProperty); }
            set { SetValue(SeletableItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SeletableItems.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SeletableItemsProperty =
            DependencyProperty.Register("SeletableItems", typeof(IEnumerable<string>), typeof(SeletableTextBox), new UIPropertyMetadata(null));

        public bool EnableSearchListItem
        {
            get { return (bool)GetValue(EnableSearchListItemProperty); }
            set { SetValue(EnableSearchListItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EnableSearchListItem.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EnableSearchListItemProperty =
            DependencyProperty.Register("EnableSearchListItem", typeof(bool), typeof(SeletableTextBox), new UIPropertyMetadata(null));


        public SeletableTextBox()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void HideSelectionListBox(object sender, MouseButtonEventArgs e)
        {
            FollowTextBoxListBox.Visibility = Visibility.Collapsed;
            e.Handled = true;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var ChangedInstance = (sender as ListBox);
            if (ChangedInstance.SelectedItem != null)
            {
                SelectedTextBox.Text = ChangedInstance.SelectedItem.ToString();
                (sender as ListBox).Visibility = Visibility.Collapsed;
            }
        }

        private void ShowSelectionListBox(object sender, MouseButtonEventArgs e)
        {
            FollowTextBoxListBox.Visibility = Visibility.Visible;
        }

        private void SearchListBoxOnTextChanged(object sender, TextChangedEventArgs e)
        {
            if(EnableSearchListItem)
            {
                var EnteredText = (sender as TextBox).Text;
                if (string.IsNullOrEmpty(EnteredText)) FollowTextBoxListBox.ItemsSource = SeletableItems;
                else
                {
                    FollowTextBoxListBox.ItemsSource = SeletableItems.Where(x => x.Contains(EnteredText));
                }
            }
        }
    }
}

using System.Linq;
using System.Windows;

namespace Relocation
{
    partial class MainWindow
    {
        public MainWindow() => this.InitializeComponent();

        private void ClearAll_Click(object sender, RoutedEventArgs e)
        {
            foreach (ItemModel item in ((MainViewModel)this.DataContext).Categories.SelectMany(category => category.Items))
            {
                item.IsSelected = false;
            }
        }
    }
}
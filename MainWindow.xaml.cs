using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using NovelTread.Properties;

namespace NovelTread
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public string MyDocuments;
        internal BookManager bkm;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWin_Loaded(object sender, RoutedEventArgs e)
        {
            MyDocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            Properties.Settings.Default.真目录 = MyDocuments + Settings.Default.书架目录;
            /*加载配置文件*/
            bkm = new BookManager();
            foreach (var item in bkm.links)
            {
                BookForm.Children.Add(new TextBlock()
                {
                    Text = item.Value.DisplayName + '\n' + item.Value.Author,
                    TextAlignment = TextAlignment.Center,
                    Width = 100,
                    Height = 100,
                    FontSize = 24
                }) ;
            }
            BookForm.Children.Add(new TextBlock()
            {
                Text = "+",
                TextAlignment = TextAlignment.Center,
                Width = 100,
                Height = 100,
                FontSize = 72
            });
            if (Properties.Settings.Default.首次使用)
                FirstUse();
        }

        private void FirstUse()
        {
            Settings.Default.首次使用 = false;
            if (!Directory.Exists(MyDocuments + Settings.Default.书架目录)){
                Directory.CreateDirectory(MyDocuments + Settings.Default.书架目录);
                MessageBox.Show("书架目录已创建！\n" + MyDocuments + Settings.Default.书架目录, "通知", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}

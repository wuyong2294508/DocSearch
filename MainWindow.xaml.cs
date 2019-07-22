using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using DocSearch.Controller;
using DocSearch.Model;
using System.Data.SQLite;
using System.Data;

namespace DocSearch
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //获取当前工作目录
            string path = AppDomain.CurrentDomain.BaseDirectory;
            string rootpath = path.Substring(0, path.LastIndexOf("bin"));
            string dbPath = rootpath + "DocSearch.db";

            //根据工作目录创建数据库文件
            DBUtils db = new DBUtils();
            db.createNewDatabase(dbPath);
            db.createTable();
        }

        /**
         * 选择目录的按钮点击事件，完成目录选择，搜索文件，显示再左侧listbox中。
         */
        private void BtnSelectPath_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            folder.Description = "请选择一个包含所有目标文件的目录";
            if (folder.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //根据选择的目录递归搜索所有满足条件的文件
                txtPath.Text = folder.SelectedPath;
                FindFiles ff = new FindFiles();
                List<string> fileNames = ff.findFiles(folder.SelectedPath);
                //将文件的完成路径放到左侧的ListBox中显示
                listBoxFileName.ItemsSource = fileNames;
            }
        }

        /**
         * 左侧ListBox的监听事件：选中左侧的item时触发解析文件，将内容显示在右侧的TextBlock中。
         */
        private void ListBoxFileName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            System.Windows.Controls.ListBox lb = (System.Windows.Controls.ListBox)sender;
            string path = lb.SelectedItem.ToString();
            //txtDisplay.Text = path;
            ReadDoc rd = new ReadDoc();
            string content = rd.readContent(path);
            txtDisplay.Text = content;
        }
    }
}

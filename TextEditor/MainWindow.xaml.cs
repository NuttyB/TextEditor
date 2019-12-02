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
using System.Configuration;
using System.IO;
using Microsoft.Win32;

namespace TextEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        string PathFileSqLiteBase = ConfigurationManager.ConnectionStrings["SqLiteBase"].ToString();
        string PathFolderSqLiteBase = ConfigurationManager.ConnectionStrings["SqLitePath"].ToString();
        string PathFileNameSqLiteBase = ConfigurationManager.ConnectionStrings["SqLiteZipName"].ToString();

        public MainWindow()
        {
           
            InitializeComponent();
            //   CompressBase.Compress();
          
    


            if (File.Exists(PathFolderSqLiteBase + PathFileNameSqLiteBase + ".zip") != true)
            {
                
                SqLiteFunc.CreateDb(PathFileSqLiteBase);
            }else
            {

                CompressBase.DeCompress(PathFolderSqLiteBase, PathFileNameSqLiteBase);
                GetLoadList(PathFileSqLiteBase);
               // File.Delete(PathFolderSqLiteBase + PathFileNameSqLiteBase + ".zip");

            }
            

        }
      
        public void GetLoadList (string PathData)
        {



            foreach (string FileName in SqLiteFunc.GetImportedFileList(PathData))
            {
                
                MenuItem fileMenuItem = new MenuItem();
                fileMenuItem.Header = FileName;
               
                fileMenuItem.Click += openMenuItem_Click;
                ListFiles.Items.Add(fileMenuItem);
            }

        }
        public void GetLoadSingle (string Name)
        {


                MenuItem fileMenuItem = new MenuItem();
                fileMenuItem.Header = Name;
               
                fileMenuItem.Click += openMenuItem_Click;
                ListFiles.Items.Add(fileMenuItem);
            

        }

        private void openMenuItem_Click(object sender, System.EventArgs e)
        {
            var myItemsMenuItems = sender as MenuItem;
            


            string d = Convert.ToString(myItemsMenuItems.Header);
            this.Title = d;
            textBox.Text = SqLiteFunc.Open(PathFileSqLiteBase, d);
           

        }

        public void SaveText(object sender, RoutedEventArgs e)
        {
            SaveDialog save = new SaveDialog();
            save.Show();
           
        }
        public void SaveTextToDisk(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "txt|*.txt|xml|*.xml|json|*.json";
            saveFileDialog1.Title = "Save an Image File";
            saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName != "")
            {
                string filename = saveFileDialog1.FileName;
                System.IO.File.WriteAllText(filename, textBox.Text);
                MessageBox.Show("Файл сохранен");

            }
            


        }
        public void OpenTextFromDisk(object sender, RoutedEventArgs e)
        {
           
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            

            openFileDialog1.Filter = "txt|*.txt|xml|*.xml|json|*.json";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName != "")
            {
                this.Title = openFileDialog1.SafeFileName;
                textBox.Text = File.ReadAllText(openFileDialog1.FileName);
            }

        }
        public void CreateNewFile(object sender, RoutedEventArgs e)
        {


            this.Title = "DefaultName";
            textBox.Text = "";


        }



        }
    }

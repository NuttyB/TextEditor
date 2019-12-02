using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TextEditor
{
    /// <summary>
    /// Interaction logic for SaveDialog.xaml
    /// </summary>
    public partial class SaveDialog : Window
    {
        public SaveDialog()
        {
            InitializeComponent();
            
            txtAnswer.Text = ((MainWindow)Application.Current.MainWindow).Title.Split('.')[0];
            FormatList.SelectedIndex = 0;
        }

        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            string pattern = "^[a-zA-Z0-9]+$";



            string d = ((MainWindow)Application.Current.MainWindow).textBox.Text;
            string path = ConfigurationManager.ConnectionStrings["SqLiteBase"].ToString();

            bool result = SqLiteFunc.GetImportedFileList(path).Contains(txtAnswer.Text);
            
                if (Regex.IsMatch(txtAnswer.Text, pattern, RegexOptions.IgnoreCase))
                {
                    if (result == false)
                    {

                        ((MainWindow)Application.Current.MainWindow).GetLoadSingle(txtAnswer.Text + FormatList.Text);

                    }

                    SqLiteFunc.Insert(path, txtAnswer.Text + FormatList.Text, d, FormatList.Text);
                    ((MainWindow)Application.Current.MainWindow).Title = txtAnswer.Text + FormatList.Text;

                    this.Close();
                 
                }
                else
                {
                    MessageBox.Show("Invalid name");
                }
            


            

           

        }

        private void btnDialogCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
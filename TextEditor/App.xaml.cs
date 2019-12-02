using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace TextEditor
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Exit(object sender, ExitEventArgs e)
        {
            
            CompressBase.Compress(ConfigurationManager.ConnectionStrings["SqLiteBase"].ToString(), ConfigurationManager.ConnectionStrings["SqLitePath"].ToString(),ConfigurationManager.ConnectionStrings["SqLiteZipName"].ToString());
        }
    }
}

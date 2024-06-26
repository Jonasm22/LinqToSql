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
namespace LinqToSql
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            LinqToSqlDataClassesDataContext dataContext;
            InitializeComponent();

            // string connectionString = ConfigurationManager.ConnectionString["LinqTo.Sql.Properties.Settings.JonathanDBConnectionString"].ConnectionString;
            string connectionString = ConfigurationManager.ConnectionStrings["LinqToSql.Properties.Settings.JonathanDBConnectionString"].ConnectionString;


            dataContext = new LinqToSqlDataClassesDataContext(connectionString);
        }
    }
}

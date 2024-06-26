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
using LinqToSql.JonathanDBDataSetTableAdapters;
namespace LinqToSql
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Connection Class
        LinqToSqlDataClassesDataContext dataContext;
        public MainWindow()
        {
            InitializeComponent();

            //Code to Connect our Linq with Sql Server.. Important!!
            string connectionString = ConfigurationManager.ConnectionStrings["LinqToSql.Properties.Settings.JonathanDBConnectionString"].ConnectionString;
            dataContext = new LinqToSqlDataClassesDataContext(connectionString);

            // Calling insertUniversities Method

            //insertUniversities();
            InsertStudents();
        }



        public void insertUniversities()
        {
            //Cleaning or deleting duplicate data
            dataContext.ExecuteCommand("delete from University");

            //Add new data to the University table
            University yale = new University(); 
            yale.Name = "Yale";
            dataContext.Universities.InsertOnSubmit(yale);

            
            //Second University
            University HSAugsburg = new University();
            HSAugsburg.Name = ("Ausburg University");
            dataContext.Universities.InsertOnSubmit(HSAugsburg);

            //SumitChanges
            dataContext.SubmitChanges();

            //Display the new data of the University to the table
            MainDataGrid.ItemsSource = dataContext.Universities;
          

            



        }

        public void InsertStudents()
        {

            University yale = dataContext.Universities.First(un => un.Name.Equals("Yale"));
            University HSAugsburg = dataContext.Universities.First(un => un.Name.Equals("Ausburg University"));


            List<Student> students = new List<Student>();

            // initialization of a new student
            students.Add(new Student { 
              
              Name = "Carla",
              Gender = "female", 
              UniversityId = yale.Id 
            });

            students.Add(new Student { 
              
              Name = "Tony",
              Gender = "Male", 
              University = HSAugsburg 
            });

            students.Add(new Student { 
              
              Name = "Leyla",
              Gender = "female", 
              University = yale 
            });

            students.Add(new Student { 
              
              Name = "Lixx",
              Gender = "Transgender", 
              University = HSAugsburg
            });

            dataContext.Students.InsertAllOnSubmit(students);
            //SumitChanges
            dataContext.SubmitChanges();

            //Display the new data of the University to the table
            MainDataGrid.ItemsSource = dataContext.Students;

        }




    }
}

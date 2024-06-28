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
            //InsertStudents();

            //insertLectures();
            //InsertLectureAssociations();
            //GetUniversityOfTony();

            // GetLecturesFromToni();

            //getAllStudentFromAux();
            //GetAllUniversityWithTransgenders();

            //GetAllLectureFromYale();    
            // UpdateToni();

            DeleteLixx();                
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


        public void insertLectures()
        {

            dataContext.Lectures.InsertOnSubmit( new Lecture { Name = "Math"});
            dataContext.Lectures.InsertOnSubmit( new Lecture { Name = "History"});

            dataContext.SubmitChanges();
            MainDataGrid.ItemsSource = dataContext.Lectures;

        }

        public void InsertLectureAssociations()
        {

            Student carla = dataContext.Students.First(st => st.Name.Equals("Carla"));
            Student tony = dataContext.Students.First(st => st.Name.Equals("tony"));
            Student leyle = dataContext.Students.First(st => st.Name.Equals("Leyla"));
            Student jame = dataContext.Students.First(st => st.Name.Equals("Lixx"));

            Lecture Math = dataContext.Lectures.First(lc => lc.Name.Equals("Math"));
            Lecture history = dataContext.Lectures.First(lc => lc.Name.Equals("History"));


            dataContext.StudentLectures.InsertOnSubmit(new StudentLecture { Student = carla, Lecture = Math });
            dataContext.StudentLectures.InsertOnSubmit(new StudentLecture { Student = tony, Lecture = Math });

            StudentLecture slToni = new StudentLecture();
            slToni.Student = tony;
            slToni.LectureId = history.Id;
            dataContext.StudentLectures.InsertOnSubmit(slToni);
            dataContext.StudentLectures.InsertOnSubmit(new StudentLecture { Student = leyle, Lecture = history });


            dataContext.SubmitChanges();

            MainDataGrid.ItemsSource = dataContext.StudentLectures;

        }


        public void GetUniversityOfTony()
        {
            Student tony = dataContext.Students.First(st => st.Name.Equals("Tony"));
            University tonysuniversity = tony.University;

            List<University> uni = new List<University>();
            uni.Add(tonysuniversity);
            MainDataGrid.ItemsSource = uni;


        }

        public void GetLecturesFromToni()
        {

            Student tony = dataContext.Students.First(st => st.Name.Equals("Tony"));

            var tonisLecture = from s1 in tony.StudentLectures select s1.Lecture;

            MainDataGrid.ItemsSource = tonisLecture;

        }


         public void getAllStudentFromAux()
        {
            var stundetFromAux = from student in dataContext.Students
                                 where student.University.Name == "Ausburg University"
                                 select student;

            MainDataGrid.ItemsSource = stundetFromAux;
        }

        public void GetAllUniversityWithTransgenders()
        {

            var transgenderPeople = from student in dataContext.Students
                                    join university in dataContext.Universities
                                    on student.University equals university
                                    where student.Gender == "Transgender"
                                    select university;

            MainDataGrid.ItemsSource = transgenderPeople;
        }


        public void GetAllLectureFromYale()

            //Posible Bug oder Incomplete Datam : the Transgender Stundent currently isnt enrolled to a Lecture
        {
            var lecturesFromYale = from sl in dataContext.StudentLectures
                                   join student in dataContext.Students
                                   on sl.StudentId equals student.Id
                                   where student.University.Name == "Yale"
                                   select sl.Lecture;
            MainDataGrid.ItemsSource = lecturesFromYale;
        }


        //Update InformationData
        public void UpdateToni()
        {

            Student Toni = dataContext.Students.FirstOrDefault(st => st.Name == "tony");
            Toni.Name = "Antonio";
            dataContext.SubmitChanges();

            MainDataGrid.ItemsSource = dataContext.Students;


        }

        //Delete InformationData

        public void DeleteLixx()
        {
            Student Lixx = dataContext.Students.FirstOrDefault(st => st.Name == "Lixx");

            dataContext.Students.DeleteOnSubmit(Lixx);
            dataContext.SubmitChanges();

            MainDataGrid.ItemsSource = dataContext.Students;
        }

    }
}

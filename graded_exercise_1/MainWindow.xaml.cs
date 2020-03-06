using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
using Microsoft.Win32;

namespace graded_exercise_1
{
    public class Person : INotifyPropertyChanged
    {
        public Person(int personID, string name, int age, int score) { PersonID = personID; Name = name; Age = age; Score = score;}
        private int personID;
        public int PersonID
        {
            set
            {
                personID = value;
                NotifyPropertyChanged(nameof(PersonID));
            }
            get { return personID; }
        }
        private string name;
        public string Name
        {
            set
            {
                name = value;
                NotifyPropertyChanged(nameof(Name));
            }
            get { return name; }
        }
        private int age;
        public int Age
        {
            set
            {
                age = value;
                NotifyPropertyChanged(nameof(Age));
            }
            get { return age; }
        }
        private int score;
        public int Score
        {
            set
            {
                score = value;
                NotifyPropertyChanged(nameof(Score));
            }
            get { return score; }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public static List<Person> ReadCSVFile(string filename)
        {
            List<Person> people = new List<Person>();

            using (var file = new StreamReader(filename))
            {
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    var L = line.Split(';');
                    var person = new Person(int.Parse(L[0]),L[1], int.Parse(L[2]), int.Parse(L[3]));
                    people.Add(person);
                }
            }
            return people;
        }

        public string ListBoxToString
    {
        get
        {
            return Name + " (" + PersonID + ")";
        }
    }
}


    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Person> data;
        private DateTime date;
        public MainWindow()
        {
            InitializeComponent();

            data = new List<Person>() {
                new Person(1,"Benny",56,100),
                new Person(2,"Heinz",56,200),
                new Person(3,"Frank",57,132),
                new Person(4,"Torben",66,134),
                new Person(5,"Brian",70,155),
            };

            listBox.ItemsSource = data;
            gridOuter.DataContext = data;
        }

        private void Open_clicked(object sender, RoutedEventArgs e)
        {
            
            OpenFileDialog openFileDialog = new OpenFileDialog();
            
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {

                List<Person> nydata;
                nydata = Person.ReadCSVFile(openFileDialog.FileName);
                date = DateTime.Now;
                listBox.ItemsSource = nydata;
                gridOuter.DataContext = nydata;
                Statusbar.Text = "File opened " + date.ToString()+ " -- Antal personer: "+nydata.Count;
                }
                catch
                {
                Statusbar.Text = "File not opened: The file cannot be loaded, the text input is incorret or double/int are odd";
                }


                
            }
        }
    }
}

using ClassLibr;
using System;
using System.Collections.Generic;
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

namespace SecuritySystem
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Associate> associates;
        private string path;
        public MainWindow()
        {
            InitializeComponent();
            
            mainWindow.Closed += WriteBinaryFile;
            path = Directory.GetCurrentDirectory() + "/worker.txt";
            associates = new List<Associate>();
            ReadBinaryFile();
            //associates = new List<Associate>
            //{
            //    new Associate
            //    {
            //        Name = "Aleksey",
            //        Surname = "Ivanov",
            //        Lastname = "Ivanovich",
            //        Position = "Klerk"
            //    },
            //    new Associate
            //    {
            //        Name = "Petr",
            //        Surname = "Strigulev",
            //        Lastname = "Ivanovich",
            //        Position = "electrician"
            //    },
            //    new Associate
            //    {
            //        Name = "Ivan",
            //        Surname = "Petrov",
            //        Lastname = "Petrovich",
            //        Position = "plumber"
            //    }
            //};
            foreach (var i in associates)
            {
                associateListBox.Items.Add(i.Surname + " " + i.Name + " " + i.Lastname + " - " + i.Position);
            }
        }

        #region MainMethods
        private void DeleteButtonClick(object sender, RoutedEventArgs e)
        {
            int select = associateListBox.SelectedIndex;
            if (select != -1)
            {
                associates.RemoveAt(select);
                associateListBox.Items.RemoveAt(select);
            }
            else
            {
                MessageBox.Show("Пожалуйста выделите сотрудника которого хотите удалить!");
            }
        }

        private void AddButtonClick(object sender, RoutedEventArgs e)
        {
            if (surnameTextBox.Text != "Фамилия" && surnameTextBox.Text != "")
            {
                if (nameTextBox.Text != "Имя" && nameTextBox.Text != "")
                {
                    if (lastnameTextBox.Text != "Отчество" && lastnameTextBox.Text != "")
                    {
                        if (positionTextBox.Text != "Должность" && positionTextBox.Text != "")
                        {
                            Associate temp = new Associate(nameTextBox.Text, surnameTextBox.Text, lastnameTextBox.Text, positionTextBox.Text);
                            associates.Add(temp);
                            associateListBox.Items.Add(temp.Surname + " " + temp.Name + " " + temp.Lastname + " - " + temp.Position);
                        }
                        else { EmptyFieldsMessage(); }
                    }
                    else { EmptyFieldsMessage(); }
                }
                else { EmptyFieldsMessage(); }
            }
            else { EmptyFieldsMessage(); }

            surnameTextBox.Text = "Фамилия";
            nameTextBox.Text = "Имя";
            lastnameTextBox.Text = "Отчество";
            positionTextBox.Text = "Должность";
        }

        private void AttendanceButtonClick(object sender, RoutedEventArgs e)
        {
            new AttendanceWindow(this.mainWindow,associates).Show();
            mainWindow.Close();
        }
        #endregion

        #region SecondMethods
        private void EmptyFieldsMessage()
        {
            MessageBox.Show("Не все поля заполнены правильно!");
        }
        #endregion

        #region BinaryFile
        private void WriteBinaryFile(object sender, EventArgs e)
        {
            // создаем объект BinaryWriter
            using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Create)))
            {
                // записываем в файл значение каждого поля структуры
                foreach (Associate s in associates)
                {
                    writer.Write(s.Name);
                    writer.Write(s.Surname);
                    writer.Write(s.Lastname);
                    writer.Write(s.Position);
                }
            }
        }

        private void ReadBinaryFile()
        {
            using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open)))
            {
                // пока не достигнут конец файла
                // считываем каждое значение из файла
                while (reader.PeekChar() > -1)
                {
                    Associate temp = new Associate();
                    temp.Name = reader.ReadString();
                    temp.Surname = reader.ReadString();
                    temp.Lastname = reader.ReadString();
                    temp.Position = reader.ReadString();
                    associates.Add(temp);
                }
            }
        }
        #endregion
    }
}

using ClassLibr;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Shapes;

namespace SecuritySystem
{
    /// <summary>
    /// Логика взаимодействия для AttendanceWindow.xaml
    /// </summary>
    public partial class AttendanceWindow : Window
    {
        private Window mainWindow;
        private List<Associate> associates;
        private List<Attendance> attendances;
        private string path;
        public AttendanceWindow(Window window,List<Associate> associate)
        {
            InitializeComponent();

            path = Directory.GetCurrentDirectory() + "/workerPresense.txt";
            associates = associate;
            attendances = new List<Attendance>();
            foreach (var i in associates)
            {
                Attendance temp = new Attendance(i.Name, i.Surname, i.Lastname, i.Position, DateTime.Now, false);
                attendances.Add(temp);
            }
            attendanceDataGrid.ItemsSource = attendances;
            
            attendanceWindow.Closed += AttendanceWindowClosed;
        }

        private void AttendanceWindowClosed(object sender, EventArgs e)
        {
            int rowIndex = 0;
            int columnIndex = 5;
            for (int i = 0; i < attendances.Count; i++)
            {
                rowIndex = i+1;
                attendances[i].Presence = (bool)((DataRowView)attendanceDataGrid.Items[rowIndex]).Row[columnIndex];
            }
            WriteBinaryFile();
        }

        #region BinaryFile
        private void WriteBinaryFile()
        {
            // создаем объект BinaryWriter
            using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Create)))
            {
                // записываем в файл значение каждого поля структуры
                foreach (Attendance s in attendances)
                {
                    writer.Write(s.Name);
                    writer.Write(s.Surname);
                    writer.Write(s.Lastname);
                    writer.Write(s.Position);
                    writer.Write(s.Date.ToString());
                    writer.Write(s.Presence);
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
                    Attendance temp = new Attendance();
                    temp.Name = reader.ReadString();
                    temp.Surname = reader.ReadString();
                    temp.Lastname = reader.ReadString();
                    temp.Position = reader.ReadString();
                    temp.Date = DateTime.Parse(reader.ReadString());
                    attendances.Add(temp);
                }
            }
        }
        #endregion
    }
}

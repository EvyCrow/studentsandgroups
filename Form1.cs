using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GroupsAndStudents
{
    public partial class Form1 : Form
    {

        internal static HashSet<Student> students;
        internal static HashSet<Student> studentscopy;
        private string fileName;
        OpenFileDialog openFileDialog1 = new OpenFileDialog();

        public Form1()
        {
            InitializeComponent();
           
            openFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
        }

        //find
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            { MessageBox.Show("Enter student's ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information); }


            try 
            {
                listBox1.Items.Clear();

                foreach (var person in students)
                {
                    string number = Convert.ToString(person.GetNum());
                    if (number.StartsWith(textBox1.Text))
                    { listBox1.Items.Add( Group.GetFullInfo(students, person.GetNum()) + '\n' );}
                }
                if (listBox1.Items.Count == 0)
                { MessageBox.Show("No students found", "Info", MessageBoxButtons.OK, MessageBoxIcon.Question); }
            }
            
            catch (NullReferenceException)
            {
                MessageBox.Show("No data avaliable", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }


        //show all
        private void button5_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

            try
            {
                students.Clear();
                foreach (var person in studentscopy)
                {
                    listBox1.Items.Add( Group.GetFullInfo(studentscopy, person.GetNum()) + '\n');
                    Group.Add(students, person);
                }
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("No data avaliable", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //open file
        private void button6_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fileName = openFileDialog1.FileName;
                string[] text = File.ReadAllLines(fileName);

                if (students != null & studentscopy != null)
                {
                    students.UnionWith(Group.Conv(text));
                    studentscopy.UnionWith(Group.Conv(text));
                }
                else
                {
                    students = Group.Conv(text);
                    studentscopy = Group.Conv(text);
                }


                listBox1.Items.Clear();

                foreach (var person in students)
                {
                    listBox1.Items.Add( Group.GetFullInfo(students, person.GetNum()) + '\n');
                }

            }
        }

        //alphabet sort
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Group.AlphabetSort(students);

                listBox1.Items.Clear();

                foreach (var person in students)
                {
                    listBox1.Items.Add( Group.GetFullInfo(students, person.GetNum()) + '\n' );
                }

            }
            catch (NullReferenceException)
            {
                MessageBox.Show("No data avaliable", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //mark sort
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                Group.MarkSort(students);

                listBox1.Items.Clear();

                foreach (var person in students)
                {
                    listBox1.Items.Add(Group.GetFullInfo(students, person.GetNum()) + '\n');
                }

            }
            catch (NullReferenceException)
            {
                MessageBox.Show("No data avaliable", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //backlogs
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                Group.Academicbacklogs(students);

                listBox1.Items.Clear();

                foreach (var person in students)
                {
                    listBox1.Items.Add( Group.GetFullInfo(students, person.GetNum()) + '\n');
                }

            }
            catch (NullReferenceException)
            {
                MessageBox.Show("No data avaliable", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //add
        private void button7_Click(object sender, EventArgs e)
        {
            Form2 newForm = new Form2();
            newForm.Show();
            
        }
    }
}

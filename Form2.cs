using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Globalization;

namespace GroupsAndStudents
{
    public partial class Form2 : Form
    {
        public static Student student = new Student();
        public Form2()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                student.SetNum(int.Parse(textBox1.Text));
                student.SetSurname(textBox2.Text);
                student.SetName(textBox3.Text);
                student.SetPatronym(textBox4.Text);

                if (checkBox1.Checked)
                    student.SetBudgetInfo(true);
                else student.SetBudgetInfo(false);

                student.SetAverageMark(float.Parse(textBox5.Text, CultureInfo.InvariantCulture.NumberFormat));
                student.SetAcadBacklogNum(int.Parse(textBox6.Text));
                student.SetNotes(textBox7.Text);

                if (Form1.students != null & Form1.studentscopy != null)
                {
                    Group.Add(Form1.students, student);
                    Group.Add(Form1.studentscopy, student);
                }
                else
                {
                    Form1.students = new HashSet<Student> { Form2.student };
                    Form1.studentscopy = new HashSet<Student> { Form2.student };
                }

                this.Close();
            }
            catch (FormatException)
            {
                MessageBox.Show("Invalid data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}

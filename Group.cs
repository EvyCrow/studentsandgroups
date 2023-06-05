using System.Collections.Generic;
using System.Linq;
using System.Globalization;

namespace GroupsAndStudents
{
    internal class Group
    {
        /**********************************************************************
         * 
        !!! Always remember, son! First [int] in Student stands for Student ID !!!

        **********************************************************************/

        //information
        public static string GetFullInfo(HashSet<Student> group, int studnum)
        {
            /*so here you should be careful with parameters
             
             To get full information about student in group you have to know their student ID
            This method requires it as studnum variable
            
             GetFullInfo returns all class object fields in one string
            It could be useful to show all information on the screen, but if you need fields
            as variables, I strongly reccomend to use Student. methods*/

            int num = 0; int i = 0;

            foreach (var person in group)
            {
                if (person.GetNum() == studnum)
                { num = i; break; }
                i++;
            }

            string info = "number - " + group.ElementAt(num).GetNum().ToString()
                + " || surname - "
                + group.ElementAt(num).GetSurname()
                + " || name - "
                + group.ElementAt(num).GetName()
                + " || patronym - "
                + group.ElementAt(num).GetPatronym()
                + "  ||  ";

            if (group.ElementAt(num).GetBudgetInfo() == true) { info += "budgetary basis || avg mark - "; }
            else { info += "contract || avg mark - "; }

            info += group.ElementAt(num).GetAverageMark().ToString()
                + " || backlogs - "
                + group.ElementAt(num).GetAcadBacklogNum().ToString()
                + " || notes: "
                + group.ElementAt(num).GetNotes() + " ";

            return info;
        }



        /****** sorting block ******/



        //sort by surname
        public static void AlphabetSort(HashSet<Student> group)
        {
            //I use List of [Student] objects for easier sorting
            List<Student> sortedlist = new List<Student>();

            /* the main idea of block below is to fill list and to not lose student ID in
               process. For that I convert ID from [int] to [string] and put it in the end
               of surname string, using separator '&'

             As you remember, ID is always [int]

             Later it will be separated from surname. For now it will be contained here,
            to be safe and sound
             
            It's in the end of surname string to sort list correctly*/

            //UPDATE//

            /*I added first name and patronym to surname field
             they will be deleted from there in the process
            
             For now it is used for correct sorting in case of similiar names*/

            foreach (var person in group)
            {
                person.SetSurname(person.GetSurname() + '&' +
                    person.GetName() + '&' + person.GetPatronym() +
                    '&' + person.GetNum().ToString());
                sortedlist.Add(person);
            }
            group.Clear();
            //list sorts from a to z
            sortedlist.Sort((p1, p2) => p1.GetSurname().CompareTo(p2.GetSurname()));

            //and here new dictionary is filled with objects
            foreach (var person in sortedlist)
            {
                //split the string
                string[] surnamenumb = person.GetSurname().Split('&');
                //set surname without numbers
                person.SetSurname(surnamenumb[0]);
                person.SetNum(int.Parse(surnamenumb[3]));
                group.Add(person);
            }
        }

        //sort by marks
        //to get more information - take a look to AlphabetSorting above. It's all the same,
        //except the comparison and sorting is not by surname, but by average grade/mark
        public static void MarkSort(HashSet<Student> group)
        {
            List<Student> sortedlist = new List<Student>();

            foreach (var person in group)
            {
                person.SetSurname(person.GetSurname() + '&' + person.GetNum().ToString());
                sortedlist.Add(person);
            }

            group.Clear();
            //decreasing. Swap p2 and p1 for increasing
            sortedlist.Sort((p1, p2) => p2.GetAverageMark().CompareTo(p1.GetAverageMark()));

            foreach (var person in sortedlist)
            {
                string[] surnamenumb = person.GetSurname().Split('&');
                person.SetSurname(surnamenumb[0]);
                person.SetNum(int.Parse(surnamenumb[1]));
                group.Add(person);
            }
        }

        public delegate void NotAddedDelegate();
        public static event NotAddedDelegate NotAdded;

        //main key is student number. We have to check it
        public static bool Add(HashSet<Student> group, Student student)
        {
            bool permition = true;

            try
            {
                foreach (var person in group)
                {
                    if (person.GetNum() == student.GetNum())
                    { permition = false; }

                    if (permition == false)
                    {
                        //event
                        NotAdded?.Invoke();
                        return false; 
                    }
                }
            }
            catch
            { permition = true; }

             if (permition == true)
             { group.Add(student); }

            return true;
        }

        //list with backlogs
        public static void Academicbacklogs(HashSet<Student> group)
        {
            HashSet<Student> noAcadBcklg = new HashSet<Student>();


            /* if student has no Academic Backlogs, they will be
            added to new list. I use Student. method to make the
            code shorter and easier to read*/

            foreach (var person in group)
            {
                if (!person.GetAcadBacklogBOOL())
                { Group.Add(noAcadBcklg, person); }
            }

            group.ExceptWith(noAcadBcklg);
        }


        //strings to hashset
        public static HashSet<Student> Conv(string[] info)
        {
            HashSet<Student> returning = new HashSet<Student>();
            
            foreach (var line in info)
            {
                Student student = new Student();
                string[] person = line.Split('$');


                try
                {
                    for (int i = 0; i < 8; i++)
                    {
                        switch (i)
                        {
                            case (0):
                                {
                                    student.SetNum(int.Parse(person[i]));
                                    continue;
                                }
                            case (1):
                                {
                                    student.SetSurname(person[i]);
                                    continue;
                                }
                            case (2):
                                {
                                    student.SetName(person[i]);
                                    continue;
                                }
                            case (3):
                                {
                                    student.SetPatronym(person[i]);
                                    continue;
                                }
                            case (4):
                                {
                                    if (person[i] == "1")
                                        student.SetBudgetInfo(true);
                                    else student.SetBudgetInfo(false);
                                    continue;
                                }
                            case (5):
                                {
                                    if (float.Parse(person[i], CultureInfo.InvariantCulture.NumberFormat) >= 2)
                                        student.SetAverageMark(float.Parse(person[i], CultureInfo.InvariantCulture.NumberFormat));
                                    else student.SetAverageMark(2);
                                    continue;
                                }
                            case (6):
                                {
                                    student.SetAcadBacklogNum(int.Parse(person[i]));
                                    continue;
                                }
                            case (7):
                                {
                                    student.SetNotes(person[i]);
                                    continue;
                                }
                        }
                    }
                }
                catch 
                {
                    return returning;
                }

                Group.Add(returning, student);
            }

            return returning;
        }
    } 
}

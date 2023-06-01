namespace GroupsAndStudents
{
    public class Student
    {
        private int studnum;
        private string surname;
        private string name;
        private string patronym;
        //true = budgetary basis | false = contract
        private bool budget = false;
        private float average = 2;
        private int backlognum = 0;
        private string notes = "-";

        public Student()
        {
            int studnum = 0;
            string surname = "";
            string name = "";
            string patronym = "";
            //true = budgetary basis | false = contract
            bool budget = false;
            float average = 2;
            int backlognum = 0;
            string notes = "-";
        }

        public Student(int studnum, string surname, string name, string patronym,
        bool budget, float average, int backlognum, string notes)
        {
            this.studnum = studnum;
            this.surname = surname;
            this.name = name;
            this.patronym = patronym;
            this.budget = budget;
            this.average = average;
            this.backlognum = backlognum;
            this.notes = notes;
        }

        public int GetNum() { return studnum; }
        public string GetName() { return name; }
        public string GetSurname() { return surname; }
        public string GetPatronym() { return patronym; }
        public bool GetBudgetInfo() { return budget; }
        public float GetAverageMark() { return average; }
        public int GetAcadBacklogNum() { return backlognum; }
        public string GetNotes() { return notes; }



        //quick check
        public bool GetAcadBacklogBOOL() 
        {
            if (backlognum != 0)
                return true;
            else return false;
        }


        public void SetNum(int studnum) { this.studnum = studnum; }
        public void SetName(string name) { this.name = name; }
        public void SetSurname(string surname) { this.surname = surname; }
        public void SetPatronym(string patronym) { this.patronym = patronym; }
        public void SetBudgetInfo(bool budget) { this.budget = budget; }
        public void SetAverageMark(float average) { this.average = average; }
        public void SetAcadBacklogNum(int backlognum = 0) { this.backlognum = backlognum; }
        public void SetNotes(string notes = "-") { this.notes = notes; }

    }
}

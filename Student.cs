using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmlAnalyze
{
    public class Student
    {
        public string Name { get; set ; }
        public string Faculty { get; set; }
        public string Adress { get; set; }
        public string Cathedra { get; set; }
        public int CourseYear { get; set; }
        
        public Student()
        {
            Name= string.Empty;
            Faculty= string.Empty;
            Adress= string.Empty;
            Cathedra= string.Empty;
        }
        public Student (string Name,string Faculty,string Adress,string Cathedra,int CourseYear)
        {
            this.Adress = Adress;
            this.Name = Name;
            this.Cathedra = Cathedra;
            this.CourseYear = CourseYear;
            this.Faculty = Faculty;
        }
        static public Student [] GetAllStudents()
        {
            Student[] Dorm = new Student[5];
            Dorm[0] = new Student("Петро", "Математичний", "пл. І. Франкa, 38", "Військова", 3);
            Dorm[1] = new Student("Андрій", "Медичний", "просп. Копиленка, 18", "Військова", 4);
            Dorm[2] = new Student("Юлія", "Юридичний", "вул. П. Орлика, 69", "Rримінального права та процесу", 3);
            Dorm[3] = new Student("Дмитро", "Історичний", "вул. Лук’янівська, 38", "Відсутня", 1);
            Dorm[4] = new Student("Катерина", "Комп'ютерні науки", "просп. Арсенальна, 37", "Моделювання складних систем", 2);
            return Dorm;
        }
    }
}

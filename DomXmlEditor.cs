using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace XmlAnalyze
{
    public class DomXmlEditor : IXmlEditor
    {
        private XmlDocument doc = new XmlDocument();
        public string XmlFilePath { get; set; }
        public DomXmlEditor(string XmlFilePath) 
        { 
            this.XmlFilePath = XmlFilePath;
            doc.Load(XmlFilePath);
        }

        private string [] FindByTagInStudent(string tag)
        {
            XmlNodeList Students = doc.GetElementsByTagName("Student");
            string[] Names = new string[Students.Count];
            int ind = 0;
            foreach (XmlNode node in Students)
            {
                Names[ind] = node[tag].FirstChild.Value;
                ind++;
            }
            Array.Sort(Names);
            return Names;
        } 

        public string[] GetNamesSorted()
        {
            return FindByTagInStudent("Name");   
        }
        public string[] GetFacultiesSorted()
        {
            return FindByTagInStudent("Faculty");
        }
        public string[] GetAdressesSorted()
        {
            return FindByTagInStudent("Adress");
        }
        public string[] GetCathedrasSorted()
        {
            return FindByTagInStudent("Cathedra");
        }
        public int[] GetCourseYearsSorted()
        {
            string[] Result = FindByTagInStudent("CourseYear");
            int Cnt=Result.Length;
            int[] CourseYearsSorted = new int[Cnt];
            for (int i = 0; i < Cnt; ++i) CourseYearsSorted[i] = Convert.ToInt32(Result[i]);
            return CourseYearsSorted;
        }
        public void AddStudentToEndOfXml(Student ToAdd)
        {
            XmlNode stud = doc.CreateElement("Student");

            XmlNode Name = doc.CreateElement("Name");
            XmlText xmlText = doc.CreateTextNode(ToAdd.Name);
            Name.AppendChild(xmlText);
            
            XmlElement Faculty = doc.CreateElement("Faculty");
            xmlText = doc.CreateTextNode(ToAdd.Faculty);
            Faculty.AppendChild(xmlText);

            XmlElement Adress = doc.CreateElement("Adress");
            xmlText = doc.CreateTextNode(ToAdd.Adress);
            Adress.AppendChild(xmlText);

            XmlElement Cathedra = doc.CreateElement("Cathedra");
            xmlText = doc.CreateTextNode(ToAdd.Cathedra);
            Cathedra.AppendChild(xmlText);

            XmlElement CourseYear = doc.CreateElement("CourseYear");
            xmlText = doc.CreateTextNode( Convert.ToString(ToAdd.CourseYear) );
            CourseYear.AppendChild(xmlText);

            stud.AppendChild(Name);
            stud.AppendChild(Faculty);
            stud.AppendChild(Adress);
            stud.AppendChild(Cathedra);
            stud.AppendChild(CourseYear);

            doc["Dorm"].AppendChild(stud);

            doc.Save(XmlFilePath);
        }
        public void RemoveStudentFromEndOfXml()
        {
            if (doc["Dorm"].LastChild == null || doc["Dorm"].ChildNodes.Count==1) return;
            doc["Dorm"].RemoveChild(doc["Dorm"].ChildNodes[doc["Dorm"].ChildNodes.Count-1]);
            doc.Save(XmlFilePath);
        }
    }
}

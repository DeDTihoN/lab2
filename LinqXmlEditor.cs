using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace XmlAnalyze
{
    public class LinqXmlEditor : IXmlEditor
    {
        public string XmlFilePath { get; set; }
        private XDocument XmlFile;
        public LinqXmlEditor(string xmlFilePath)
        {
            XmlFilePath = xmlFilePath;
            XmlFile = XDocument.Load(XmlFilePath);
        }

        private string[] FindByTagInStudent(string Tag)
        {
            return (from student in XmlFile.Descendants("Student") orderby student.Element(Tag).Value ascending select student.Element(Tag).Value).ToArray();
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
            IEnumerable<string> years = FindByTagInStudent("CourseYear");
            int[] YearsSorted = new int[years.Count()];
            for (int i = 0; i < years.Count(); ++i)
            {
                YearsSorted[i] = Convert.ToInt32(years.ElementAt(i));
            }
            return YearsSorted;
        }
        public void AddStudentToEndOfXml(Student ToAdd)
        {
            XElement stud = new XElement("Student",
                   new XElement("Name", ToAdd.Name),
                   new XElement("Faculty", ToAdd.Faculty),
                   new XElement("Adress", ToAdd.Adress),
                   new XElement("Cathedra", ToAdd.Cathedra),
                   new XElement("CourseYear", ToAdd.CourseYear)
                );
            if (XmlFile.Element("Dorm").FirstNode == null)
            {
                XmlFile.Element("Dorm").AddFirst(stud);
                return;
            }
            XmlFile.Element("Dorm").Elements("Student").Where(x=>(x.NextNode==null)).FirstOrDefault().AddAfterSelf(stud);
            XmlFile.Save(XmlFilePath);
            return;
        }
        public void RemoveStudentFromEndOfXml()
        {
            if (XmlFile.Descendants("Student").Count()==0 || XmlFile.Descendants("Student").Count() == 1)
            {
                return;
            }
            XmlFile.Element("Dorm").Elements("Student").Where(x => (x.NextNode == null)).FirstOrDefault().Remove();
            XmlFile.Save(XmlFilePath);
            return;
        }
    }
}

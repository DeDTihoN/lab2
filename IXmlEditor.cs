using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmlAnalyze
{
    public interface IXmlEditor
    {
        public string XmlFilePath { get; set; }
        public string[] GetNamesSorted();
        public string[] GetFacultiesSorted();
        public string[] GetAdressesSorted();
        public string[] GetCathedrasSorted();
        public int[] GetCourseYearsSorted();
        public void AddStudentToEndOfXml(Student ToAdd);
        public void RemoveStudentFromEndOfXml();
    }
}

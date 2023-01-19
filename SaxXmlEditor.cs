using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace XmlAnalyze
{
    public class SaxXmlEditor : IXmlEditor
    {
        public string XmlFilePath { get; set; }
        private XmlReader reader;
        public SaxXmlEditor(string XmlFilePath) 
        {
            this.XmlFilePath = XmlFilePath;
        }
        private string [] FindByTagInStudent(string Tag)
        {
            reader = new XmlTextReader(XmlFilePath);
            List<string> names = new List<string>();
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    if (reader.Name == Tag)
                    {
                        names.Add(reader.ReadInnerXml());
                    }
                }
            }
            string[] Result = new string[names.Count];
            for (int i = 0; i < names.Count; ++i) Result[i] = names[i].ToString();
            Array.Sort(Result);
            return Result;
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
            int Cnt = Result.Length;
            int[] CourseYearsSorted = new int[Cnt];
            for (int i = 0; i < Cnt; ++i) CourseYearsSorted[i] = Convert.ToInt32(Result[i]);
            return CourseYearsSorted;
        }
        public void AddStudentToEndOfXml(Student ToAdd)
        {
            reader = new XmlTextReader(XmlFilePath);
            int Cnt = 0;
            string XmlResult = "";
            foreach (string line in File.ReadLines(XmlFilePath))
            {
                if (line.Contains("Student"))
                {
                    ++Cnt;
                }
            }

            int CurrentCnt = 0;

            foreach (string line in File.ReadLines(XmlFilePath))
            {
                XmlResult += line;
                XmlResult += "\r\n";
                if (line.Contains("Student"))
                {
                    ++CurrentCnt;
                    if (CurrentCnt == Cnt)
                    {
                        XmlResult += $"  <Student>\r\n" +
                            $"    <Name>{ToAdd.Name}</Name>\r\n  " +
                            $"  <Faculty>{ToAdd.Faculty}</Faculty>\r\n  " +
                            $"  <Adress>{ToAdd.Adress}</Adress>\r\n " +
                            $"   <Cathedra>{ToAdd.Cathedra}</Cathedra>\r\n  " +
                            $"  <CourseYear>{ToAdd.CourseYear}</CourseYear>\r\n " +
                            $" </Student>\r\n";
                    }
                }
            }

            using (var writer = new StreamWriter(new FileStream(XmlFilePath, FileMode.Create), Encoding.UTF8))
            {
                writer.Write(XmlResult);
            }

        }
        public void RemoveStudentFromEndOfXml()
        {
            reader = new XmlTextReader(XmlFilePath);
            int Cnt = 0;
            string XmlResult = "";
            foreach (string line in File.ReadLines(XmlFilePath))
            {
                if (line.Contains("Student"))
                {
                    ++Cnt;
                }
            }

            if (Cnt <= 2)
            {
                foreach (string line in File.ReadLines(XmlFilePath))
                {
                    XmlResult += line;
                    XmlResult+= "\r\n";
                }
            }
            else
            {
                int CurrentCnt = 0;
                foreach (string line in File.ReadLines(XmlFilePath))
                {
                    if (CurrentCnt < Cnt - 2)
                    {
                        XmlResult+= line;
                        XmlResult += "\r\n";
                    }
                    else if (CurrentCnt==Cnt-2 && !line.Contains("Student"))
                    {
                        XmlResult+= line;
                        XmlResult += "\r\n";
                    }
                    else if (CurrentCnt == Cnt)
                    {
                        XmlResult+= line;
                        XmlResult += "\r\n";
                    }
                    if (line.Contains("Student"))
                    {
                        ++CurrentCnt;
                    }
                }
            }

            using (var writer = new StreamWriter(new FileStream(XmlFilePath, FileMode.Create), Encoding.UTF8))
            {
                writer.Write(XmlResult);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmlAnalyze
{
    public class XmlEditor
    {
        public const int LinqToXmlStrategy = 1;
        public const int DomStrategy = 2;
        public const int SaxStrategy = 3;

        private int Strategy { get; set; }
        private IXmlEditor Editor {get;set;}
        public string XmlFilePath { get; set; }
        public void SetStrategy(int Strategy)
        {
            this.Strategy = Strategy;
            if (Strategy == LinqToXmlStrategy)
            {
                Editor = new LinqXmlEditor(XmlFilePath);
            }
            else if (Strategy == DomStrategy)
            {
                Editor = new DomXmlEditor(XmlFilePath);
            }
            else if (Strategy == SaxStrategy)
            {
                Editor = new SaxXmlEditor(XmlFilePath);
            }
            else
            {
                throw new Exception("Wrong Strategy for XmlEditor");
            }
        }

        public string GetStrategy()
        {
            if (Strategy == LinqToXmlStrategy)
            {
                return "LinqToXml Strategy";
            }
            else if (Strategy == DomStrategy)
            {
                return "Dom Strategy";
            }
            else if (Strategy == SaxStrategy)
            {
                return "Sax Strategy";
            }
            else
            {
                throw new Exception("Wrong Strategy Setted for XmlEditor");
            }
        }

        public XmlEditor(string XmlFilePath, int Strategy) 
        {
            this.XmlFilePath = XmlFilePath;
            SetStrategy(Strategy);
            return;
        }

        public string[] GetNamesSorted()
        {
            return Editor.GetNamesSorted();
        }
        public string[] GetFacultiesSorted()
        {
            return Editor.GetFacultiesSorted();
        }
        public string[] GetAdressesSorted()
        {
            return Editor.GetAdressesSorted();
        }
        public string[] GetCathedrasSorted()
        {
            return Editor.GetCathedrasSorted();
        }
        public int[] GetCourseYearsSorted()
        {
            return Editor.GetCourseYearsSorted();
        }
        public void AddStudentToEndOfXml(Student ToAdd)
        {
            Editor.AddStudentToEndOfXml(ToAdd);
        }
        public void RemoveStudentFromEndOfXml()
        {
            Editor.RemoveStudentFromEndOfXml();
        }

    }
}

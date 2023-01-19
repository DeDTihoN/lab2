using System.Xml.Linq;
using System.Linq;
using System.Xml.Xsl;
using System.Xml;
using System.Text;
using System.Runtime.Serialization;

namespace XmlAnalyze
{
    internal class Program
    {
        public static string TransformXMLToHTML(string projectDirectory)
        {
            string inputXml = "", xsltString = "";
            inputXml = File.ReadAllText((projectDirectory+"\\Data.xml"));
            xsltString = File.ReadAllText(projectDirectory+"\\XSLT_Transform.xslt");
            XslCompiledTransform transform = new XslCompiledTransform();
            using (XmlReader reader = XmlReader.Create(new StringReader(xsltString)))
            {
                transform.Load(reader);
            }
            StringWriter results = new StringWriter();
            using (XmlReader reader = XmlReader.Create(new StringReader(inputXml)))
            {
                transform.Transform(reader, null, results);
            }
            return results.ToString();
        }
        static void Main(string[] args)
        {
            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            #region CreatingXml
            XDocument xDocument = new XDocument(new XDeclaration("1.0", "utf-8", "yes"),
                new XProcessingInstruction("xsl-stylesheet", "type=\"text/xsl\" href=\"XSLT_Transform.xslt\" media=\"application/xhtml+xml\" ")
                , new XElement("Dorm",
                   from student in Student.GetAllStudents()
                   select new XElement("Student",
                   new XElement("Name", student.Name),
                   new XElement("Faculty", student.Faculty),
                   new XElement("Adress", student.Adress),
                   new XElement("Cathedra", student.Cathedra),
                   new XElement("CourseYear", student.CourseYear)
                )));

            xDocument.Save(projectDirectory + "\\Data.xml");
            #endregion

            XmlEditor Editor = new XmlEditor(projectDirectory+"\\Data.xml",XmlEditor.LinqToXmlStrategy);

            #region ConvertToHtml
            string HtmlResult = TransformXMLToHTML(projectDirectory);
            using (var writer = new StreamWriter(new FileStream(projectDirectory + "\\index.html", FileMode.Create), Encoding.UTF8 ) )
            {
                writer.Write(HtmlResult);
            }
            #endregion
        }
    }

}
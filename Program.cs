using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var appPath = Environment.GetCommandLineArgs()[0];
            var appDirectory = Path.GetDirectoryName(appPath);
            var schemaDirectory = Path.Combine(appDirectory, "schemas");
            var schemaPath = Path.Combine(schemaDirectory, "consStatServ_v4.00.xsd");
            var xmlPath = Path.Combine(appDirectory, "input.xml");
            var stringXml = File.ReadAllText(xmlPath);

            var cfg = new XmlReaderSettings { ValidationType = ValidationType.Schema };
            cfg.Schemas.Add(null, schemaPath);
            XmlReader reader = XmlReader.Create(new StringReader(stringXml), cfg);
            XmlDocument document = new XmlDocument();
            document.Load(reader);
            document.Validate(ValidationEventHandler);
        }

        private static void ValidationEventHandler(object sender, ValidationEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}

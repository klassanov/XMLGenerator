using RazorEngine;
using RazorEngine.Configuration;
using RazorEngine.Templating;
using RazorEngine.Text;
using System;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using XMLGenerator.Model;
using XMLGenerator.Extensions;
using System.Text;

namespace XMLGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            string templateFileName = @"D:\SW Development\Tests\XMLGenerator\XMLGenerator\XMLTemplate\purchaseOrderTemplate.cshtml";
            string outputFileName = @"D:\SW Development\Tests\XMLGenerator\XMLGenerator\Result\purchaseOrder.xml";

            PurchaseOrder order = new PurchaseOrder
            {
                OrderDate = DateTime.Now,
                Price = 1234.65m,
                Quantity = 3,
                ProductName = "Sweet beer"
            };

            string templateFile = File.ReadAllText(templateFileName);

            //Generate the result
            string generatedRazorOutput = Engine.Razor.RunCompile(templateFile, "templateKey", typeof(PurchaseOrder), order);
            StringBuilder sb = FormatXmlString(generatedRazorOutput);

            File.WriteAllText(outputFileName, sb.ToString());

            Console.WriteLine("Done!");
            Console.ReadKey();
        }

        private static StringBuilder FormatXmlString(string generatedRazorOutput)
        {
            //Remove newline characters?
            //string result = generatedRazorOutput.Replace(Environment.NewLine, string.Empty);
            string result = generatedRazorOutput;
            
            //Create xDocument
            XDocument xDocument = XDocument.Parse(result);

            //Trim white spaces from values
            xDocument.Root.TrimWhiteSpaceFromValues();

            StringBuilder sb = new StringBuilder();
            XmlWriterSettings xws = new XmlWriterSettings
            {
                OmitXmlDeclaration = true,
                Indent = true
            };

            //More possibilites for creating the XmlWriter
            //Serialize the document to a string builder
            using (XmlWriter xw = XmlWriter.Create(sb, xws))
            {
                xDocument.Save(xw);
            }

            return sb;
        }
    }
}

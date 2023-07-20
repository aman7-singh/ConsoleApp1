using System;
using System.Data;
using System.Xml;
using System.IO;

namespace Utility
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = @"D:\incisive\xml\parameters.csv";
            if(File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            string Header = $"Parameter Name, Parameter Type, Afected Parameter";
            File.AppendAllText(filePath, Header + Environment.NewLine);

            string ParameterRuleXmlPath = @"D:\incisive\xml\parameterRules.xml";
            //string ParameterRuleXmlPath = @"D:\incisive\xml\test.xml";            

            XmlDocument objXml = new XmlDocument();
            objXml.Load(ParameterRuleXmlPath);
            XmlNodeList objNodes = objXml.GetElementsByTagName("ParameterRules");
            var node = objNodes[0];
            foreach (XmlNode child in node.ChildNodes)
            {
                string name = "", affectedParameter = "", type = "";
                if (child.Name.CompareTo("#comment")==0)
                {
                    continue;
                }
                name = child.Name;
                type = child.Attributes["type"].Value;
                foreach (XmlNode subChild in child.ChildNodes)
                {
                    foreach (XmlNode superChild in subChild.ChildNodes)
                    {
                        if (superChild.Name.CompareTo("AffectedItems") == 0)
                        {
                            affectedParameter = superChild.InnerText.Trim();
                        }
                    }
                }
                string parameterDetail = $"{name} , {type} , {affectedParameter}";
                File.AppendAllText(filePath, parameterDetail + Environment.NewLine);           
            }
        }
    }
}

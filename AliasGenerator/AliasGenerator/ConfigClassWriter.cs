using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliasGenerator
{
    // Class for creating the "Config.cs" code file.
    class ConfigClassWriter
    {
        private string path;
        private string namespaceName;
        private string aliasesNamespaceName;
        private string className;

        private StreamWriter writer;

        private const string TAB = "    ";
        private const string QUALIFIER = "public MFIdentifier ";

        public ConfigClassWriter(string path, string namespaceName, string aliasesNamespaceName, string className)
        {
            this.path = path;
            this.namespaceName = namespaceName;
            this.aliasesNamespaceName = aliasesNamespaceName;
            this.className = className;
        }

        // Must be called first.
        public void Initialize()
        {
            string fileName = path + "\\" + className + ".cs";

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            writer = File.CreateText(fileName);

            writer.WriteLine("using System;");
            writer.WriteLine("using System.Text;");
            writer.WriteLine("using MFilesAPI;");
            writer.WriteLine("using MFiles.VAF;");
            writer.WriteLine("using MFiles.VAF.Common;");
            writer.WriteLine("");
            writer.WriteLine("namespace " + namespaceName);
            writer.WriteLine("{");
            writer.WriteLine(TAB + "public class " + className);
            writer.WriteLine(TAB + "{");
        }

        // Must be called last.
        public void Close()
        {
            writer.WriteLine(TAB + "}");
            writer.WriteLine("}");
            writer.Flush();
            writer.Close();
        }

        // Call as many times as needed in between.
        public void WriteAliases(List<Alias> aliases, string comment)
        {
            writer.WriteLine("");
            writer.WriteLine(TAB + TAB + "// " + comment);
            writer.WriteLine(TAB + TAB + "// *********************************");
            writer.WriteLine("");

            foreach (Alias alias in aliases)
            {
                string aliasConst = namespaceName + "." + alias.ElementAliasConstName;
                writer.WriteLine(TAB + TAB + alias.ElementMFIdentifierAttribute);
                writer.WriteLine(TAB + TAB + QUALIFIER + alias.ElementMFIdentifierName + " = " + this.aliasesNamespaceName + "." + alias.ElementAliasConstName + ";");
                writer.WriteLine("");
            }
        }
    }
}

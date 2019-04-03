using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliasGenerator
{
    // This class is a simple container for storing the details of each alias (mostly for code generation purposes).
    class Alias
    {
        public string ElementName { get; set; }
        public string ElementAlias { get; set; }
        public string ElementAliasConstName { get; set; }
        public string ElementMFIdentifierAttribute { get; set; }
        public string ElementMFIdentifierName { get; set; }

        public const string ELEMENT_TYPE_OT = "Obj.";
        public const string ELEMENT_TYPE_CL = "Class.";
        public const string ELEMENT_TYPE_VL = "VL.";
        public const string ELEMENT_TYPE_PD = "Prop.";
        public const string ELEMENT_TYPE_WF = "WF.";
        public const string ELEMENT_TYPE_WFS = "WF.";

        // Modify these if you e.g. need to make all MFIdentifiers optional.
        private const string MF_IDENTIFIER_OT = "[MFObjType(Required = true)]";
        private const string MF_IDENTIFIER_CL = "[MFClass(Required = true)]";
        private const string MF_IDENTIFIER_VL = "[MFValueList(Required = true)]";
        private const string MF_IDENTIFIER_PD = "[MFPropertyDef(Required = true)]";
        private const string MF_IDENTIFIER_WF = "[MFWorkflow(Required = true)]";
        private const string MF_IDENTIFIER_WFS = "[MFState(Required = true)]";

        // Example usage:
        // Alias newAlias = new Alias(Alias.ELEMENT_TYPE_OT, "Customer", "MF.OT.");
        public Alias(string elementType, string elementName, string aliasPrefix)
        {
            this.ElementName = elementName;
            this.ElementAlias = RemoveDiacriticsAndSpecialChars(aliasPrefix + elementName);
            this.ElementAliasConstName = "ALIAS_" + this.ElementAlias.Replace(".", "_").ToUpper();

            if (elementType == ELEMENT_TYPE_OT)
            {
                this.ElementMFIdentifierAttribute = MF_IDENTIFIER_OT;
                this.ElementMFIdentifierName = ELEMENT_TYPE_OT + "_" + RemoveDiacriticsAndSpecialChars(this.ElementName);
            }
            else if (elementType == ELEMENT_TYPE_CL)
            {
                this.ElementMFIdentifierAttribute = MF_IDENTIFIER_CL;
                this.ElementMFIdentifierName = ELEMENT_TYPE_CL + "_" + RemoveDiacriticsAndSpecialChars(this.ElementName);
            }
            else if (elementType == ELEMENT_TYPE_VL)
            {
                this.ElementMFIdentifierAttribute = MF_IDENTIFIER_VL;
                this.ElementMFIdentifierName = ELEMENT_TYPE_VL + "_" + RemoveDiacriticsAndSpecialChars(this.ElementName);
            }
            else if (elementType == ELEMENT_TYPE_PD)
            {
                this.ElementMFIdentifierAttribute = MF_IDENTIFIER_PD;
                this.ElementMFIdentifierName = ELEMENT_TYPE_PD + "_" + RemoveDiacriticsAndSpecialChars(this.ElementName);
            }
            else if (elementType == ELEMENT_TYPE_WF)
            {
                this.ElementMFIdentifierAttribute = MF_IDENTIFIER_WF;
                this.ElementMFIdentifierName = ELEMENT_TYPE_WF + "_" + RemoveDiacriticsAndSpecialChars(this.ElementName);
            }
            else if (elementType == ELEMENT_TYPE_WFS)
            {
                this.ElementMFIdentifierAttribute = MF_IDENTIFIER_WFS;
                this.ElementMFIdentifierName = ELEMENT_TYPE_WFS + "_" + RemoveDiacriticsAndSpecialChars(this.ElementName);
            }
            else
            {
                this.ElementMFIdentifierAttribute = "// ERROR: unknown element type: " + elementType;
                this.ElementMFIdentifierName = "// ERROR: unknown element type: " + elementType;
            }
        }

        // Removes umlauts from characters like "ä", "ö", etc., and also
        // allows only letters, digits, dots and underscores (to avoid problems when generating the code files).
        // Not perfect, but works for this purpose quite ok...
        private string RemoveDiacriticsAndSpecialChars(string s)
        {
            String normalizedString = s.Normalize(NormalizationForm.FormD);
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < normalizedString.Length; i++)
            {
                Char c = normalizedString[i];
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark
                    && (Char.IsLetterOrDigit(c) || c == '.' || c == '_'))
                    stringBuilder.Append(c);
            }

            return stringBuilder.ToString();
        }
    }
}

using DRAMA.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TRIAL
{
    [TestClass]
    public class StringExtensionsTests
    {
        [TestClass]
        public class ToTitleCase
        {
            [TestMethod]
            public void Text_Is_Converted_To_Title_Case()
            {
                const string text = "Why did the programmer listen to the UNIX shell? So he could hear the C.";
                const string comparisonText = "Why Did The Programmer Listen To The UNIX Shell? So He Could Hear The C.";

                Assert.AreEqual(comparisonText, text.ToTitleCase(), "Text Case Is Title Case");
            }
        }

        [TestClass]
        public class Flatten
        {
            [TestMethod]
            public void Flattened_Text_Has_Leading_And_Trailing_White_Spaces_Removed()
            {
                string text = $"{string.Empty} Why Do Java Programmers Have to Wear Glasses? Because They Can’t C#. {string.Empty}";
                const string comparisonText = "WHY DO JAVA PROGRAMMERS HAVE TO WEAR GLASSES? BECAUSE THEY CAN’T C#.";

                Assert.AreEqual(comparisonText, text.Flatten(), "Flattened Text Has No Leading Or Trailing White-Space Characters");
            }

            [TestMethod]
            public void Flattened_Text_Is_Uppercase()
            {
                const string text = "Why do programmers always mix up Christmas and Halloween? Because Dec 25 is Oct 31.";
                const string comparisonText = "WHY DO PROGRAMMERS ALWAYS MIX UP CHRISTMAS AND HALLOWEEN? BECAUSE DEC 25 IS OCT 31.";

                Assert.AreEqual(comparisonText, text.Flatten(), "Flattened Text Is Uppercase");
            }
        }
    }
}
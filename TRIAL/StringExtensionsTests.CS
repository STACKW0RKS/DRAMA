using DRAMA.Extensions;
using NUnit.Framework;

namespace TRIAL
{
    [TestFixture]
    public class StringExtensionsTests
    {
        [TestFixture]
        public class ToTitleCase
        {
            [Test]
            public void Text_Is_Converted_To_Title_Case()
            {
                const string text = "Why did the programmer listen to the UNIX shell? So he could hear the C.";
                const string comparisonText = "Why Did The Programmer Listen To The UNIX Shell? So He Could Hear The C.";

                Assert.AreEqual(comparisonText, text.ToTitleCase(), "Text Case Is Title Case");
            }
        }

        [TestFixture]
        public class Flatten
        {
            [Test]
            public void Flattened_Text_Has_Leading_And_Trailing_White_Spaces_Removed()
            {
                string text = $"{string.Empty} Why Do Java Programmers Have to Wear Glasses? Because They Can’t C#. {string.Empty}";
                const string comparisonText = "WHY DO JAVA PROGRAMMERS HAVE TO WEAR GLASSES? BECAUSE THEY CAN’T C#.";

                Assert.AreEqual(comparisonText, text.Flatten(), "Flattened Text Has No Leading Or Trailing White-Space Characters");
            }

            [Test]
            public void Flattened_Text_Is_Uppercase()
            {
                const string text = "Why do programmers always mix up Christmas and Halloween? Because Dec 25 is Oct 31.";
                const string comparisonText = "WHY DO PROGRAMMERS ALWAYS MIX UP CHRISTMAS AND HALLOWEEN? BECAUSE DEC 25 IS OCT 31.";

                Assert.AreEqual(comparisonText, text.Flatten(), "Flattened Text Is Uppercase");
            }
        }

        [TestFixture]
        public class ContainsNot
        {
            [Test]
            public void String_Containing_Text_Returns_False()
                => Assert.IsFalse("SLAUGHTER".ContainsNot("LAUGHTER"), "String Does Not Contain Text");

            [Test]
            public void String_Not_Containing_Text_Returns_True()
                => Assert.IsTrue("COFFEE".ContainsNot("GLUTEN"), "String Does Not Contain Text");

            [Test]
            public void String_Containing_Character_Returns_False()
                => Assert.IsFalse("DRAMA".ContainsNot('A'), "String Does Not Contain Character");

            [Test]
            public void String_Not_Containing_Character_Returns_True()
                => Assert.IsTrue("DRAMA".ContainsNot('V'), "String Does Not Contain Character");
        }
    }
}
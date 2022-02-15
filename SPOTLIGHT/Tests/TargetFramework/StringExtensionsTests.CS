namespace SPOTLIGHT.Tests.TargetFramework;

[TestFixture]
public class StringExtensionsTests
{
    [TestFixture]
    public class ToTitleCase
    {
        [Test]
        public void TEXT_IS_CONVERTED_TO_TITLE_CASE()
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
        public void FLATTENED_TEXT_HAS_LEADING_AND_TRAILING_WHITE_SPACES_REMOVED()
        {
            string text = $"{string.Empty} Why Do Java Programmers Have to Wear Glasses? Because They Can’t C#. {string.Empty}";
            const string comparisonText = "WHY DO JAVA PROGRAMMERS HAVE TO WEAR GLASSES? BECAUSE THEY CAN’T C#.";

            Assert.AreEqual(comparisonText, text.Flatten(), "Flattened Text Has No Leading Or Trailing White-Space Characters");
        }

        [Test]
        public void FLATTENED_TEXT_IS_UPPERCASE()
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
        public void STRING_CONTAINING_TEXT_RETURNS_FALSE()
            => Assert.IsFalse("SLAUGHTER".ContainsNot("LAUGHTER"), "String Does Not Contain Text");

        [Test]
        public void STRING_NOT_CONTAINING_TEXT_RETURNS_TRUE()
            => Assert.IsTrue("COFFEE".ContainsNot("GLUTEN"), "String Does Not Contain Text");

        [Test]
        public void STRING_CONTAINING_CHARACTER_RETURNS_FALSE()
            => Assert.IsFalse("DRAMA".ContainsNot('A'), "String Does Not Contain Character");

        [Test]
        public void STRING_NOT_CONTAINING_CHARACTER_RETURNS_TRUE()
            => Assert.IsTrue("DRAMA".ContainsNot('V'), "String Does Not Contain Character");
    }

    // TODO: Add Unit Tests For HasTextContent
}

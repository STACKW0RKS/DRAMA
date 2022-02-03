namespace SPOTLIGHT.Tests.TargetFramework;

[TestFixture]
public class EnumerableExtensionsTests
{
    [TestFixture]
    public class Shuffle
    {
        [Test]
        public void INPUT_COLLECTION_AND_OUTPUT_COLLECTION_HAVE_THE_SAME_ELEMENT_COUNT()
        {
            string[] inputCollection = { "foo", "bar", "qux" };
            string[] outputCollection = inputCollection.Shuffle().ToArray();
            Assert.AreEqual(inputCollection.Length, outputCollection.Length, "Output Collection Element Count");
        }
    }

    [TestFixture]
    public class RandomElement
    {
        [Test]
        public void RANDOMLY_SELECTED_ELEMENT_EXISTS_IN_INPUT_COLLECTION()
        {
            int[] inputCollection = { 42, 13, 666 };
            int randomElement = inputCollection.RandomElement();
            Assert.IsTrue(inputCollection.Contains(randomElement), "Input Collection Contains Randomly Selected Element");
        }
    }

    [TestFixture]
    public class None
    {
        [Test]
        public void NONE_ON_COLLECTION_WITHOUT_ELEMENTS_RETURNS_TRUE()
            => Assert.IsTrue(new List<string>().None(), "Input Collection Is Empty");

        [Test]
        public void NONE_ON_COLLECTION_WITH_ELEMENTS_RETURNS_FALSE()
        {
            List<int> inputCollection = new() { 42, 13, 666 };
            Assert.IsFalse(inputCollection.None(), "Input Collection Is Empty");
        }

        [Test]
        public void NONE_ON_FILTERED_COLLECTION_WITHOUT_ELEMENTS_RETURNS_TRUE()
            => Assert.IsTrue(new List<string>().None(element => element.Equals("foo")), "Filtered Input Collection Is Empty");

        [Test]
        public void NONE_ON_FILTERED_COLLECTION_WITH_ELEMENTS_WHICH_DO_NOT_SATISFY_CONDITION_RETURNS_TRUE()
        {
            List<int> inputCollection = new() { 42, 13, 666 };
            Assert.IsTrue(inputCollection.None(element => element.Equals(163)), "No Elements In Filtered Input Collection Satisfy Condition");
        }

        [Test]
        public void NONE_ON_FILTERED_COLLECTION_WITH_ELEMENTS_WHICH_SATISFY_CONDITION_RETURNS_FALSE()
        {
            List<int> inputCollection = new() { 42, 13, 666 };
            Assert.IsFalse(inputCollection.None(element => element.Equals(42)), "No Elements In Filtered Input Collection Satisfy Condition");
        }
    }
}

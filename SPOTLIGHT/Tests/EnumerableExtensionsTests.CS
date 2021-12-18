namespace SPOTLIGHT.Tests;

[TestFixture]
public class EnumerableExtensionsTests
{
    [TestFixture]
    public class Shuffle
    {
        [Test]
        public void Input_Collection_And_Output_Collection_Have_The_Same_Element_Count()
        {
            string[] inputCollection = { "foo", "bar", "qux" };
            IEnumerable<string> outputCollection = inputCollection.Shuffle();
            Assert.AreEqual(inputCollection.Length, outputCollection.Count(), "Output Collection Element Count");
        }
    }

    [TestFixture]
    public class RandomElement
    {
        [Test]
        public void Randomly_Selected_Element_Exists_In_Input_Collection()
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
        public void None_On_Collection_Without_Elements_Returns_True()
            => Assert.IsTrue(new List<string>().None(), "Input Collection Is Empty");

        [Test]
        public void None_On_Collection_With_Elements_Returns_False()
        {
            List<int> inputCollection = new() { 42, 13, 666 };
            Assert.IsFalse(inputCollection.None(), "Input Collection Is Empty");
        }

        [Test]
        public void None_On_Filtered_Collection_Without_Elements_Returns_True()
            => Assert.IsTrue(new List<string>().None(element => element.Equals("foo")), "Filtered Input Collection Is Empty");

        [Test]
        public void None_On_Filtered_Collection_With_Elements_Which_Do_Not_Satisfy_Condition_Returns_True()
        {
            List<int> inputCollection = new() { 42, 13, 666 };
            Assert.IsTrue(inputCollection.None(element => element.Equals(163)), "No Elements In Filtered Input Collection Satisfy Condition");
        }

        [Test]
        public void None_On_Filtered_Collection_With_Elements_Which_Satisfy_Condition_Returns_False()
        {
            List<int> inputCollection = new() { 42, 13, 666 };
            Assert.IsFalse(inputCollection.None(element => element.Equals(42)), "No Elements In Filtered Input Collection Satisfy Condition");
        }
    }
}

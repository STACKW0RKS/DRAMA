using System.Collections.Generic;
using System.Linq;
using DRAMA.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TRIAL
{
    [TestClass]
    public class IEnumerableExtensionsTests
    {
        [TestClass]
        public class Shuffle
        {
            [TestMethod]
            public void Input_Collection_And_Output_Collection_Have_The_Same_Element_Count()
            {
                string[] inputCollection = {"foo", "bar", "qux"};
                IEnumerable<string> outputCollection = inputCollection.Shuffle();
                Assert.AreEqual(inputCollection.Count(), outputCollection.Count(), "Output Collection Element Count");
            }
        }

        [TestClass]
        public class RandomElement
        {
            [TestMethod]
            public void Randomly_Selected_Element_Exists_In_Input_Collection()
            {
                int[] inputCollection = {42, 13, 666};
                int randomElement = inputCollection.RandomElement();
                Assert.IsTrue(inputCollection.Contains(randomElement), "Input Collection Contains Randomly Selected Element");
            }
        }

        [TestClass]
        public class None
        {
            [TestMethod]
            public void None_On_Collection_Without_Elements_Is_True()
            {
                List<string> inputCollection = new();
                Assert.IsTrue(inputCollection.None(), "Input Collection Is Empty");
            }

            [TestMethod]
            public void None_On_Collection_With_Elements_Is_False()
            {
                List<int> inputCollection = new() {42, 13, 666};
                Assert.IsFalse(inputCollection.None(), "Input Collection Is Not Empty");
            }
        }
    }
}
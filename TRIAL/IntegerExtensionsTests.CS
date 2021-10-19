using DRAMA.Extensions;
using NUnit.Framework;

namespace TRIAL
{
    [TestFixture]
    public class IntegerExtensionsTests
    {
        [TestFixture]
        public class IncrementBy
        {
            [Test]
            public void Increment_By_Correct_Amount_Outputs_Correct_Value()
                => Assert.AreEqual(75, 50.IncrementBy(25), "50 + 25");

            [Test]
            public void Increment_By_Incorrect_Amount_Outputs_Incorrect_Value()
                => Assert.AreNotEqual(25, 50.IncrementBy(25), "50 + 25");
        }

        [TestFixture]
        public class DecrementBy
        {
            [Test]
            public void Decrement_By_Correct_Amount_Outputs_Correct_Value()
                => Assert.AreEqual(25, 50.DecrementBy(25), "50 - 25");

            [Test]
            public void Decrement_By_Incorrect_Amount_Outputs_Incorrect_Value()
                => Assert.AreNotEqual(75, 50.DecrementBy(25), "50 - 25");
        }
    }
}
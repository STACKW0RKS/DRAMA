namespace DRAMA.UnitTests.Tests.TargetFramework;

[TestFixture]
public class IntegerExtensionsTests
{
    [TestFixture]
    public class IncrementBy
    {
        [Test]
        public void INCREMENT_BY_CORRECT_AMOUNT_OUTPUTS_CORRECT_VALUE()
            => Assert.AreEqual(75, 50.IncrementBy(25), "50 + 25");

        [Test]
        public void INCREMENT_BY_INCORRECT_AMOUNT_OUTPUTS_INCORRECT_VALUE()
            => Assert.AreNotEqual(25, 50.IncrementBy(25), "50 + 25");
    }

    [TestFixture]
    public class DecrementBy
    {
        [Test]
        public void DECREMENT_BY_CORRECT_AMOUNT_OUTPUTS_CORRECT_VALUE()
            => Assert.AreEqual(25, 50.DecrementBy(25), "50 - 25");

        [Test]
        public void DECREMENT_BY_INCORRECT_AMOUNT_OUTPUTS_INCORRECT_VALUE()
            => Assert.AreNotEqual(75, 50.DecrementBy(25), "50 - 25");
    }
}

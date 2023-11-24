namespace DRAMA.UnitTests.Tests.TargetFramework;

[TestFixture]
public class JsonElementExtensionsTests
{
    [TestFixture]
    public class Get
    {
        private readonly JsonElement JSON = JsonDocument.Parse("{\"ONE\":{\"ONE\":\"1.1\",\"TWO\":\"1.2\"},\"TWO\":[{\"ONE\":[\"2.1\"]},{\"TWO\":[\"2.2\"]}]}").RootElement;

        [Test]
        public void GET_JSON_ELEMENT_BY_KEY_NAME_WHEN_JSON_ELEMENT_IS_FOUND()
            => Assert.AreEqual("1.1", JSON.Get("ONE")?.Get("ONE")?.GetString(), "JSON Element Retrieved By Key Name");

        [Test]
        public void JSON_ELEMENT_BY_KEY_NAME_IS_NULL_WHEN_JSON_ELEMENT_IS_NOT_FOUND()
            => Assert.IsNull(JSON.Get("SEVEN"), "Non-Existent JSON Element Retrieved By Key Name");

        [Test]
        public void GET_JSON_ELEMENT_BY_ARRAY_INDEX_WHEN_JSON_ELEMENT_IS_FOUND()
            => Assert.AreEqual("2.2", JSON.Get("TWO")?.Get(1)?.Get("TWO")?.Get(0)?.GetString(), "JSON Element Retrieved By Array Index");

        [Test]
        public void JSON_ELEMENT_BY_ARRAY_INDEX_IS_NULL_WHEN_JSON_ELEMENT_IS_NOT_FOUND()
            => Assert.IsNull(JSON.Get("TWO")?.Get(7), "Non-Existent JSON Element Retrieved By Array Index");
    }
}

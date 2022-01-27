namespace SPOTLIGHT.Tests.TargetFramework;

[TestFixture]
public class JsonElementExtensionsTests
{
    [TestFixture]
    public class Get
    {
        private readonly JsonElement JSON = JsonDocument.Parse("{\"ONE\":{\"ONE\":\"1.1\",\"TWO\":\"1.2\"},\"TWO\":[{\"ONE\":[\"2.1\"]},{\"TWO\":[\"2.2\"]}]}").RootElement;

        [Test]
        public void Get_JsonElement_By_Key_Name_When_JSON_Element_Is_Found()
            => Assert.AreEqual("1.1", JSON.Get("ONE")?.Get("ONE")?.GetString(), "JSON Element Retrieved By Key Name");

        [Test]
        public void Get_JsonElement_By_Key_Name_When_JSON_Element_Is_Not_Found()
            => Assert.IsNull(JSON.Get("SEVEN"), "Non-Existent JSON Element Retrieved By Key Name");

        [Test]
        public void Get_JsonElement_By_Array_Index_When_JSON_Element_Is_Found()
            => Assert.AreEqual("2.2", JSON.Get("TWO")?.Get(1)?.Get("TWO")?.Get(0)?.GetString(), "JSON Element Retrieved By Array Index");

        [Test]
        public void Get_JsonElement_By_Array_Index_When_JSON_Element_Is_Not_Found()
            => Assert.IsNull(JSON.Get("TWO")?.Get(7), "Non-Existent JSON Element Retrieved By Array Index");
    }
}

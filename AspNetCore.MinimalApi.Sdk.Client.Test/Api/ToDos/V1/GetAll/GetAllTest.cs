using AspNetCore.Simple.MsTest.Sdk;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AspNetCore.MinimalApi.Sdk.Client.Test.Api.ToDos.V1.GetAll
{
    [TestClass]
    [TestCategory("Todos - V2")]
    public class GetAllTest : ClientTestBase
    {
        [TestMethod]
        public void Simple_Object_Comparison()
        {
            var person1 = new Person("Son", "Goku", 29);
            var person2 = new Person("Muten", "Roshi", 63);

            Assert.That.ObjectsAreEqual(person1, person2, title: "Persons are not equal");
        }

        [TestMethod]
        public async Task Should_Return_All_Todos()
        {
            var result = await AspNetCoreMinimalApiSdkClient.ToDo.V1.GetAllToDosAsync().ConfigureAwait(false);

            Assert.That.ObjectsAreEqual("Api.ToDos.V1.GetAll.Responses.GetAllToDosOkResponse.json", result);
        }

        internal record Person(string Name, string FamilyName, int Age);
    }
}

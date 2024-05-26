using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AspNetCore.MinimalApi.Sdk.Client.Test.Api.ToDos.V1.GetAll
{
    [TestClass]
    [TestCategory("Todos - V2")]
    public class GetAllTest : ClientTestBase
    {
        [TestMethod]
        public async Task Should_Return_All_Todos()
        {
            var result = await AspNetCoreMinimalApiSdkClient.ToDo.V1.GetAllToDosAsync().ConfigureAwait(false);
            Assert.IsTrue(result.Any());
        }
    }
}

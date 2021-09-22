using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using PetWorldAPIConsole.Models;

namespace PetWorldAPIUnitTests
{
    [TestClass]
    public class PetUnitTests
    {
        [TestMethod]
        public void ImportPetTest()
        {
            var petsList = new Pets();
            
            var badJson = "{\"id\": 5179,\"category\": {\"id\": 832809,\"name\": \"PerFold Cat (Experimental Breed - WCF)\"},\"photoUrls\": [],\"tags\": [],\"status\": \"available\"}";
            var goodJson = "{\"id\": 5180,\"category\": {\"id\": 832809,\"name\": \"PerFold Cat (Experimental Breed - WCF)\"},\"name\": \"stephen\",\"photoUrls\": [],\"tags\": [],\"status\": \"available\"}";

            JToken badJsonToken = JToken.Parse(badJson);
            JToken goodJsonToken = JToken.Parse(goodJson);

            petsList.ImportPet(badJsonToken);
            petsList.ImportPet(goodJsonToken);

            var result1 = petsList.pets[0]; // bad json for pet description
            var result2 = petsList.pets[1]; // good json for pet description

            Assert.IsTrue(result1.name == "field name not found");
            Assert.IsTrue(result2.name == "stephen");
        }
    }
}

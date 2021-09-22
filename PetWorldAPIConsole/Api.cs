using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PetWorldAPIConsole.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace PetWorldAPIConsole
{
    public class Api
    {

        //call api to retrieve pets list - available
        public async void RetrievePetsAsync()
        {
            using (var httpClient = new HttpClient())
            {
                var client = new RestClient("https://petstore.swagger.io/v2/pet/findByStatus?status=available");
                var request = new RestRequest(Method.GET);
                request.AddHeader("cache-control", "no-cache");
                IRestResponse response = client.Execute(request);
                JArray jsonArray = JArray.Parse(response.Content);

                Console.WriteLine(String.Format("Retrieved {0} pets from API", jsonArray.Count));

                Pets petList = new Pets();
                foreach(JToken jo in jsonArray) { petList.ImportPet(jo); }
                Console.WriteLine(String.Format("Successfully parsed {0} pets from retrieved list.", petList.pets.Count));

                Console.WriteLine("First 5 pets as retrieved by API");
                for (int i=0; i<5; i++) { Pet p = petList.pets[i]; Console.WriteLine(String.Format("Pet ID: {0}, Name: {1}, Category: {2}", p.id, p.name, p.category.name)); }

                Pets orderedPetList = new Pets(petList.pets.OrderBy(f => f.category.name).ThenByDescending(g => g.name).ToList<Pet>());
                Console.WriteLine("First 50 pets, ordered by Category ASC, Name Desc");
                for (int i = 0; i < 50; i++) { Pet p = orderedPetList.pets[i]; Console.WriteLine(String.Format("Pet ID: {0}, Name: {1}, Category: {2}", p.id, p.name, p.category.name)); }

                Console.ReadLine();
            }
        }
    }

    
}

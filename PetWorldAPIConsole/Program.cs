using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net.Http;
using PetWorldAPIConsole.Models;

namespace PetWorldAPIConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Retrieving all available pets from the store.");

            var _api = new Api();
            _api.RetrievePetsAsync();
        }
    }
}

using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace PetWorldAPIConsole.Models
{
    public class Pets
    {
        public List<Pet> pets { get; set; }

        public Pets()
        {
            pets = new List<Pet>();
        }

        public Pets(List<Pet> petlist)
        {
            pets = petlist;
        }

        public void ImportPet(JToken jo)
        {
            try
            {
                pets.Add(new Pet()
                {
                    id = long.Parse(jo["id"].ToString()),
                    category = new PetCategory(jo["category"]),
                    name = ParseJsonSafely(jo, "name"),
                    photoUrls = ImportPhotos(jo["photoUrls"]),
                    tags = ImportTags(jo["tags"]),
                    status = ParseJsonSafely(jo, "status")
                });
            }
            catch (Exception e) { }
        }

        private string ParseJsonSafely(JToken jo, string value)
        {
            if (jo[value] == null) { return string.Format("field {0} not found", value); } // string.Empty; }
            return jo[value].ToString();
        }

        private List<string> ImportPhotos(JToken photos)
        {
            if (photos == null) { return new List<string>(); }
            List<string> photoList = new List<string>();
            foreach(JToken jt in photos.Values()) { photoList.Add(jt.ToString()); }
            return photoList;
        }
        private List<string> ImportTags(JToken tags)
        {
            if (tags == null) { return new List<string>(); }
            List<string> tagsList = new List<string>();
            foreach (JToken jt in tags.Values()) { tagsList.Add(jt.ToString()); }
            return tagsList;
        }
    }

    public class Pet
    {
        public long id { get; set; }
        public PetCategory category { get; set; }
        public string name { get; set; }
        public List<string> photoUrls { get; set; }
        public List<string> tags { get; set; }
        public string status { get; set; }
    }

    public class PetCategory
    {
        public long id { get; set; }
        public string name { get; set; }

        public PetCategory(JToken category)
        {
            if (category == null) { name = "unknown-notset"; return; }
            if (category["id"] == null) { name = "unknown-idnotset"; return; }
            id = long.Parse(category["id"].ToString());
            if (category["name"] == null) { name = "unknown-namenotset"; return; }
            name = category["name"].ToString();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetDevMob.Models
{
    public class Enregistrement
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Tag { get; set; }
        public string ImageName { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Adress { get; set; }

        public Enregistrement(string name, string description, string tag, string imageName, double latitude, double longitude, string adress)
        {
            Name = name;
            Description = description;
            Tag = tag;
            ImageName = imageName;
            Latitude = latitude;
            Longitude = longitude;
            Adress = adress; 
        }

        public Enregistrement()
        {

        }
    }
}

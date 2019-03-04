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
        public string Coordonee { get; set; }
        public string Adress { get; set; }

        public Enregistrement(string name, string description, string tag, string coordonee, string adress)
        {
            Name = name;
            Description = description;
            Tag = tag;
            Coordonee = coordonee;
            Adress = adress; 
        }

        public Enregistrement()
        {

        }
    }
}

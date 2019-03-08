using ProjetDevMob.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetDevMob.Services
{
    public interface IEnregistrementService
    {
        List<Enregistrement> GetEnregistrements();

        void AddEnregistrement(Enregistrement enreg);

        void DeleteEnregistrement(Enregistrement enreg);

        Enregistrement GetEnregistrement(int pos);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjetDevMob.Client;
using ProjetDevMob.Models;

namespace ProjetDevMob.Services
{
    public class EnregistrementService : IEnregistrementService
    {

        private List<Enregistrement> _enregs;
        private ILiteDBClient _liteDBClient;
        private string _dbCollectionEnreg = "collectionEnreg";


        public EnregistrementService(ILiteDBClient liteDBClient)
        {

            _liteDBClient = liteDBClient;

            _enregs = new List<Enregistrement>();

            //InitLocal();
            //FirstInsertDB();
            Init();

        }

        private void Init()
        {
            _enregs = _liteDBClient.GetCollectionFromDB<Enregistrement>(_dbCollectionEnreg);
        }


        public void AddEnregistrement(Enregistrement enreg)
        {
            _liteDBClient.InsertObjectInDB<Enregistrement>(enreg, _dbCollectionEnreg);
            _enregs.Add(enreg);
        }

        public void DeleteEnregistrement(Enregistrement enreg)
        {
            _liteDBClient.RemoveObjectFromDB<Enregistrement>(enreg.Id, _dbCollectionEnreg);
            _enregs.Remove(enreg);
        }

        public Enregistrement GetEnregistrement(int pos)
        {
            return _enregs.ElementAt(pos);
        }

        public List<Enregistrement> GetEnregistrements()
        {
            return _enregs;
        }
    }
}

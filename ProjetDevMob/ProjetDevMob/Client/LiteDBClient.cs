﻿using LiteDB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ProjetDevMob.Client
{
    class LiteDBClient : ILiteDBClient
    {
        private string _dbPath;

        public LiteDBClient()
        {
            var docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            _dbPath = Path.Combine(docPath, "MyDB.db");
        }



        public void CleanCollection(string collectionName)
        {
            using (var db = new LiteDatabase(_dbPath))
            {
                db.DropCollection(collectionName);
            }
        }

        public List<TObject> GetCollectionFromDB<TObject>(string collectionName) where TObject : class
        {
            List<TObject> list = new List<TObject>();

            using (var db = new LiteDatabase(_dbPath))
            {
                var collection = db.GetCollection<TObject>(collectionName);
                var elements = collection.FindAll();
                foreach (TObject element in elements)
                {
                    list.Add(element);
                }
            }

            return list;
        }

        public void InsertObjectInDB<TObject>(TObject objectToInsert, string collectionName) where TObject : class
        {
            using (var db = new LiteDatabase(_dbPath))
            {
                var collection = db.GetCollection<TObject>(collectionName);
                collection.Insert(objectToInsert);
                var doc = db.GetCollection(collectionName);
                var elements = doc.FindAll();
            }
        }

        public void RemoveObjectFromDB<TObject>(int objectToRemoveID, string collectionName) where TObject : class
        {
            using (var db = new LiteDatabase(_dbPath))
            {
                var collection = db.GetCollection<TObject>(collectionName);
                collection.Delete(objectToRemoveID);
            }
        }
    }
}

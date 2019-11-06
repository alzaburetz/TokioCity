using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using LiteDB;
using TokioCity.Models;

namespace TokioCity.Services
{
    public class DataBaseService: IDataBase
    {
        public string DataBasePath { get; set; }
        private LiteDatabase database;
        public void RemoveAll<T>(string collection)
        {
            database.GetCollection<T>(collection).Delete(Query.All());
        }

        public IEnumerable<T> GetAllStream<T>(string collection)
        {
            IEnumerable<T> result;
            var items = database.GetCollection<T>(collection).FindAll();
            while (items.GetEnumerator().MoveNext())
            {
                yield return items.GetEnumerator().Current;
            }
        }

        public int GetRecordCount<T>(string collection)
        {
            return database.GetCollection<T>(collection).Count();
        }

        public List<T> GetAll<T>(string collection)
        {
            List<T> result = new List<T>();
            var items = database.GetCollection<T>(collection).FindAll();
            while (items.GetEnumerator().MoveNext())
            {
                result.Add(items.GetEnumerator().Current);
            }
            return result;
        }

        public List<T> GetByQuery<T>(string collection, Query query)
        {
            List<T> result = new List<T>();
            var list = database.GetCollection<T>(collection);
            var found = list.Find(query);
            while (found.GetEnumerator().MoveNext())
            {
                result.Add(found.GetEnumerator().Current);
            }
            return result;
        }

        public IEnumerator<T> GetByQueryEnumerable<T>(string collection, Query query)
        {
            var enumeration = database.GetCollection<T>(collection)
                .Find(query)
                .GetEnumerator();
            while (enumeration.MoveNext())
                yield return enumeration.Current;
        }

        public void WriteAll<T>(string collection, List<T> items)
        {
            RemoveAll<T>(collection);
            var Items = database.GetCollection<T>(collection).IncludeAll(3);
            Items.Delete(Query.All());
            Items.InsertBulk(items);
        }

        public T GetOneOfCategory<T>(string collection, int category)
        {
            Func<BsonValue, bool> predicate = x => x.AsArray.Contains(category);
            return database.GetCollection<T>(collection).FindOne(Query.Where("category", predicate));
        }

        public T GetProduct<T>(string collection, string uid)
        {
            return database.GetCollection<T>(collection).FindOne(Query.EQ("uid", uid));
        }

        public IEnumerator<T> GetOneItem<T>(string collection, Query query = null)
        {
            return database.GetCollection<T>(collection).Find(Query.Where("id",x => x.AsInt32 != 0)).GetEnumerator();
        }

        public DataBaseService()
        {
            DataBasePath = Path
            .Combine(System
            .Environment
            .GetFolderPath(System
                .Environment
                .SpecialFolder
                .Personal), @"database.db");

            database = new LiteDatabase(DataBasePath);
        }
    }
}

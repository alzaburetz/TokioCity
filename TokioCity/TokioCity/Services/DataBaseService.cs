using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using LiteDB;
using System.Threading.Tasks;

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
            return database.GetCollection<T>(collection).FindAll();
        }

        public Task<IEnumerable<T>> GetAllStreamAsync<T>(string collection)
        {
            return Task.Run(() => database.GetCollection<T>(collection).FindAll());
        }
        public Task<T> GetItemAsync<T>(string collection, Query query)
        {
            return Task.Run(() => database.GetCollection<T>(collection).IncludeAll(4).FindOne(query));
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

        public IEnumerable<T> GetByQuery<T>(string collection, Query query)
        {
            return database.GetCollection<T>(collection).Find(query);
        }

        public IEnumerator<T> GetByQueryEnumerable<T>(string collection, Query query)
        {
            return database.GetCollection<T>(collection)
                .Find(query)
                .GetEnumerator();
        }

        public Task<IEnumerable<T>> GetByQueryEnumerableAsync<T>(string collection, Query query)
        {
            return Task.Run(() => database.GetCollection<T>(collection).Find(query));
        }

        public void WriteAll<T>(string collection, List<T> items)
        {
            RemoveAll<T>(collection);
            var Items = database.GetCollection<T>(collection).IncludeAll(4);
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

        public T GetOneItem<T>(string collection, Query query = null)
        {
            return database.GetCollection<T>(collection).FindOne(Query.Where("id", x => x.AsInt32 != 0));
        }

        public void WriteItem<T>(string collection, T item)
        {
            var col = database.GetCollection<T>(collection).IncludeAll(5);
            col.EnsureIndex("Id");
            var count =  col.Insert(item);
        }

        public void UpdateItem<T>(string collection, Query query, T item)
        {
            database.GetCollection<T>(collection).Update(item);
        }

        public void  RemoveItem<T>(string collection, Query query)
        {
            var removed = database.GetCollection<T>(collection).Delete(query);
        }

        public T GetItem<T>(string collection, Query query)
        {
            return database.GetCollection<T>(collection).IncludeAll(4).FindOne(query);
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

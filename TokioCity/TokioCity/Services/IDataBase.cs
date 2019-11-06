﻿using System.Collections.Generic;
using LiteDB;

namespace TokioCity.Services
{
    public interface IDataBase
    {
       string DataBasePath { get; set; }
        T GetProduct<T>(string collection, string uid);
       List<T> GetAll<T>(string collection);
       IEnumerable<T> GetAllStream<T>(string collection);
       void WriteAll<T>(string collection, List<T> items);
       void RemoveAll<T>(string collection);
       List<T> GetByQuery<T>(string collection, Query query);
       IEnumerator<T> GetByQueryEnumerable<T>(string collection, Query query);
       int GetRecordCount<T>(string collection);
       T GetOneOfCategory<T>(string collection, int category);
        IEnumerator<T> GetOneItem<T>(string collection, Query query = null);
    }
}

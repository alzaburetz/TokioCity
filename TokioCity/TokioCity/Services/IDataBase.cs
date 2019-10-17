using System;
using System.Collections.Generic;
using System.Text;
using LiteDB;
using TokioCity.Models;

namespace TokioCity.Services
{
    public interface IDataBase
    {
       string DataBasePath { get; set; }
       List<T> GetAll<T>(string collection);
       IEnumerable<T> GetAllStream<T>(string collection);
       void WriteAll<T>(string collection, List<T> items);
       void RemoveAll<T>(string collection);
       List<T> GetByQuery<T>(string collection, Query query);
       IEnumerator<T> GetByQueryEnumerable<T>(string collection, Query query);
       int GetRecordCount<T>(string collection);
       T GetOneOfCategory<T>(string collection, int category);
    }
}

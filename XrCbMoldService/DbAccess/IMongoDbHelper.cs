using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace XrCbMoldService.DbAccess
{
    interface IMongoDbHelper<T> where T : class, new()
    {
        IMongodbHostOptions Host { get; }

        Task<int> AddAsync(T t);

        long Count(Expression<Func<T, bool>> query);

        long Count(FilterDefinition<T> filter);

        long Count(IMongoCollection<T> client, Expression<Func<T, bool>> query);

        Task<long> CountAsync(FilterDefinition<T> filter);

        DeleteResult Delete(Expression<Func<T, bool>> query);

        DeleteResult Delete(string id);

        Task<DeleteResult> DeleteAsync(string id);

        DeleteResult DeleteMany(FilterDefinition<T> filter);

        Task<DeleteResult> DeleteManyAsync(FilterDefinition<T> filter);

        IEnumerable<T> FindAll();

        List<T> FindList(Expression<Func<T, bool>> query);

        List<T> FindList(FilterDefinition<T> filter, string[] field = null, SortDefinition<T> sort = null);

        Task<List<T>> FindListAsync(FilterDefinition<T> filter, string[] field = null, SortDefinition<T> sort = null);

        List<T> FindListByPage(Expression<Func<T, bool>> query, int pageIndex, int pageSize, out long count, string[] field = null, SortDefinition<T> sort = null);

        Task<List<T>> FindListByPageAsync(FilterDefinition<T> filter, int pageIndex, int pageSize, string[] field = null, SortDefinition<T> sort = null);

        T FindOne(Expression<Func<T, bool>> query);

        T FindOne(string id, string[] field = null);

        Task<T> FindOneAsync(string id, string[] field = null);

        Task<IList<T>> GetIntentFormEntity(string entityName);

        int Insert(T t);

        int InsertMany(List<T> t);

        Task<int> InsertManyAsync(List<T> t);

        UpdateResult Update(T t, string id);

        Task<UpdateResult> UpdateAsync(T t, string id);

        UpdateResult UpdateManay(Dictionary<string, string> dic, FilterDefinition<T> filter);

        Task<UpdateResult> UpdateManayAsync(Dictionary<string, string> dic, FilterDefinition<T> filter);
    }
}

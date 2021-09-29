/**
* 命名空间: XrCbMoldService.DbAccess
*
* 功 能： N/A
* 类 名：MongoDbHelper.cs
*
* Ver  变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2021/2/26 9:56:03  彭政亮 初版
*
* Copyright (c) 2020 Lir Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：宁波瑞辉智能科技有限公司 　　　　　 　　　　　　　    　│
*└──────────────────────────────────┘
*/
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace XrCbMoldService.DbAccess
{
    public class MongoDbHelper<T> : IMongoDbHelper<T> where T : class, new()
    {
        public MongoDbHelper(MongodbHostOptions host)
        {
            this.Host = host;
            this.client = MongodbInfoClient(host);
            Console.WriteLine($"创建MongoDB实例===================================");
        }

        public IMongodbHostOptions Host { get; }

        private IMongoCollection<T> client;

        #region +MongodbInfoClient 获取mongodb实例

        /// <summary>
        /// 获取mongodb表实例
        /// 因为MongoDB默认是长连接 所以此处不做处理
        /// </summary>
        /// <param name="host">连接字符串，库，表</param>
        /// <returns></returns>
        private IMongoCollection<T> MongodbInfoClient(MongodbHostOptions host)
        {
            try
            {
                MongoClient client = new MongoClient(host.Connection);
                var dataBase = client.GetDatabase(host.DataBase);
                if (string.IsNullOrEmpty(host.Table))
                {
                    return dataBase.GetCollection<T>(typeof(T).Name);
                }
                else
                {
                    return dataBase.GetCollection<T>(host.Table);
                }
            }
            catch (Exception ex)
            {
                //RH.IoT.Utility.RhLogs.RhLogHelper.Log4Net.WriteLog($"创建MongoDB连接时出错{ex.Message}", ex);
                Console.WriteLine($"创建MongoDB连接时出错{ex.Message}");
                return null;
            }
        }

        #endregion +MongodbInfoClient 获取mongodb实例

        #region +Add 添加一条数据

        /// <summary>
        /// 添加一条数据
        /// </summary>
        /// <param name="t">添加的实体</param>
        /// <param name="host">mongodb连接信息</param>
        /// <returns></returns>
        public int Insert(T t)
        {
            try
            {
                client.InsertOne(t);
                return 1;
            }
            catch (Exception ex)
            {
                //RH.IoT.Utility.RhLogs.RhLogHelper.Log4Net.WriteLog("往MongoDB添加一条数据时发生异常", ex);
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        #endregion +Add 添加一条数据

        #region +AddAsync 异步添加一条数据

        /// <summary>
        /// 异步添加一条数据
        /// </summary>
        /// <param name="t">添加的实体</param>
        /// <param name="host">mongodb连接信息</param>
        /// <returns></returns>
        public async Task<int> AddAsync(T t)
        {
            try
            {
                await client.InsertOneAsync(t);
                return 1;
            }
            catch (Exception ex)
            {
                //RH.IoT.Utility.RhLogs.RhLogHelper.Log4Net.WriteLog("往MongoDB异步添加一条数据时发生异常", ex);
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        #endregion +AddAsync 异步添加一条数据

        #region +InsertMany 批量插入

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="host">mongodb连接信息</param>
        /// <param name="t">实体集合</param>
        /// <returns></returns>
        public int InsertMany(List<T> t)
        {
            try
            {
                client.InsertMany(t);
                return 1;
            }
            catch (Exception ex)
            {
                try
                {
                    var client1 = new MongoClient(Host.Connection);
                    var dataBase = client1.GetDatabase(Host.DataBase);
                    var collection = dataBase.GetCollection<T>(Host.Table);
                    collection.InsertMany(t);
                    return 2;
                }
                catch (Exception)
                {
                    //RH.IoT.Utility.RhLogs.RhLogHelper.Log4Net.WriteLog($"插入MongoDB时错误:\r\n{ex?.InnerException?.Message}");
                    Console.WriteLine($"插入MongoDB是错误:\r\n{ex?.InnerException?.Message}");
                    return 0;
                }
            }
        }

        #endregion +InsertMany 批量插入

        #region +InsertManyAsync 异步批量插入

        /// <summary>
        /// 异步批量插入
        /// </summary>
        /// <param name="host">mongodb连接信息</param>
        /// <param name="t">实体集合</param>
        /// <returns></returns>
        public async Task<int> InsertManyAsync(List<T> t)
        {
            try
            {
                await client.InsertManyAsync(t);
                return 1;
            }
            catch //(Exception ex)
            {
                //RH.IoT.Utility.RhLogs.RhLogHelper.Log4Net.WriteLog("往MongoDB异步批量插入数据时发生异常", ex);
                return 0;
            }
        }

        #endregion +InsertManyAsync 异步批量插入

        //#region +Update 修改一条数据 依据条件
        ///// <summary>
        ///// 更新操作
        ///// </summary>
        ///// <typeparam name="T">类型</typeparam>
        ///// <param name="collectionName">表名</param>
        ///// <param name="query">条件</param>
        ///// <param name="entry">新实体</param>
        //public  int Update( T entity, Expression<Func<T, bool>> query)
        //{
        //

        //    var properties = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public);

        //    var updateDefinitionList = GetUpdateDefinitionList<T>(properties, entity);
        //    var updateDefinitionBuilder = new UpdateDefinitionBuilder<T>().Combine(updateDefinitionList);
        //    var updateResult = client.UpdateOne(query, updateDefinitionBuilder);
        //    return (int)updateResult.ModifiedCount;
        //}
        ///// <summary>
        ///// 获取更新信息
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="propertyInfos"></param>
        ///// <param name="entity"></param>
        ///// <returns></returns>
        // List<UpdateDefinition<T>> GetUpdateDefinitionList<T>(PropertyInfo[] propertyInfos, object entity)
        //{
        //    var updateDefinitionList = new List<UpdateDefinition<T>>();

        //    propertyInfos = propertyInfos.Where(a => a.Name != "_id").ToArray();

        //    foreach (var propertyInfo in propertyInfos)
        //    {
        //        if (propertyInfo.PropertyType.IsArray || typeof(IList).IsAssignableFrom(propertyInfo.PropertyType))
        //        {
        //            var value = propertyInfo.GetValue(entity) as IList;

        //            var filedName = propertyInfo.Name;

        //            updateDefinitionList.Add(Builders<T>.Update.Set(filedName, value));
        //        }
        //        else
        //        {
        //            var value = propertyInfo.GetValue(entity);
        //            if (propertyInfo.PropertyType == typeof(decimal))
        //                value = value.ToString();

        //            var filedName = propertyInfo.Name;

        //            updateDefinitionList.Add(Builders<T>.Update.Set(filedName, value));
        //        }
        //    }

        //    return updateDefinitionList;
        //}

        //#endregion

        #region +Update 修改一条数据

        /// <summary>
        /// 修改一条数据
        /// </summary>
        /// <param name="t">添加的实体</param>
        /// <param name="host">mongodb连接信息</param>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public UpdateResult Update(T t, string id)
        {
            try
            {
                //修改条件
                FilterDefinition<T> filter = Builders<T>.Filter.Eq("_id", new ObjectId(id));
                //要修改的字段
                var list = new List<UpdateDefinition<T>>();
                foreach (var item in t.GetType().GetProperties())
                {
                    if (item.Name.ToLower() == "id")
                    {
                        continue;
                    }

                    list.Add(Builders<T>.Update.Set(item.Name, item.GetValue(t)));
                }
                var updatefilter = Builders<T>.Update.Combine(list);
                return client.UpdateOne(filter, updatefilter);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //RH.IoT.Utility.RhLogs.RhLogHelper.Log4Net.WriteLog("往MongoDB修改一条数据时发生异常", ex);
                return null;
                //throw ex;
            }
        }

        #endregion +Update 修改一条数据

        #region +UpdateAsync 异步修改一条数据

        /// <summary>
        /// 异步修改一条数据
        /// </summary>
        /// <param name="t">添加的实体</param>
        /// <param name="host">mongodb连接信息</param>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public async Task<UpdateResult> UpdateAsync(T t, string id)
        {
            try
            {
                //修改条件
                FilterDefinition<T> filter = Builders<T>.Filter.Eq("_id", new ObjectId(id));
                //要修改的字段
                var list = new List<UpdateDefinition<T>>();
                foreach (var item in t.GetType().GetProperties())
                {
                    if (item.Name.ToLower() == "id")
                    {
                        continue;
                    }

                    list.Add(Builders<T>.Update.Set(item.Name, item.GetValue(t)));
                }
                var updatefilter = Builders<T>.Update.Combine(list);
                return await client.UpdateOneAsync(filter, updatefilter);
            }
            catch
            {
                //RH.IoT.Utility.RhLogs.RhLogHelper.Log4Net.WriteLog("往MongoDB异步修改一条数据时发生异常", ex);
                return null;
                //throw ex;
            }
        }

        #endregion +UpdateAsync 异步修改一条数据

        #region +UpdateManay 批量修改数据

        /// <summary>
        /// 批量修改数据
        /// </summary>
        /// <param name="dic">要修改的字段</param>
        /// <param name="host">mongodb连接信息</param>
        /// <param name="filter">修改条件</param>
        /// <returns></returns>
        public UpdateResult UpdateManay(Dictionary<string, string> dic, FilterDefinition<T> filter)
        {
            try
            {
                T t = new T();
                //要修改的字段
                var list = new List<UpdateDefinition<T>>();
                foreach (var item in t.GetType().GetProperties())
                {
                    if (!dic.ContainsKey(item.Name))
                    {
                        continue;
                    }

                    var value = dic[item.Name];
                    list.Add(Builders<T>.Update.Set(item.Name, value));
                }
                var updatefilter = Builders<T>.Update.Combine(list);
                return client.UpdateMany(filter, updatefilter);
            }
            catch
            {
                //RH.IoT.Utility.RhLogs.RhLogHelper.Log4Net.WriteLog("往MongoDB批量修改数据时发生异常", ex);
                return null;
                //throw ex;
            }
        }

        #endregion +UpdateManay 批量修改数据

        #region +UpdateManayAsync 异步批量修改数据

        /// <summary>
        /// 异步批量修改数据
        /// </summary>
        /// <param name="dic">要修改的字段</param>
        /// <param name="host">mongodb连接信息</param>
        /// <param name="filter">修改条件</param>
        /// <returns></returns>
        public async Task<UpdateResult> UpdateManayAsync(Dictionary<string, string> dic, FilterDefinition<T> filter)
        {
            try
            {
                T t = new T();
                //要修改的字段
                var list = new List<UpdateDefinition<T>>();
                foreach (var item in t.GetType().GetProperties())
                {
                    if (!dic.ContainsKey(item.Name))
                    {
                        continue;
                    }

                    var value = dic[item.Name];
                    list.Add(Builders<T>.Update.Set(item.Name, value));
                }
                var updatefilter = Builders<T>.Update.Combine(list);
                return await client.UpdateManyAsync(filter, updatefilter);
            }
            catch
            {
                //RH.IoT.Utility.RhLogs.RhLogHelper.Log4Net.WriteLog("往MongoDB异步批量修改数据时发生异常", ex);
                return null;
                //throw ex;
            }
        }

        #endregion +UpdateManayAsync 异步批量修改数据

        #region +Delete 删除数据根据条件

        public DeleteResult Delete(Expression<Func<T, bool>> query)
        {
            try
            {
                return client.DeleteMany(query);
            }
            catch
            {
                //RH.IoT.Utility.RhLogs.RhLogHelper.Log4Net.WriteLog("从MongoDB删除数据时发生异常", ex);
                return null;
            }
        }

        #endregion +Delete 删除数据根据条件

        #region Delete 删除一条数据

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="host">mongodb连接信息</param>
        /// <param name="id">objectId</param>
        /// <returns></returns>
        public DeleteResult Delete(string id)
        {
            try
            {
                FilterDefinition<T> filter = Builders<T>.Filter.Eq("_id", new ObjectId(id));
                return client.DeleteOne(filter);
            }
            catch
            {
                //RH.IoT.Utility.RhLogs.RhLogHelper.Log4Net.WriteLog("从MongoDB删除数据时发生异常", ex);
                return null;
            }
        }

        #endregion Delete 删除一条数据

        #region DeleteAsync 异步删除一条数据

        /// <summary>
        /// 异步删除一条数据
        /// </summary>
        /// <param name="host">mongodb连接信息</param>
        /// <param name="id">objectId</param>
        /// <returns></returns>
        public async Task<DeleteResult> DeleteAsync(string id)
        {
            try
            {
                //修改条件
                FilterDefinition<T> filter = Builders<T>.Filter.Eq("_id", new ObjectId(id));
                return await client.DeleteOneAsync(filter);
            }
            catch
            {
                //RH.IoT.Utility.RhLogs.RhLogHelper.Log4Net.WriteLog("从MongoDB异步删除一条数据时发生异常", ex);
                return null;
            }
        }

        #endregion DeleteAsync 异步删除一条数据

        #region DeleteMany 删除多条数据

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="host">mongodb连接信息</param>
        /// <param name="filter">删除的条件</param>
        /// <returns></returns>
        public DeleteResult DeleteMany(FilterDefinition<T> filter)
        {
            try
            {
                return client.DeleteMany(filter);
            }
            catch
            {
                //RH.IoT.Utility.RhLogs.RhLogHelper.Log4Net.WriteLog("从MongoDB删除多条数据时发生异常", ex);
                return null;
            }
        }

        #endregion DeleteMany 删除多条数据

        #region DeleteManyAsync 异步删除多条数据

        /// <summary>
        /// 异步删除多条数据
        /// </summary>
        /// <param name="host">mongodb连接信息</param>
        /// <param name="filter">删除的条件</param>
        /// <returns></returns>
        public async Task<DeleteResult> DeleteManyAsync(FilterDefinition<T> filter)
        {
            try
            {
                return await client.DeleteManyAsync(filter);
            }
            catch
            {
                //RH.IoT.Utility.RhLogs.RhLogHelper.Log4Net.WriteLog("从MongoDB异步删除多条数据时发生异常", ex);
                return null;
            }
        }

        #endregion DeleteManyAsync 异步删除多条数据

        #region Count 根据条件获取总数

        /// <summary>
        /// 根据条件获取总数
        /// </summary>
        /// <param name="host">mongodb连接信息</param>
        /// <param name="filter">条件</param>
        /// <returns></returns>
        public long Count(FilterDefinition<T> filter)
        {
            try
            {
#pragma warning disable CS0618 // 类型或成员已过时
                return client.Count(filter);
#pragma warning restore CS0618 // 类型或成员已过时
            }
            catch
            {
                //RH.IoT.Utility.RhLogs.RhLogHelper.Log4Net.WriteLog("从MongoDB获取总数发生异常", ex);
                return 0;
            }
        }

        /// <summary>
        /// 获取记录数
        /// </summary>
        /// <param name="host"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public long Count(Expression<Func<T, bool>> query)
        {
            try
            {
                return client.CountDocuments(query);
            }
            catch
            {
                //RH.IoT.Utility.RhLogs.RhLogHelper.Log4Net.WriteLog("从MongoDB获取记录数发生异常", ex);
                return 0;
            }
        }

        /// <summary>
        /// 获取记录数
        /// </summary>
        /// <param name="host"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public long Count(IMongoCollection<T> client, Expression<Func<T, bool>> query)
        {
            try
            {
                return client.CountDocuments(query);
            }
            catch
            {
                //RH.IoT.Utility.RhLogs.RhLogHelper.Log4Net.WriteLog("从MongoDB获取总数发生异常", ex);
                return 0;
            }
        }

        #endregion Count 根据条件获取总数

        #region CountAsync 异步根据条件获取总数

        /// <summary>
        /// 异步根据条件获取总数
        /// </summary>
        /// <param name="host">mongodb连接信息</param>
        /// <param name="filter">条件</param>
        /// <returns></returns>
        public async Task<long> CountAsync(FilterDefinition<T> filter)
        {
            try
            {
#pragma warning disable CS0618 // 类型或成员已过时
                return await client.CountAsync(filter);
#pragma warning restore CS0618 // 类型或成员已过时
            }
            catch
            {
                //RH.IoT.Utility.RhLogs.RhLogHelper.Log4Net.WriteLog("从MongoDB异步获取总数发生异常", ex);
                return 0;
            }
        }

        #endregion CountAsync 异步根据条件获取总数

        #region FindOne 根据id查询一条数据

        /// <summary>
        /// 获取一条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="host">mongodb连接信息</param>
        /// <param name="query"></param>
        /// <returns></returns>
        public T FindOne(Expression<Func<T, bool>> query)
        {
            try
            {
                T result = default(T);

                result = client.Find(query).FirstOrDefault();
                return result;
            }
            catch //(Exception ex)
            {
                //RH.IoT.Utility.RhLogs.RhLogHelper.Log4Net.WriteLog("从MongoDB查询数据发生异常", ex);
                return null;
            }
        }

        /// <summary>
        /// 根据id查询一条数据
        /// </summary>
        /// <param name="host">mongodb连接信息</param>
        /// <param name="id">objectid</param>
        /// <param name="field">要查询的字段，不写时查询全部</param>
        /// <returns></returns>
        public T FindOne(string id, string[] field = null)
        {
            try
            {
                FilterDefinition<T> filter = Builders<T>.Filter.Eq("_id", new ObjectId(id));
                //不指定查询字段
                if (field == null || field.Length == 0)
                {
                    return client.Find(filter).FirstOrDefault<T>();
                }

                //制定查询字段
                var fieldList = new List<ProjectionDefinition<T>>();
                for (int i = 0; i < field.Length; i++)
                {
                    fieldList.Add(Builders<T>.Projection.Include(field[i].ToString()));
                }
                var projection = Builders<T>.Projection.Combine(fieldList);
                fieldList?.Clear();
                return client.Find(filter).Project<T>(projection).FirstOrDefault<T>();
            }
            catch //(Exception ex)
            {
                //RH.IoT.Utility.RhLogs.RhLogHelper.Log4Net.WriteLog("从MongoDB查询数据发生异常", ex);
                return null;
            }
        }

        #endregion FindOne 根据id查询一条数据

        #region FindOneAsync 异步根据id查询一条数据

        /// <summary>
        /// 异步根据id查询一条数据
        /// </summary>
        /// <param name="host">mongodb连接信息</param>
        /// <param name="id">objectid</param>
        /// <param name="field">字段名称</param>
        /// <returns></returns>
        public async Task<T> FindOneAsync(string id, string[] field = null)
        {
            try
            {
                FilterDefinition<T> filter = Builders<T>.Filter.Eq("_id", new ObjectId(id));
                //不指定查询字段
                if (field == null || field.Length == 0)
                {
                    return await client.Find(filter).FirstOrDefaultAsync();
                }

                //制定查询字段
                var fieldList = new List<ProjectionDefinition<T>>();
                for (int i = 0; i < field.Length; i++)
                {
                    fieldList.Add(Builders<T>.Projection.Include(field[i].ToString()));
                }
                var projection = Builders<T>.Projection.Combine(fieldList);
                fieldList?.Clear();
                return await client.Find(filter).Project<T>(projection).FirstOrDefaultAsync();
            }
            catch //(Exception ex)
            {
                //RH.IoT.Utility.RhLogs.RhLogHelper.Log4Net.WriteLog("从MongoDB查询数据发生异常", ex);
                return null;
            }
        }

        #endregion FindOneAsync 异步根据id查询一条数据

        #region FindList 查询集合

        /// <summary>
        /// 获取一个集合下所有数据
        /// </summary>
        /// <param name="collectionName"></param>
        /// <returns></returns>
        public IEnumerable<T> FindAll()
        {
            try
            {
                var result = client.Find<T>(new BsonDocument()).ToEnumerable();
                return result;
            }
            catch //(Exception ex)
            {
                //RH.IoT.Utility.RhLogs.RhLogHelper.Log4Net.WriteLog("从MongoDB查询数据发生异常", ex);
                return null;
            }
        }

        public async Task<IList<T>> GetIntentFormEntity(string entityName)
        {
            var query = "{$or:[{'utterances.entities.entityId':'" + entityName + "'}, {'parameters.entityId':'" + entityName + "'}]}";
            var filter = BsonSerializer.Deserialize<BsonDocument>(query);
            var result = await client.FindAsync(filter);
            return result.ToList();
        }

        /// <summary>
        /// 查询集合
        /// </summary>
        /// <param name="host">mongodb连接信息</param>
        /// <param name="query">查询条件</param>
        /// <returns></returns>
        public List<T> FindList(Expression<Func<T, bool>> query)
        {
            try
            {
                var result = client.Find<T>(query).ToList();
                return result;
            }
            catch //(Exception ex)
            {
                //RH.IoT.Utility.RhLogs.RhLogHelper.Log4Net.WriteLog("从MongoDB查询数据发生异常", ex);
                return null;
            }
        }

        /// <summary>
        /// 查询集合
        /// </summary>
        /// <param name="host">mongodb连接信息</param>
        /// <param name="filter">查询条件</param>
        /// <param name="field">要查询的字段,不写时查询全部</param>
        /// <param name="sort">要排序的字段</param>
        /// <returns></returns>
        public List<T> FindList(FilterDefinition<T> filter, string[] field = null, SortDefinition<T> sort = null)
        {
            try
            {
                //不指定查询字段
                if (field == null || field.Length == 0)
                {
                    if (sort == null)
                    {
                        return client.Find(filter).ToList();
                    }
                    //进行排序
                    return client.Find(filter).Sort(sort).ToList();
                }

                //制定查询字段
                var fieldList = new List<ProjectionDefinition<T>>();
                for (int i = 0; i < field.Length; i++)
                {
                    fieldList.Add(Builders<T>.Projection.Include(field[i].ToString()));
                }
                var projection = Builders<T>.Projection.Combine(fieldList);
                fieldList?.Clear();
                if (sort == null)
                {
                    return client.Find(filter).Project<T>(projection).ToList();
                }
                //排序查询
                return client.Find(filter).Sort(sort).Project<T>(projection).ToList();
            }
            catch //(Exception ex)
            {
                //RH.IoT.Utility.RhLogs.RhLogHelper.Log4Net.WriteLog("从MongoDB查询数据发生异常", ex);
                return null;
            }
        }

        #endregion FindList 查询集合

        #region FindListAsync 异步查询集合

        /// <summary>
        /// 异步查询集合
        /// </summary>
        /// <param name="host">mongodb连接信息</param>
        /// <param name="filter">查询条件</param>
        /// <param name="field">要查询的字段,不写时查询全部</param>
        /// <param name="sort">要排序的字段</param>
        /// <returns></returns>
        public async Task<List<T>> FindListAsync(FilterDefinition<T> filter, string[] field = null, SortDefinition<T> sort = null)
        {
            try
            {
                //不指定查询字段
                if (field == null || field.Length == 0)
                {
                    if (sort == null)
                    {
                        return await client.Find(filter).ToListAsync();
                    }

                    return await client.Find(filter).Sort(sort).ToListAsync();
                }

                //制定查询字段
                var fieldList = new List<ProjectionDefinition<T>>();
                for (int i = 0; i < field.Length; i++)
                {
                    fieldList.Add(Builders<T>.Projection.Include(field[i].ToString()));
                }
                var projection = Builders<T>.Projection.Combine(fieldList);
                fieldList?.Clear();
                if (sort == null)
                {
                    return await client.Find(filter).Project<T>(projection).ToListAsync();
                }
                //排序查询
                return await client.Find(filter).Sort(sort).Project<T>(projection).ToListAsync();
            }
            catch //(Exception ex)
            {
                //RH.IoT.Utility.RhLogs.RhLogHelper.Log4Net.WriteLog("从MongoDB异步查询数据发生异常", ex);
                return null;
            }
        }

        #endregion FindListAsync 异步查询集合

        #region FindListByPage 分页查询集合

        /// <summary>
        /// 分页查询集合
        /// </summary>
        /// <param name="host">mongodb连接信息</param>
        /// <param name="filter">查询条件</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="count">总条数</param>
        /// <param name="field">要查询的字段,不写时查询全部</param>
        /// <param name="sort">要排序的字段</param>
        /// <returns></returns>
        public List<T> FindListByPage(Expression<Func<T, bool>> query, int pageIndex, int pageSize, out long count, string[] field = null, SortDefinition<T> sort = null)
        {
            try
            {
                count = client.CountDocuments<T>(query);

                //不指定查询字段
                if (field == null || field.Length == 0)
                {
                    if (sort == null)
                    {
                        return client.Find(query).Skip((pageIndex - 1) * pageSize).Limit(pageSize).ToList();
                    }
                    //进行排序
                    return client.Find(query).Sort(sort).Skip((pageIndex - 1) * pageSize).Limit(pageSize).ToList();
                }

                //制定查询字段
                var fieldList = new List<ProjectionDefinition<T>>();
                for (int i = 0; i < field.Length; i++)
                {
                    fieldList.Add(Builders<T>.Projection.Include(field[i].ToString()));
                }
                var projection = Builders<T>.Projection.Combine(fieldList);
                fieldList?.Clear();

                //不排序
                if (sort == null)
                {
                    return client.Find(query).Project<T>(projection).Skip((pageIndex - 1) * pageSize).Limit(pageSize).ToList();
                }

                //排序查询
                return client.Find(query).Sort(sort).Project<T>(projection).Skip((pageIndex - 1) * pageSize).Limit(pageSize).ToList();
            }
            catch //(Exception ex)
            {
                //RH.IoT.Utility.RhLogs.RhLogHelper.Log4Net.WriteLog("从MongoDB查询数据发生异常", ex);
                count = 0;
                return null;
                //throw ex;
            }
        }

        #endregion FindListByPage 分页查询集合

        #region FindListByPageAsync 异步分页查询集合

        /// <summary>
        /// 异步分页查询集合
        /// </summary>
        /// <param name="host">mongodb连接信息</param>
        /// <param name="filter">查询条件</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="field">要查询的字段,不写时查询全部</param>
        /// <param name="sort">要排序的字段</param>
        /// <returns></returns>
        public async Task<List<T>> FindListByPageAsync(FilterDefinition<T> filter, int pageIndex, int pageSize, string[] field = null, SortDefinition<T> sort = null)
        {
            try
            {
                //不指定查询字段
                if (field == null || field.Length == 0)
                {
                    if (sort == null)
                    {
                        return await client.Find(filter).Skip((pageIndex - 1) * pageSize).Limit(pageSize).ToListAsync();
                    }
                    //进行排序
                    return await client.Find(filter).Sort(sort).Skip((pageIndex - 1) * pageSize).Limit(pageSize).ToListAsync();
                }

                //制定查询字段
                var fieldList = new List<ProjectionDefinition<T>>();
                for (int i = 0; i < field.Length; i++)
                {
                    fieldList.Add(Builders<T>.Projection.Include(field[i].ToString()));
                }
                var projection = Builders<T>.Projection.Combine(fieldList);
                fieldList?.Clear();

                //不排序
                if (sort == null)
                {
                    return await client.Find(filter).Project<T>(projection).Skip((pageIndex - 1) * pageSize).Limit(pageSize).ToListAsync();
                }

                //排序查询
                return await client.Find(filter).Sort(sort).Project<T>(projection).Skip((pageIndex - 1) * pageSize).Limit(pageSize).ToListAsync();
            }
            catch //(Exception ex)
            {
                //RH.IoT.Utility.RhLogs.RhLogHelper.Log4Net.WriteLog("从MongoDB异步分页查询数据发生异常", ex);
                return null;
            }
        }

        //public void Add(string v1, DateTime dateTime, string v2, string v3)
        //{
        //    MongoHelper<RhLogDto>.RhIotLogs.Add(new RhLogDto { LogTime = dateTime, LogTitle = v1, LogType = v2, Msg = v3 });
        //}

        #endregion FindListByPageAsync 异步分页查询集合

        //#region 根据条件求和相应列
        ///// <summary>
        ///// 获取记录数
        ///// </summary>
        ///// <param name="host"></param>
        ///// <param name="query"></param>
        ///// <returns></returns>
        //public long Sum(Expression<Func<T, bool>> query)
        //{
        //    try
        //    {
        //        return client.Aggregate()
        //    }
        //    catch (Exception ex)
        //    {
        //        //RH.IoT.Utility.RhLogs.RhLogHelper.Log4Net.WriteLog("从MongoDB获取记录数发生异常", ex);
        //        return 0;
        //    }
        //}
        //#endregion
    }
}

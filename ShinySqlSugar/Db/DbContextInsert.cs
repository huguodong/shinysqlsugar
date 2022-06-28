using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShinySqlSugar
{
    /// <summary>
    /// 数据库上下文添加方法
    /// </summary>
    public partial class DbContext
    {

        #region 单条

        /// <summary>
        /// 获取插入实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="addOneParams"></param>
        /// <returns></returns>
        private static IInsertable<T> GetInsertable<T>(AddOneParams<T> addOneParams) where T : class, new()
        {

            var query = Db.Insertable(addOneParams.Entity).IgnoreColumns(addOneParams.IgnoreColumns);
            if (addOneParams.InsertColumns != null)
            {
                query.InsertColumns(addOneParams.InsertColumns);
            }
            if (addOneParams.IsSpliteTable)
            {
                query.SplitTable();
            }
            return query;
        }

        /// <summary>
        /// 获取分表插入实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="addOneParams"></param>
        /// <returns></returns>
        private static SplitInsertable<T> GetSplitInsertable<T>(AddOneParams<T> addOneParams) where T : class, new()
        {

            var query = GetInsertable(addOneParams);
            return query.SplitTable();
        }


        /// <summary>
        /// 添加一条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="addOneParams"></param>
        /// <returns>添加成功数量</returns>
        public static async Task<int> AddOneAsync<T>(AddOneParams<T> addOneParams) where T : class, new()
        {

            if (addOneParams.IsSpliteTable)//分表
            {
                return await GetSplitInsertable(addOneParams).ExecuteCommandAsync();
            }
            else
            {
                return await GetInsertable(addOneParams).ExecuteCommandAsync();
            }
        }

        /// <summary>
        /// 添加一条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="addOneParams"></param>
        /// <returns>添加成功数量</returns>
        public static int AddOne<T>(AddOneParams<T> addOneParams) where T : class, new()
        {

            if (addOneParams.IsSpliteTable)//分表
            {
                return GetSplitInsertable(addOneParams).ExecuteCommand();
            }
            else
            {
                return GetInsertable(addOneParams).ExecuteCommand();
            }
        }

        #endregion

        #region 批量



        /// <summary>
        /// 获取批量插入实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="addListParams"></param>
        /// <returns></returns>
        private static IInsertable<T> GetInsertable<T>(AddListParams<T> addListParams) where T : class, new()
        {

            var query = Db.Insertable(addListParams.Entities);
            if (addListParams.InsertColumns != null)
            {
                query = query.InsertColumns(addListParams.InsertColumns);
            }
            return query;
        }

        /// <summary>
        /// 获取分表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="addListParams"></param>
        /// <returns></returns>
        private static SplitInsertable<T> GetSplitInsertable<T>(AddListParams<T> addListParams) where T : class, new()
        {

            var query = GetInsertable(addListParams);
            return query.SplitTable();
        }


        /// <summary>
        /// 批量添加数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="addListParams"></param>
        /// <returns></returns>
        public static async Task<int> AddListAsync<T>(AddListParams<T> addListParams) where T : class, new()
        {
            if (addListParams.IsSpliteTable)//分表
            {
                return await GetSplitInsertable(addListParams).ExecuteCommandAsync();
            }
            else
            {
                return await GetInsertable(addListParams).ExecuteCommandAsync();
            }
        }

        /// <summary>
        /// 批量添加数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="addListParams"></param>
        /// <returns></returns>
        public static int AddList<T>(AddListParams<T> addListParams) where T : class, new()
        {
            if (addListParams.IsSpliteTable)//分表
            {
                return GetSplitInsertable(addListParams).ExecuteCommand();
            }
            else
            {
                return GetInsertable(addListParams).ExecuteCommand();
            }
        }
        #endregion

    }
}

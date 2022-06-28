using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ShinySqlSugar
{
    /// <summary>
    /// 数据库上下文查询方法
    /// </summary>
    public partial class DbContext
    {
        #region 列表查询

        /// <summary>
        /// 获取批量查询实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="queryListParams"></param>
        /// <returns></returns>
        private static ISugarQueryable<TResult> GetSugarQueryable<T, TResult>(QueryListParams<T> queryListParams)
        {
            queryListParams.Select = null;
            var query = GetSugarQueryable(queryListParams).Select<TResult>();//去重
            return query;
        }

        /// <summary>
        /// 获取批量查询实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryListParams"></param>
        /// <returns></returns>
        private static ISugarQueryable<T> GetSugarQueryable<T>(QueryListParams<T> queryListParams)
        {
            var query = Db.QueryableWithAttr<T>().WhereIF(queryListParams.Where != null, queryListParams.Where);//条件

            if (!string.IsNullOrEmpty(queryListParams.OrderBy)) query.OrderBy(queryListParams.OrderBy);//排序
            if (queryListParams.OrderbyExp != null) query.OrderBy(queryListParams.OrderbyExp, queryListParams.OrderByType);//排序
            if (queryListParams.Take > 0) query.Take(queryListParams.Take);//指定条数
            if (queryListParams.Skip > 0) query.Skip(queryListParams.Skip);//跳过指定数量
            if (queryListParams.Distinct) query.Distinct();//去重
            if (queryListParams.Select != null) query.Select(queryListParams.Select);//输出
            return query;
        }

        /// <summary>
        /// 条件查询返回列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryListParams"></param>
        /// <returns>实体列表</returns>
        public static List<T> QueryList<T>(QueryListParams<T> queryListParams)
        {
            var query = GetSugarQueryable(queryListParams);
            return query.ToList();
        }

        /// <summary>
        /// 条件查询返回指定实体列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult">结果实体</typeparam>
        /// <param name="queryListParams"></param>
        /// <returns></returns>
        public static List<TResult> QueryList<T, TResult>(QueryListParams<T> queryListParams)
        {

            var query = GetSugarQueryable<T, TResult>(queryListParams);
            return query.ToList();
        }


        /// <summary>
        /// 条件查询返回指定实体列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult">结果实体</typeparam>
        /// <param name="queryListParams"></param>
        /// <returns></returns>
        public static async Task<List<TResult>> QueryListAsync<T, TResult>(QueryListParams<T> queryListParams)
        {
            var query = GetSugarQueryable<T, TResult>(queryListParams);
            return await query.ToListAsync();
        }

        /// <summary>
        /// 条件查询返回列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryListParams"></param>
        /// <returns>实体列表</returns>
        public static async Task<List<T>> QueryListAsync<T>(QueryListParams<T> queryListParams)
        {

            var query = GetSugarQueryable(queryListParams);
            return await query.ToListAsync();
        }
        #endregion

        #region 单查

        /// <summary>
        /// 获取单查实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryOneParams"></param>
        /// <returns></returns>
        private static ISugarQueryable<T> GetSugarQueryable<T>(QueryOneParams<T> queryOneParams)
        {
            var query = Db.QueryableWithAttr<T>().WhereIF(queryOneParams.Where != null, queryOneParams.Where);//条件
            if (!string.IsNullOrEmpty(queryOneParams.OrderBy)) query.OrderBy(queryOneParams.OrderBy);//排序
            if (queryOneParams.OrderbyExp != null) query.OrderBy(queryOneParams.OrderbyExp, queryOneParams.OrderByType);//排序
            if (queryOneParams.Select != null) query.Select(queryOneParams.Select);//输出
            return query;
        }

        /// <summary>
        /// 获取单查实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult">返回实体</typeparam>
        /// <param name="queryOneParams"></param>
        /// <returns></returns>
        private static ISugarQueryable<TResult> GetSugarQueryable<T, TResult>(QueryOneParams<T> queryOneParams)
        {
            queryOneParams.Select = null;
            var query = GetSugarQueryable(queryOneParams).Select<TResult>();
            return query;
        }


        /// <summary>
        /// 查询第一条
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryOneParams"></param>
        /// <returns></returns>
        public static async Task<T> FirstAsync<T>(QueryOneParams<T> queryOneParams)
        {
            var query = GetSugarQueryable(queryOneParams);
            return await query.FirstAsync();
        }

        /// <summary>
        /// 查询第一条返回指定实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult">返回实体</typeparam>
        /// <param name="queryOneParams"></param>
        /// <returns></returns>
        public static async Task<TResult> FirstAsync<T, TResult>(QueryOneParams<T> queryOneParams)
        {
            var query = GetSugarQueryable<T, TResult>(queryOneParams);
            return await query.FirstAsync();
        }


        /// <summary>
        /// 查询第一条
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryOneParams"></param>
        /// <returns></returns>
        public static T First<T>(QueryOneParams<T> queryOneParams)
        {
            var query = GetSugarQueryable(queryOneParams);
            return query.First();
        }

        /// <summary>
        /// 查询第一条返回指定实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult">返回实体</typeparam>
        /// <param name="queryOneParams"></param>
        /// <returns></returns>
        public static TResult First<T, TResult>(QueryOneParams<T> queryOneParams)
        {
            var query = GetSugarQueryable<T, TResult>(queryOneParams);
            return query.First();
        }


        /// <summary>
        /// 根据主键查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pkValue">主键</param>
        /// <returns></returns>
        public static T InSingle<T>(object pkValue)
        {
            return Db.QueryableWithAttr<T>().InSingle(pkValue);
        }

        /// <summary>
        /// 根据主键查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pkValue">主键</param>
        /// <returns></returns>
        public static async Task<T> InSingleAsync<T>(object pkValue)
        {
            return await Db.QueryableWithAttr<T>().InSingleAsync(pkValue);
        }
        #endregion

        #region 统计

        /// <summary>
        /// 数据行数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        public static int Count<T>(Expression<Func<T, bool>> where)
        {
            return Db.QueryableWithAttr<T>().WhereIF(where != null, where).Count();
        }

        /// <summary>
        /// 数据行数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        public static async Task<int> CountAsync<T>(Expression<Func<T, bool>> where)
        {
            return await Db.QueryableWithAttr<T>().WhereIF(where != null, where).CountAsync();
        }

        /// <summary>
        /// 是否存在记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        public static bool Any<T>(Expression<Func<T, bool>> where)
        {
            return Db.QueryableWithAttr<T>().WhereIF(where != null, where).Any();
        }

        /// <summary>
        /// 是否存在记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        public static async Task<bool> AnyAsync<T>(Expression<Func<T, bool>> where)
        {
            return await Db.QueryableWithAttr<T>().WhereIF(where != null, where).AnyAsync();
        }

        #region 聚合

        /// <summary>
        /// 获取最大值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult">返回类型</typeparam>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        public static TResult Max<T, TResult>(Expression<Func<T, TResult>> where)
        {
            return Db.QueryableWithAttr<T>().Max(where);
        }

        /// <summary>
        /// 获取最大值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult">返回类型</typeparam>
        /// <param name="where">查询条件</param>
        /// <returns></returns>

        public static async Task<TResult> MaxAsync<T, TResult>(Expression<Func<T, TResult>> where)
        {
            return await Db.QueryableWithAttr<T>().MaxAsync(where);
        }

        /// <summary>
        /// 获取最大值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult">返回类型</typeparam>
        /// <param name="maxField">查询条件</param>
        /// <returns></returns>
        public static TResult Max<T, TResult>(string maxField)
        {
            return Db.QueryableWithAttr<T>().Max<TResult>(maxField);
        }

        /// <summary>
        /// 获取最大值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult">返回类型</typeparam>
        /// <param name="maxField">查询条件</param>
        /// <returns></returns>
        public static async Task<TResult> MaxAsync<T, TResult>(string maxField)
        {
            return await Db.QueryableWithAttr<T>().MaxAsync<TResult>(maxField);
        }

        /// <summary>
        /// 获取最小值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult">返回类型</typeparam>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        public static TResult Min<T, TResult>(Expression<Func<T, TResult>> where)
        {
            return Db.QueryableWithAttr<T>().Min(where);
        }

        /// <summary>
        /// 获取最小值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult">返回类型</typeparam>
        /// <param name="where">查询条件</param>
        /// <returns></returns>

        public static async Task<TResult> MinAsync<T, TResult>(Expression<Func<T, TResult>> where)
        {
            return await Db.QueryableWithAttr<T>().MinAsync(where);
        }

        /// <summary>
        /// 获取最小值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult">返回类型</typeparam>
        /// <param name="maxField">查询条件</param>
        /// <returns></returns>
        public static TResult Min<T, TResult>(string maxField)
        {
            return Db.QueryableWithAttr<T>().Max<TResult>(maxField);
        }

        /// <summary>
        /// 获取最小值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult">返回类型</typeparam>
        /// <param name="maxField">查询条件</param>
        /// <returns></returns>
        public static async Task<TResult> MinAsync<T, TResult>(string maxField)
        {

            return await Db.QueryableWithAttr<T>().MinAsync<TResult>(maxField);
        }


        /// <summary>
        /// 求和
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult">返回类型</typeparam>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        public static TResult Sum<T, TResult>(Expression<Func<T, TResult>> where)
        {
            return Db.QueryableWithAttr<T>().Sum(where);
        }

        /// <summary>
        /// 求和
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult">返回类型</typeparam>
        /// <param name="where">查询条件</param>
        /// <returns></returns>

        public static async Task<TResult> SumAsync<T, TResult>(Expression<Func<T, TResult>> where)
        {
            return await Db.QueryableWithAttr<T>().SumAsync(where);
        }

        /// <summary>
        /// 求和
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult">返回类型</typeparam>
        /// <param name="maxField">查询条件</param>
        /// <returns></returns>
        public static TResult Sum<T, TResult>(string maxField)
        {
            return Db.QueryableWithAttr<T>().Sum<TResult>(maxField);
        }

        /// <summary>
        /// 求和
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult">返回类型</typeparam>
        /// <param name="maxField">查询条件</param>
        /// <returns></returns>
        public static async Task<TResult> SumAsync<T, TResult>(string maxField)
        {

            return await Db.QueryableWithAttr<T>().SumAsync<TResult>(maxField);
        }
        #endregion

        #endregion

        #region
        #endregion
    }
}

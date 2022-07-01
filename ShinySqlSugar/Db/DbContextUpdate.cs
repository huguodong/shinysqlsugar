using SqlSugar;
using System.Linq;
using System.Threading.Tasks;

namespace ShinySqlSugar
{
    /// <summary>
    /// 数据库上下文更新方法
    /// </summary>
    public partial class DbContext
    {

        #region 更新单条

        #region 获取实例
        /// <summary>
        /// 获取修改实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="updateOneParams"></param>
        /// <returns></returns>
        public static IUpdateable<T> GetUpdateable<T>(UpdateOneParams<T> updateOneParams) where T : class, new()
        {
            IUpdateable<T> query;
            if (updateOneParams.Entity != null)//有实体
            {
                query = Db.UpdateableWithAttr(updateOneParams.Entity).IgnoreColumns(updateOneParams.IgnoreNullColumns);
                if (updateOneParams.IgnoreColumns != null)
                {
                    query.IgnoreColumns(updateOneParams.IgnoreColumns);
                }
                if (updateOneParams.UpdateColumns != null)
                {
                    query.UpdateColumns(updateOneParams.UpdateColumns);
                }
                if (updateOneParams.WhereColumns != null)
                {
                    query.WhereColumns(updateOneParams.WhereColumns);
                }
            }
            else//无实体
            {
                query = Db.UpdateableWithAttr<T>().SetColumns(updateOneParams.SetsColumns);
            }
            if (updateOneParams.Where != null)
            {
                query.Where(updateOneParams.Where);
            }
            return query;
        }

        /// <summary>
        /// 获取插入分表实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="updateOneParams"></param>
        /// <returns></returns>
        public static SplitTableUpdateByObjectProvider<T> GetSplitUpdateable<T>(UpdateOneParams<T> updateOneParams) where T : class, new()
        {
            var query = GetUpdateable(updateOneParams);

            return query.SplitTable();
        }
        #endregion


        /// <summary>
        /// 根据实体更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="updateOneParams"></param>
        /// <returns></returns>
        public static int Update<T>(UpdateOneParams<T> updateOneParams) where T : class, new()
        {
            if (updateOneParams.IsSpliteTable)
            {
                return GetSplitUpdateable(updateOneParams).ExecuteCommand();
            }
            else
            {
                return GetUpdateable(updateOneParams).ExecuteCommand();
            }

        }

        /// <summary>
        /// 根据实体更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="updateOneParams"></param>
        /// <returns></returns>
        public static async Task<int> UpdateAsync<T>(UpdateOneParams<T> updateOneParams) where T : class, new()
        {
            if (updateOneParams.IsSpliteTable)
            {
                return await GetSplitUpdateable(updateOneParams).ExecuteCommandAsync();
            }
            else
            {
                return await GetUpdateable(updateOneParams).ExecuteCommandAsync();
            }
        }
        #endregion


        #region 批量更新

        #region 获取实例
        /// <summary>
        /// 获取批量插入实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="updateListParams"></param>
        /// <returns></returns>
        public static IUpdateable<T> GetUpdateable<T>(UpdateListParams<T> updateListParams) where T : class, new()
        {
            IUpdateable<T> query;
            if (updateListParams.Entities != null)//有实体
            {
                query = Db.UpdateableWithAttr(updateListParams.Entities);
                if (updateListParams.IgnoreColumns != null)
                {
                    query.IgnoreColumns(updateListParams.IgnoreColumns);
                }
                if (updateListParams.UpdateColumns != null)
                {
                    query.UpdateColumns(updateListParams.UpdateColumns);
                }
                if (updateListParams.WhereColumns != null)
                {
                    query.WhereColumns(updateListParams.WhereColumns);
                }
            }
            else//无实体
            {
                query = Db.UpdateableWithAttr<T>().SetColumns(updateListParams.SetsColumns);
            }
            if (updateListParams.Where != null)
            {
                query.Where(updateListParams.Where);
            }
            return query;
        }

        /// <summary>
        /// 获取批量插入分表实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="updateListParams"></param>
        /// <returns></returns>
        public static SplitTableUpdateByObjectProvider<T> GetSplitUpdateable<T>(UpdateListParams<T> updateListParams) where T : class, new()
        {

            var query = GetUpdateable(updateListParams);
            return query.SplitTable();
        }
        #endregion

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="updateListParams"></param>
        /// <returns></returns>
        public static int UpdateList<T>(UpdateListParams<T> updateListParams) where T : class, new()
        {

            if (updateListParams.IsSpliteTable)
            {
                return GetSplitUpdateable(updateListParams).ExecuteCommand();
            }
            else
            {
                return GetUpdateable(updateListParams).ExecuteCommand();
            }
        }

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="updateListParams"></param>
        /// <returns></returns>
        public static async Task<int> UpdateListAsync<T>(UpdateListParams<T> updateListParams) where T : class, new()
        {
            if (updateListParams.IsSpliteTable)
            {
                return await GetSplitUpdateable(updateListParams).ExecuteCommandAsync();
            }
            else
            {
                return await GetUpdateable(updateListParams).ExecuteCommandAsync();
            }
        }

        #endregion


        #region
        #endregion
    }
}

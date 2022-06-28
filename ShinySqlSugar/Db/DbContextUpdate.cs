using SqlSugar;
using System.Threading.Tasks;

namespace ShinySqlSugar
{
    /// <summary>
    /// 数据库上下文更新方法
    /// </summary>
    public partial class DbContext
    {

        #region 更新单条
        /// <summary>
        /// 获取插入实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="updateOneParams"></param>
        /// <returns></returns>
        private static IUpdateable<T> GetUpdateable<T>(UpdateOneParams<T> updateOneParams) where T : class, new()
        {
            var query = Db.UpdateableWithAttr(updateOneParams.Entity).IgnoreColumns(updateOneParams.IgnoreNullColumns);

            if (updateOneParams.IgnoreColumns != null)
            {
                query.IgnoreColumns(updateOneParams.IgnoreColumns);
            }
            if (updateOneParams.Where != null)
            {
                query.Where(updateOneParams.Where);
            }
            if (updateOneParams.UpdateColumns != null)
            {
                query.UpdateColumns(updateOneParams.UpdateColumns);
            }
            return query;
        }

        /// <summary>
        /// 根据实体更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="updateOneParams"></param>
        /// <returns></returns>
        public static int Update<T>(UpdateOneParams<T> updateOneParams) where T : class, new()
        {
            var query = GetUpdateable(updateOneParams);
            return query.ExecuteCommand();
        }

        /// <summary>
        /// 根据实体更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="updateOneParams"></param>
        /// <returns></returns>
        public static async Task<int> UpdateAsync<T>(UpdateOneParams<T> updateOneParams) where T : class, new()
        {
            var query = GetUpdateable(updateOneParams);
            return await query.ExecuteCommandAsync();
        }
        #endregion


        #region 批量更新

        /// <summary>
        /// 获取批量插入实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="updateListParams"></param>
        /// <returns></returns>
        private static IUpdateable<T> GetUpdateable<T>(UpdateListParams<T> updateListParams) where T : class, new()
        {

            var query = Db.UpdateableWithAttr(updateListParams.Entities);
            if (updateListParams.Where != null)
            {
                query.Where(updateListParams.Where);
            }
            if (updateListParams.UpdateColumns != null)
            {
                query.UpdateColumns(updateListParams.UpdateColumns);
            }
            if (updateListParams.IsSpliteTable)
            {
                query.SplitTable();
            }
            return query;
        }

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="updateListParams"></param>
        /// <returns></returns>
        public static int UpdateList<T>(UpdateListParams<T> updateListParams) where T : class, new()
        {
            var query = GetUpdateable(updateListParams);
            return query.ExecuteCommand();
        }

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="updateListParams"></param>
        /// <returns></returns>
        public static async Task<int> UpdateListAsync<T>(UpdateListParams<T> updateListParams) where T : class, new()
        {
            var query = GetUpdateable(updateListParams);
            return await query.ExecuteCommandAsync();
        }

        #endregion


        #region
        #endregion
    }
}

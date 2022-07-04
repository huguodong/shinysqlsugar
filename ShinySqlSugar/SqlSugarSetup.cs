using Microsoft.Extensions.DependencyInjection;
using SqlSugar;
using System;
using System.Collections.Generic;


namespace ShinySqlSugar
{
    /// <summary>
    /// SqlSugar扩展类
    /// </summary>
    public static class SqlSugarSetUp
    {
        /// <summary>
        /// 添加SqlSugar服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="connectionConfigs">连接配置</param>
        /// <param name="filters">动态表过滤器</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void AddSqlSugar(this IServiceCollection services, List<ConnectionConfig> connectionConfigs, List<TableFilters> filters = null)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            DbConfig.ConnectionConfigs = connectionConfigs;
            if (filters != null)
            {
                DbConfig.Filters = filters;
            }
        }
    }
}

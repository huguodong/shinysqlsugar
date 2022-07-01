﻿using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShinySqlSugar
{
    /// <summary>
    /// 删除参数
    /// </summary>
    public class DeleteParams<T> where T : class, new()
    {

        /// <summary>
        /// 是否分表
        /// </summary>
        public bool IsSpliteTable { get; set; } = false;

        /// <summary>
        /// 分表条件
        /// </summary>
        public Func<List<SplitTableInfo>, IEnumerable<SplitTableInfo>> SplitTable { get; set; }

    }

    /// <summary>
    /// 单个删除参数
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DeleteOneParams<T> : DeleteParams<T> where T : class, new()
    {
        /// <summary>
        /// 实体类
        /// </summary>
        public T Entity { get; set; }

    }

    /// <summary>
    /// 批量删除参数
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DeleteListParams<T> : DeleteParams<T> where T : class, new()
    {
        /// <summary>
        /// 实体类
        /// </summary>
        public List<T> Entities { get; set; }
    }
}

<h1>一、说明</h1>
<p>ShinySqlSugar包是一个操作数据库得orm，基于果糖网的sqlsugar的单例模式的二次封装，无需每个项目都要新建dbcontext类，只需要注入ShinySqlSugar就可以使用sqlsugar的所有功能。</p>
<h1>二、安装</h1>
<p>nuget搜索ShinySqlSugar直接安装</p>
<p><img src="https://img2022.cnblogs.com/blog/668465/202206/668465-20220622161606049-506023207.png" /></p>
<p>配置文件格式</p>
<pre class="language-json highlighter-hljs"><code>"ConnectionConfigs": [
    {
      "ConfigId": "1",
      "ConnectionString": "Server=xxx",
      "DbType": "SqlServer",
      "IsAutoCloseConnection": true
    },
    {
      "ConfigId": "2",
      "ConnectionString": "Server=xxx",
      "DbType": "SqlServer",
      "IsAutoCloseConnection": true
    }
  ]</code></pre>
<p>ConfigureServices里面注入就行,这里我用的furion里面获取配置文件到类的方法，如果没用furion可以用其他方法获取到ConnectionConfigs</p>
<pre class="language-csharp highlighter-hljs"><code>        var config = App.GetConfig&lt;List&lt;ConnectionConfig&gt;&gt;("ConnectionConfigs", true);
        services.AddSqlSugar(config);</code></pre>
<h1>三、使用</h1>
<p>代码里直接用</p>
<pre class="language-csharp highlighter-hljs"><code>var data = await DbContext.Db.GetConnection("1").Queryable&lt;dynamic&gt;().AS("User").ToListAsync();</code></pre>
<p>也可以在构造函数里定义</p>
<p><img src="https://img2022.cnblogs.com/blog/668465/202206/668465-20220622144953081-604932684.png" /></p>
<p>需要添加表过滤器，直接使用AddTableFilter方法</p>
<p><img src="https://img2022.cnblogs.com/blog/668465/202206/668465-20220623082103083-1875784540.png" /></p>
<p>操作数据库直接调用静态方法就行</p>
<p><img src="https://img2022.cnblogs.com/blog/668465/202206/668465-20220628181119335-1701212514.png" /></p>
<p><img src="https://img2022.cnblogs.com/blog/668465/202206/668465-20220628181042002-1702558945.png" /></p>

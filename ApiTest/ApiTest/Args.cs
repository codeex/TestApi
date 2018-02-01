using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTest
{
    public class Args<T>
    {
        /// <summary>
        /// 请求的业务名称
        /// </summary>
        public string m { get; set; }

        /// <summary>
        /// m的版本号
        /// </summary>
        public string mv { get; set; }

        /// <summary>
        /// 客户端类型
        /// </summary>
        public string ct { get; set; }


        /// <summary>
        /// 客户版本号
        /// </summary>
        public string cv { get; set; }


        /// <summary>
        /// 请求的id，一般用Guid，便于日志对应请求和响应，要求返还的ResponseId原样复制
        /// </summary>
        public string rid { get; set; }


        /// <summary>
        /// 授权令牌
        /// </summary>
        public string tk { get; set; }

        public string cs { get; set; }
     
        /// <summary>
        /// 业务数据
        /// </summary>
        public T v { get; set; }

        public bool icp { get; set; }
      
        /// <summary>
        /// 可以路由的Uri
        /// </summary>
        public string uri { get; set; }

       /// <summary>
        /// 语言
        /// </summary>
        public string lg { get; set; }
    }

    public class Result<T>
    {
        public string rid { get; set; }
        public int c { get; set; }
        public string msg { get; set; }
        public string cs { get; set; }

        /// <summary>
        /// 业务数据
        /// </summary>
        public T v { get; set; }

        public bool icp { get; set; }

        /// <summary>
        /// 可以路由的Uri
        /// </summary>
        public string uri { get; set; }

        /// <summary>
        /// 语言
        /// </summary>
        public string lg { get; set; }
    }

    public class TokenResult
    {
        public string Token { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;

namespace ApiTest
{
    public class SqlConnFactory
    {
        /// <summary>
        /// 切记：调用方负责关闭
        /// </summary>
        /// <returns>打开的连接</returns>
        public static IDbConnection GetConn()
        {
            var dbconnStr = Program.Config.DbConn;
            var conn = new MySqlConnection(dbconnStr);
            conn.Open();
            return conn;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper;

namespace ApiTest
{
    public class ApiRepository
    {
        private IDbConnection _conn;
        public ApiRepository(IDbConnection conn)
        {
            _conn = conn;
        }

        public IEnumerable<ApiModel> GetAll()
        {
            return this._conn.Query<ApiModel>("select * from scmapi order by `Sort`");
        }
    }
}

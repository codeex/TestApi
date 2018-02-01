using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTest
{
    class Program
    {
        public static TestCfg Config ;
        static void Main(string[] args)
        {
            Config = TestCfg.InitCfg();
            List<ApiModel> apis;
            using (var conn = SqlConnFactory.GetConn())
            {
                var resp = new ApiRepository(conn);
                apis = resp.GetAll().ToList();
                conn.Close();
            }

            Task.Run(async () =>
            {
                var ssid = await ApiHelper.GetSSID();
                var token = await ApiHelper.GetToken(ssid);
                apis.ForEach( x =>
                {
                    var a = ApiHelper.TestApi($"{Program.Config.HttpProxy}{x.Uri}", token,
                        JsonConvert.DeserializeObject(x.PostParam)).Result;
                });
                
            });
           
            Console.WriteLine("start api test...");
            Console.ReadKey(true);
        }
    }
}

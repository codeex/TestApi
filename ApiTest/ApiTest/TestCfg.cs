using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;

namespace ApiTest
{
    public class TestCfg
    {
        protected TestCfg()
        {
            this.Interface = new List<string>();
            this.InterfaceParam = new List<string>();
        }

        /// <summary>
        /// 返回初始化配置信息
        /// </summary>
        /// <returns></returns>
        public static TestCfg InitCfg()
        {
            try
            {
                var dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                var file = Path.Combine(dir, "cfg.json");
                using (StreamReader sr = new StreamReader(file, Encoding.UTF8))
                {
                    var s = sr.ReadToEnd();
                    var cfg = JsonConvert.DeserializeObject<TestCfg>(s);
                    return cfg;
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
                return null;
            }
        }
        public string Login { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string HttpProxy { get; set; }
        public string DbConn { get; set; }
        public List<string> Interface { get; set; }
        public List<string> InterfaceParam { get; set; }
    }
}

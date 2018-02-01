using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace ApiTest
{
    public class ApiHelper
    {
        private static HttpClient _client = new HttpClient();
        private static object _Context = new object();
        public static async Task<string> GetSSID()
        {
            var md5 = MD5.Create();
            var bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(Program.Config.Password));
            string byte2String = null;
            for (int i = 0; i < bytes.Length; i++)
            {
                byte2String += bytes[i].ToString("x2");
            }

            NameValueCollection dict = HttpUtility.ParseQueryString(String.Empty);
            dict.Add("userid", Program.Config.User);
            dict.Add("password", byte2String);
            dict.Add("token", "");
            byte[] byteArray = Encoding.UTF8.GetBytes(dict.ToString());

            WebRequest request = WebRequest.CreateHttp(Program.Config.Login + "/Login/Login");
            request.Proxy = null;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded;";
            request.ContentLength = byteArray.Length;
            using (Stream newStream = request.GetRequestStream())
            {
                newStream.Write(byteArray, 0, byteArray.Length);
                newStream.Close();
            }
            var resp = await request.GetResponseAsync();
            string sResp = "";
            using (Stream s = resp.GetResponseStream())
            {
                using (var rs = new MemoryStream())
                {
                    await s.CopyToAsync(rs, 1024);
                    sResp = Encoding.UTF8.GetString(rs.ToArray());
                }
                s.Close();
            }
            if (sResp.Contains("\"flag\":true"))
            {
                string cookie = resp.Headers.Get("Set-Cookie");
                if (!string.IsNullOrEmpty(cookie))
                {
                    var n = cookie.IndexOf("SSID=");
                    cookie = cookie.Substring(n + 5, cookie.IndexOf(";") - n - 5);
                    //CookieContainer cc = new CookieContainer();
                    //cc.SetCookies(new Uri(Program.Config.Login),cookie);
                    var ssid = cookie;
                    return ssid.ToString();
                }
            }
            
            return "";
        }
        public static async Task<string> GetToken(string ssid)
        {
            var uri = Program.Config.HttpProxy + "SCMBASEService/GetTokenBySsid";
            var req = WebRequest.CreateHttp(new Uri(uri));
            req.Proxy = null;
            req.Method = "POST";
            req.ContentType = "application/json";
            Args<object> p = new Args<object>()
            {
                v = new {SSID = ssid},
                tk = ""
            };
            var st = WriteMsg();
            string args = JsonConvert.SerializeObject(p);
            byte[] byteArray = Encoding.UTF8.GetBytes(args);
            req.ContentLength = byteArray.Length;
            using (Stream newStream = req.GetRequestStream())
            {

                newStream.Write(byteArray, 0, byteArray.Length);
                newStream.Close();
            }

            var resp = await req.GetResponseAsync();
            string sResp = "";
            
            using (Stream s = resp.GetResponseStream())
            {
                using (var rs = new MemoryStream())
                {
                    await s.CopyToAsync(rs, 1024);
                    sResp = Encoding.UTF8.GetString(rs.ToArray());
                }
                s.Close();
            }
            var rst = JsonConvert.DeserializeObject<Result<TokenResult>>(sResp);
            WriteMsg(rst, st, p, uri);
            if (rst.c == 200)
            {
                return rst.v.Token;
            }
            else
            {
                return "";
            }
        }

        private static void WriteMsg<T1,T2>(Result<T1> rst,Stopwatch st, Args<T2> args, string uri)
        {
            lock (_Context)
            {
                Console.WriteLine($"==================={uri}=====================");
                Console.WriteLine($"=={args.tk}:{args.v.ToString()}");
                Console.WriteLine($"=={rst.c}:{rst.msg}");
                st.Stop();
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine($"*耗时:{st.ElapsedMilliseconds} ms");
                Console.ForegroundColor = ConsoleColor.White;
                if (st.ElapsedMilliseconds > 800)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("!!耗时警告!!");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                if (rst.c != 200)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("!!Api测试失败!!");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                //Console.WriteLine(
                //    "===============================================================================================");
            }
        }
        private static Stopwatch WriteMsg()
        {
            var st = new Stopwatch();
            st.Start();
            return st;
        }
        public static async Task<string> TestApi(string uri, string token, object param)
        {
            var req = WebRequest.CreateHttp(new Uri(uri));
            req.Proxy = null;
            req.Method = "POST";
            req.ContentType = "application/json";
            Args<object> p = new Args<object>()
            {
                v = param,
                tk = token
            };
            var st = WriteMsg();
            string args = JsonConvert.SerializeObject(p);
            byte[] byteArray = Encoding.UTF8.GetBytes(args);
            req.ContentLength = byteArray.Length;
            using (Stream newStream = req.GetRequestStream())
            {
                newStream.Write(byteArray, 0, byteArray.Length);
                newStream.Close();
            }

            var resp = await req.GetResponseAsync();
            string sResp = "";
            using (Stream s = resp.GetResponseStream())
            {
                using (var rs = new MemoryStream())
                {
                    await s.CopyToAsync(rs, 1024);
                    sResp = Encoding.UTF8.GetString(rs.ToArray());
                }
                s.Close();
            }
            var rst = JsonConvert.DeserializeObject<Result<object>>(sResp);
            WriteMsg(rst,st, p, uri);
            if (rst.c == 200)
            {
                return "";
            }
            else
            {
                return rst.msg;
            }
        }
        
    }
}

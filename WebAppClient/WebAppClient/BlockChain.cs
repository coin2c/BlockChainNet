using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Newtonsoft.Json;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Text;
using System.IO;

namespace WebAppClient
{
    public class BlockChain : IBlockChain
    {
        public Uri BlockChainUri;
        public ICredentials Credentials;

        public BlockChain()
        {

        }

        public BlockChain(string sUri)
        {
            BlockChainUri = new Uri(sUri);
        }

/*
        public JObject InvokeMethod(string a_sMethod, params object[] a_params)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(BlockChainUri);
            webRequest.Credentials = Credentials;

            webRequest.ContentType = "application/json-rpc";
            webRequest.Method = "POST";

            JObject joe = new JObject();
            joe["jsonrpc"] = "1.0";
            joe["id"] = "1";
            joe["method"] = a_sMethod;

            if (a_params != null)
            {
                if (a_params.Length > 0)
                {
                    JArray props = new JArray();
                    foreach (var p in a_params)
                    {
                        props.Add(p);
                    }
                    joe.Add(new JProperty("params", props));
                }
            }

            string s = JsonConvert.SerializeObject(joe);
            // serialize json for the request
            byte[] byteArray = Encoding.UTF8.GetBytes(s);
            webRequest.ContentLength = byteArray.Length;

            using (Stream dataStream = webRequest.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
            }

            using (WebResponse webResponse = webRequest.GetResponse())
            {
                using (Stream str = webResponse.GetResponseStream())
                {
                    using (StreamReader sr = new StreamReader(str))
                    {
                        return JsonConvert.DeserializeObject<JObject>(sr.ReadToEnd());
                    }
                }
            }
        }
 */

        static string CreateAuthorization(string realm, string userName, string password)
        {
            string auth = ((realm != null) && (realm.Length > 0) ?
            realm + @"\" : "") + userName + ":" + password;
            auth = Convert.ToBase64String(Encoding.Default.GetBytes(auth));
            return auth;
        }


        public JObject InvokeMethod(string a_sMethod, params object[] a_params)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(BlockChainUri);
            webRequest.Credentials = Credentials;

            string auth = CreateAuthorization("", "4bfb9301-090e-4f6f-9ba8-1edba1065cf5", "c2csecret!");
            webRequest.Headers["Authorization"] = "Basic " + auth;

            webRequest.ContentType = "application/json";
            webRequest.Method = "POST";

            JObject joe = new JObject();
            joe["jsonrpc"] = "2.0";
            joe["id"] = "1";
            joe["method"] = a_sMethod;

            if (a_params != null)
            {
                if (a_params.Length > 0)
                {
                    JArray props = new JArray();
                    foreach (var p in a_params)
                    {
                        props.Add(p);
                    }
                    joe.Add(new JProperty("params", props));
                }
            }

            string s = JsonConvert.SerializeObject(joe);
            // serialize json for the request
            byte[] byteArray = Encoding.UTF8.GetBytes(s);
            webRequest.ContentLength = byteArray.Length;

            try
            {
                using (Stream dataStream = webRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                }
            }
            catch (WebException we)
            {
                //inner exception is socket
                //{"A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 23.23.246.5:8332"}
                throw;
            }
            WebResponse webResponse = null;
            try
            {
                using (webResponse = webRequest.GetResponse())
                {
                    using (Stream str = webResponse.GetResponseStream())
                    {
                        using (StreamReader sr = new StreamReader(str))
                        {
                            var tempRet = JsonConvert.DeserializeObject<JObject>(sr.ReadToEnd());
                            return tempRet;
                        }
                    }
                }
            }
            catch (WebException webex)
            {

                using (Stream str = webex.Response.GetResponseStream())
                {
                    using (StreamReader sr = new StreamReader(str))
                    {
                        var tempRet = JsonConvert.DeserializeObject<JObject>(sr.ReadToEnd());
                        return tempRet;
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public JObject GetInfo()
        {
            return InvokeMethod("getinfo")["result"] as JObject;
        }

        public bool WalletPassPhrase(string pass, int timeout)
        {
            return (bool)InvokeMethod("walletpassphrase", pass, timeout)["result"];
        }

        public string GetAccount(string sAddress)
        {
            return (string)InvokeMethod("getaccount", sAddress)["result"];
        }

        public JObject ListReceivedByAddress()
        {
            return InvokeMethod("listreceivedbyaddress")["result"] as JObject;
        }



        public float GetDifficulty()
        {
            return (float)InvokeMethod("getdifficulty")["result"];
        }

        // deprecated ?
        public string GetBlockByCount(int a_height)
        {
            return InvokeMethod("getblockbycount", a_height)["result"].ToString();
        }

        public int GetBlockCount()
        {
            return (int)InvokeMethod("getblockcount")["result"];
        }

        public int GetBlockNumber()
        {
            return (int)InvokeMethod("getblocknumber")["result"];
        }

        public int GetConnectionCount()
        {
            return (int)InvokeMethod("getconnectioncount")["result"];
        }

        public bool GetGenerate()
        {
            return (bool)InvokeMethod("getgenerate")["result"];
        }

        public float GetHashesPerSec()
        {
            return (float)InvokeMethod("gethashespersec")["result"];
        }

        public string GetNewAddress()
        {
            return InvokeMethod("getnewaddress")["result"].ToString();
        }

        public float GetReceivedByAccount(string a_account, int a_minconf = 1)
        {
            return (float)InvokeMethod("getreceivedbyaccount", a_account, a_minconf)["result"];
        }

        public float GetReceivedByAddress(string a_address, int a_minconf = 1)
        {
            return (float)InvokeMethod("getreceivedbyaddress", a_address, a_minconf)["result"];
        }




    }
}
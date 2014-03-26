using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAppClient
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            BlockChain bc = new BlockChain("http://rpc.blockchain.info:80");
            bc.Credentials = new NetworkCredential("4bfb9301-090e-4f6f-9ba8-1edba1065cf5", "c2csecret!");
/*
            JObject i = bc.GetInfo();

            int s = (int)i["balance"];

            float d = bc.GetDifficulty();

           string c = bc.GetBlockByCount(7);
*/
            JObject rec = bc.ListReceivedByAddress();

            if (bc.WalletPassPhrase("c22csecret!", 30))
            {
                //string n = bc.GetNewAddress();

                string a = bc.GetAccount("1PAJ3JjFAEPGaAAYNirD7NxuUwRnjt6Aac");
            }



        }

        protected void Button1_Click(object sender, EventArgs e)
        {


        }
    }
}
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
            bc.Credentials = new NetworkCredential("xx", "yy");
           // var i = bc.GetInfo();

           var d = bc.GetBlockByCount(7);



        }

        protected void Button1_Click(object sender, EventArgs e)
        {


        }
    }
}
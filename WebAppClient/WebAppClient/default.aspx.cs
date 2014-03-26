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

            BlockChain bc = new BlockChain("https://rpc.blockchain.info:443");
            bc.Credentials = new NetworkCredential("mylogin", "mypass");

            var i = bc.GetInfo();

          //  var d = bc.GetDifficulty();



        }

        protected void Button1_Click(object sender, EventArgs e)
        {


        }
    }
}
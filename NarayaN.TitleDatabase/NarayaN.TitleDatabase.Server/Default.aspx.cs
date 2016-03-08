using NarayaN.TitleDatabase.Server.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NarayaN.TitleDatabase.Server
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<string> ApplicationRoutes = new List<string>();

            foreach (Route r in RouteTable.Routes)
                ApplicationRoutes.Add(r.Url);

            gvServices.DataSource = ApplicationRoutes;
            gvServices.DataBind();


            BSTmdb bs = new BSTmdb();
            bs.CreateMovieWithImdbId("tt3498820");
        }
    }
}
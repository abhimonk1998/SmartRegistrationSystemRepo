using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RepositoryTestApplication
{
    public partial class _Default : Page
    {
        ServiceReference1.Service1Client repoServiceClient;
        protected void Page_Load(object sender, EventArgs e)
        {
            repoServiceClient = new ServiceReference1.Service1Client();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            repoServiceClient.createStudent("Abhi", "K", "Hello");
        }
    }
}
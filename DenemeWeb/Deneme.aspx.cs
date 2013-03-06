using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DenemeWeb
{
    public partial class Deneme : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=EMRECEYLAN-PC;Initial Catalog=Emergency Service;Integrated Security=True");
            var cmd = new SqlCommand("sp_HastaneGetir", conn) { CommandType = CommandType.StoredProcedure };
            conn.Open();

            try
            {
                var hastaneID = new SqlParameter("@HastaneID", SqlDbType.Int) { Direction = ParameterDirection.Output };
                var hastaneAdi = new SqlParameter("@HastaneAdi", SqlDbType.VarChar) { Direction = ParameterDirection.Output };
                hastaneAdi.Size = 100;
                var hastaneAdres = new SqlParameter("@HastaneAdres", SqlDbType.VarChar) { Direction = ParameterDirection.Output };
                hastaneAdres.Size = 200;
                var hastaneLocationLat = new SqlParameter("@lat_Hastane", SqlDbType.Float) { Direction = ParameterDirection.Output };
                var hastaneLocationLong = new SqlParameter("@long_Hastane", SqlDbType.Float) { Direction = ParameterDirection.Output };

                cmd.Parameters.Add(hastaneID);
                cmd.Parameters.Add(hastaneAdi);
                cmd.Parameters.Add(hastaneAdres);
                cmd.Parameters.Add(hastaneLocationLat);
                cmd.Parameters.Add(hastaneLocationLong);
                cmd.ExecuteNonQuery();

                Label1.Text = hastaneAdi.Value.ToString();
                Label2.Text = hastaneID.Value.ToString();
                Label3.Text = hastaneAdres.Value.ToString();
                Label4.Text = hastaneLocationLat.Value.ToString();
                Label5.Text = hastaneLocationLong.Value.ToString();

            }

            finally
            {
                conn.Close();
            }
        }
    }
}
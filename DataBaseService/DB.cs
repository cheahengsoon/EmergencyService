using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DataBaseService
{
    public class DB
    {
        SqlConnection sqlConn;
        
        public void connection() 
        {
            sqlConn = new SqlConnection();
            sqlConn.ConnectionString = "Data Source=EMRECEYLAN-PC;Initial Catalog=Emergency Service;Integrated Security=True";
        
        }

        internal void olayKayit(string olayTürü, string kisiSayi, string adres, double latitude, double longitude)
        {
            
            connection();

            SqlCommand cmd = new SqlCommand("sp_OlayKayit", sqlConn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@olayTürü", SqlDbType.VarChar, 50).Value = olayTürü;

            cmd.Parameters.Add("@kisi_Sayi", SqlDbType.Int).Value = kisiSayi;

            cmd.Parameters.Add("@adres", SqlDbType.VarChar, 200).Value = adres;

            cmd.Parameters.Add("@lat", SqlDbType.Float).Value = latitude;

            cmd.Parameters.Add("@long", SqlDbType.Float).Value = longitude;

            try
            {
                sqlConn.Open();

                cmd.ExecuteNonQuery();
            }

            finally
            {
                sqlConn.Close();
            }
        }

        internal List<string> HastaneGetir()
        {
            
            connection();
            SqlDataReader dr;
            List<string> hastaneListe = new List<string>();

            SqlCommand cmd = new SqlCommand("sp_HastaneGetir", sqlConn)
            {
                CommandType = CommandType.StoredProcedure
            };

            //var hastaneID = new SqlParameter("@HastaneID", SqlDbType.Int) { Direction = ParameterDirection.Output };
            //var hastaneAdi = new SqlParameter("@HastaneAdi", SqlDbType.VarChar) { Direction = ParameterDirection.Output };
            //hastaneAdi.Size = 100;
            //var hastaneAdres = new SqlParameter("@HastaneAdres", SqlDbType.VarChar) { Direction = ParameterDirection.Output };
            //hastaneAdres.Size = 200;
            //var hastaneLocationLat = new SqlParameter("@lat_Hastane", SqlDbType.Float) { Direction = ParameterDirection.Output };
            //var hastaneLocationLong = new SqlParameter("@long_Hastane", SqlDbType.Float) { Direction = ParameterDirection.Output };
   
            //cmd.Parameters.Add(hastaneID);
            //cmd.Parameters.Add(hastaneAdi);
            //cmd.Parameters.Add(hastaneAdres);
            //cmd.Parameters.Add(hastaneLocationLat);
            //cmd.Parameters.Add(hastaneLocationLong);

            try
            {
                sqlConn.Open();
                //cmd.ExecuteNonQuery();
                dr = cmd.ExecuteReader();
                while (dr.Read()) 
                {
                    hastaneListe.Add(dr["HastaneAdi"].ToString());
                }
                dr.Close();
            }

            finally
            {
                sqlConn.Close();
            }

            //string hastaneler = hastaneAdi.Value.ToString();
            //string hastaneAdlari = hastaneListe[1].ToString();
            return hastaneListe;




        }
    }
}
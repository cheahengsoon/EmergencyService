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
        
        private void connection() 
        {
            sqlConn = new SqlConnection();
            sqlConn.ConnectionString = "Data Source=z2r1m0bhyk.database.windows.net;Initial Catalog=Emergency Service;Persist Security Info=True;User ID=coderunner;Password=emre#1417";
        
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

        internal Tuple<List<string>, List<string>, List<string>, List<string>, List<string>> HastaneGetir()
        {
            
            connection();
            SqlDataReader dr;
            List<string> hastaneIDListe = new List<string>();
            List<string> hastaneAdListe = new List<string>();
            List<string> hastaneAdresListe = new List<string>();
            List<string> hastaneLocationLatListe=new List<string>();
            List<string> hastaneLocationLongListe = new List<string>(); 

            SqlCommand cmd = new SqlCommand("sp_HastaneGetir", sqlConn)
            {
                CommandType = CommandType.StoredProcedure
            };

            

            try
            {
                sqlConn.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read()) 
                {   
                    hastaneIDListe.Add(dr[0].ToString());
                    hastaneAdListe.Add(dr[1].ToString());
                    hastaneAdresListe.Add(dr[2].ToString());
                    hastaneLocationLatListe.Add(dr[3].ToString());
                    hastaneLocationLongListe.Add(dr[4].ToString());
                    
                }
                dr.Close();
            }

            finally
            {
                sqlConn.Close();
            }

            
            return new Tuple<List<string>, List<string>, List<string>, List<string>, List<string>>(hastaneIDListe,hastaneAdListe,hastaneAdresListe,hastaneLocationLatListe,hastaneLocationLongListe);




        }

        internal Tuple<List<string>, List<string>, List<string>, List<string>, List<string>, List<string>> olayGoster(double hastaneLat, double hastaneLong)
        {

            
            SqlDataReader dr2;
            List<string> olayIDListe = new List<string>();
            List<string> olayTurListe = new List<string>();
            List<string> olayKisiSayiListe = new List<string>();
            List<string> olayAdresListe = new List<string>();
            List<string> olayLocationLatListe = new List<string>();
            List<string> olayLocationLongListe = new List<string>(); 
            
            connection();
            SqlCommand cmd = new SqlCommand("sp_OlayGoster", sqlConn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@hastaneLat", SqlDbType.Float).Value = hastaneLat;
            cmd.Parameters.Add("@hastaneLong", SqlDbType.Float).Value = hastaneLong;

            try
            {
                sqlConn.Open();
                dr2 = cmd.ExecuteReader();
                while (dr2.Read())
                {
                    olayIDListe.Add(dr2[0].ToString());
                    olayTurListe.Add(dr2[1].ToString());
                    olayKisiSayiListe.Add(dr2[2].ToString());
                    olayAdresListe.Add(dr2[3].ToString());
                    olayLocationLatListe.Add(dr2[4].ToString());
                    olayLocationLongListe.Add(dr2[5].ToString());

                }
                dr2.Close();
            }

            finally
            {
                sqlConn.Close();
            }

            return new Tuple<List<string>, List<string>, List<string>, List<string>, List<string>, List<string>>(olayIDListe, olayTurListe, olayKisiSayiListe, olayAdresListe, olayLocationLatListe, olayLocationLongListe);
            
        }
    }
}
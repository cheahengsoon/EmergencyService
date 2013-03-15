using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace DataBaseService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public string KarsilamaMesaji(string isim)
        {
            return isim + "Windows Store Uygulamasina Hosgeldiniz!!!";
        }

        public string olayEkle(string olayTürü, string kisiSayi, string adres, double latitude, double longitude)
        {
            DB db = new DB();
            db.olayKayit(olayTürü, kisiSayi, adres, latitude, longitude);
            return "Tamam";
            
        }
        public Tuple<List<string>, List<string>, List<string>, List<string>, List<string>> HastaneGetir()
        {
            
            DB db2 = new DB();
            Tuple<List<string>, List<string>, List<string>, List<string>, List<string>> hastaneBilgi = db2.HastaneGetir();
            //List<string> hastaneIDListe = db2.HastaneGetir().Item1;
            //List<string> hastaneAdListe = db2.HastaneGetir().Item2;
            //List<string> hastaneAdresListe = db2.HastaneGetir().Item3;
            //List<string> hastaneLocationLatListe = db2.HastaneGetir().Item4;
            //List<string> hastaneLocationLongListe = db2.HastaneGetir().Item5;
            return hastaneBilgi;   
        }

        public Tuple<List<string>, List<string>, List<string>, List<string>, List<string>, List<string>> olayGoster(double hastaneLat, double hastaneLong) 
        {
            DB db3 = new DB();
            Tuple<List<string>, List<string>, List<string>, List<string>, List<string>, List<string>> olayBilgi = db3.olayGoster(hastaneLat, hastaneLong);
            //List<string> olayIDListe = olayBilgi.Item1;
            //List<string> olayTurListe = olayBilgi.Item2;
            //List<string> olayKisiSayiListe = olayBilgi.Item3;
            //List<string> olayAdresListe = olayBilgi.Item4;
            //List<string> olayLocationLatListe = olayBilgi.Item5;
            //List<string> olayLocationLongListe = db3.olayGoster(hastaneLat, hastaneLong).Item6;
            return olayBilgi;
        }

        
    }
}

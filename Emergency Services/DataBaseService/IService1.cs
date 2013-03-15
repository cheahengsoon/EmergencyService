using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Media;
using System.IO;

namespace DataBaseService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        string KarsilamaMesaji(string isim);

        [OperationContract]
        string olayEkle(string olayTürü, string kisiSayi, string adres, double latitude, double longitude);

        [OperationContract]
        Tuple<List<string>, List<string>, List<string>, List<string>, List<string>> HastaneGetir();

        [OperationContract]
        Tuple<List<string>, List<string>, List<string>, List<string>, List<string>, List<string>> olayGoster(double hastaneLat, double hastaneLong);
        
        
        
        // TODO: Add your service operations here
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    
}

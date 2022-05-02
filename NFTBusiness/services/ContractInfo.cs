using System.Linq;
using System.IO;
using Newtonsoft.Json.Linq;
using NFT_Cert_Issue_Check.NFTBusiness.interfaces;
using System;

namespace NFT_Cert_Issue_Check.NFTBusiness.services
{
    public class ContractInfo : IContractInfo
    {
        string _contractPath;

        public string getContractPath()
        {
            return _contractPath;
        }

        public void setContractPath(string pathFileName)
        {
            _contractPath = pathFileName;
        }

        public string LoadABI()
        {
            string _message = "";
            try
            {
                StreamReader r = new StreamReader(_contractPath);
                string filestr = r.ReadToEnd();
                JObject json = JObject.Parse(filestr);
                _message = json["abi"].ToString();
            }
            catch (Exception e)
            {
                _message = "LoadABIError:" + e.Message;
            }
            return _message;


        }
    }
}

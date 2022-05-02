
using System.Drawing;

namespace NFT_Cert_Issue_Check.NFTBusiness.interfaces
{
    public interface IMint
    {

        /*string setABI(string ABI);

        string owner_address(string owner_address);
        string contract_address(string contract_address);
        string authority_address(string authority_address); //Wallet Address

        string pkey(string pkey);
        int chainId(int chainId);
        */

        public string owner_address { get; set; }
        public string ABI { get; set; }
        public string authority_address { get; set; }
        public string contract_address { get; set; }
        public string pkey { get; set; }
        public int chainId { get; set; }


        public string Transaction(); //return transaction hash id

        public Bitmap MintCert();




    }
}

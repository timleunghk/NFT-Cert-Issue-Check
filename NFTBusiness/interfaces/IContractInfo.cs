
namespace NFT_Cert_Issue_Check.NFTBusiness.interfaces
{
    public interface IContractInfo
    {
        string getContractPath();
        void setContractPath(string pathFileName);
        string LoadABI();



    }
}

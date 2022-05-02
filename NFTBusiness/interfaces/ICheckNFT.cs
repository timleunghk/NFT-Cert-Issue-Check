using System.Collections.Generic;

namespace NFT_Cert_Issue_Check.NFTBusiness.interfaces
{
    public interface ICheckNFT
    {
        List<string> CheckByTransactionHash(string txHash);
    }
}

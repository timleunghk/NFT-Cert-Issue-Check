using NFT_Cert_Issue_Check.NFTBusiness.interfaces;
using System;
using System.Collections.Generic;
using Nethereum.Web3;

namespace NFT_Cert_Issue_Check.NFTBusiness.services
{
    public class CheckNFT:ICheckNFT
    {
        public List<string> CheckByTransactionHash(string txHash)
        {
            var url = "https://rinkeby.infura.io/v3/38e2ff674539433b8a3456eeaf72ae2f"; //Please goto infura.io to apply your own endpoint
            var web3 = new Web3(url);
            var details_2 = web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(txHash);
            var details = web3.Eth.Transactions.GetTransactionByHash.SendRequestAsync(txHash);
            var block = web3.Eth.Blocks.GetBlockWithTransactionsByNumber.SendRequestAsync(details.Result.BlockNumber);

            List<string> listResult = new List<string>();

            double timestamp = (double)block.Result.Timestamp.Value;
            details_2.Wait();
            details.Wait();
            if (details.IsCompletedSuccessfully)
            {
                listResult.Add("This certificate is issued successfully");
                listResult.Add("Certification Name:" + details_2.Result.Logs[0]["address"]);
                listResult.Add("Certification Issue Date/Time:" + UnixTimeStampToDateTime(timestamp) + "GMT");
                listResult.Add("Issuer Name:" + details.Result.From);
                listResult.Add("Owner Name: " + details.Result.To);
            }

            else
                listResult.Add("This certificate is issued but not successful");

            return listResult;

        }

        private static string UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToUniversalTime();

            return dtDateTime.ToString("dd/MM/yyyy hh:mm:ss");
        }
    }
}

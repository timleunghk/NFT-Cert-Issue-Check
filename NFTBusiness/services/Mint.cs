using System;
using System.Threading;
using System.Numerics;
using System.Threading.Tasks;
using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;
using Nethereum.Web3;
using System.IO;
using Newtonsoft.Json.Linq;
using Nethereum.Web3.Accounts;


using NFT_Cert_Issue_Check.NFTBusiness.interfaces;
using System.Drawing;
using System.Drawing.Drawing2D;
using QRCoder;

namespace NFT_Cert_Issue_Check.NFTBusiness.services
{
    public class Mint:IMint
    {
        private string _owner_address;
        private string _ABI;
        private string _authority_address;
        private string _contract_address;
        private string _pkey;
        private int _chainId;

        public string owner_address { get { return _owner_address; } set { _owner_address = value; } }

        public string ABI { get { return _ABI; } set { _ABI = value; } }
        public string authority_address { get { return _authority_address; } set { _authority_address = value; } }
        public string contract_address { get { return _contract_address; } set { _contract_address = value; } }
        public string pkey { get { return _pkey; } set { _pkey = value; } }
        public int chainId { get { return _chainId; } set { _chainId = value; } }

        public Bitmap MintCert()
        {
            /*
              url = "https://rinkeby.infura.io/v3/38e2ff674539433b8a3456eeaf72ae2f"; //Please goto infura.io to apply your own endpoint
              myaccount_address = "0xeF24E50b4E1a05d22480851a73a383d691D2c4f5";
              destination_account_address = "0xec174fEFDd67b2a4FF3D6c592FEcFBACDdFA0146";
              var wallet_account = new Account("065a29f39cbcbbf278efaad8282b462c48ec928672fcec493228ed3973005735", 4); //Private Key + ChainId
              contract_address = "0xd2260bb84E903f5FF227E377EE614D80d9B902CC"; //remote contract address
              var web3 = new Web3(wallet_account, url);
              contract = web3.Eth.GetContract(ABI, contract_address);
             */

            string _hashNo = Transaction();
            string _CertificateTo = this._owner_address;
            //string _IssueAuthority= this._authority_address;

 
            var bmp = (Bitmap)Image.FromFile("/app/Assets/NFTCertTemplate.jpg");

            _hashNo = "https://rinkeby.etherscan.io/tx/" + _hashNo;

            //Generate QR Code -- Hash Code: 
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(_hashNo, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);
            Bitmap qrCodeImage_resized = new Bitmap(qrCodeImage, new Size(100, 100));


            // Owner Name
            RectangleF rectf = new RectangleF(179, 384, 700, 25);

            Graphics g = Graphics.FromImage(bmp);




            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.DrawString(_CertificateTo, new Font("Tahoma", 18), Brushes.Black, rectf);

            g.DrawImage(qrCodeImage_resized, new Point(470, 649));

            g.Flush();





            return bmp;

        }

        public string Transaction()
        {
            string _result = "";
            var url = "https://rinkeby.infura.io/v3/38e2ff674539433b8a3456eeaf72ae2f";
            var wallet_account = new Account(_pkey, _chainId); //Private Key + ChainId
            var web3 = new Web3(wallet_account, url);
            byte[] endbyte = { 0x00 };

            ContractInfo contractInfo = new ContractInfo();
            contractInfo.setContractPath("/app/BuiltContract/FishToken.json");
            _ABI = contractInfo.LoadABI();
            Contract _contract = web3.Eth.GetContract(_ABI, _contract_address);


            try
            {

                HexBigInteger gas = new HexBigInteger(new BigInteger(5500000));
                HexBigInteger value = new HexBigInteger(new BigInteger(0));

                Task<string> reserve = _contract.GetFunction("reserveFish").SendTransactionAsync(_authority_address, gas, value, 1);
                reserve.Wait();
                //Check my balance after minting (reserveFish)
                Task<BigInteger> balance = _contract.GetFunction("balanceOf").CallAsync<BigInteger>(_authority_address, 1);
                balance.Wait();

                Task<string> transfer = _contract.GetFunction("safeTransferFrom").SendTransactionAsync(_authority_address, gas, value, _authority_address, _owner_address, 1, 1, endbyte);
                transfer.Wait();

                _result = transfer.Result;

                return _result;
            }
            catch (Exception e)
            {
                return "Error:" + e.Message;
            }



        }
    }
}

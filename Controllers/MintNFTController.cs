using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NFT_Cert_Issue_Check.NFTBusiness.interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace NFT_Cert_Issue_Check.Controllers
{
    public class MintNFTController : ControllerBase
    {

        private readonly ILogger<MintNFTController> _logger;
        private readonly IMint _mint;


        public MintNFTController(IMint mint)
        {
            _mint = mint;
        }

        [HttpPost(nameof(MintAndTx))]
        public IActionResult MintAndTx([Required] string OwnerAccount,
            [Required] int ChainId,
            [Required] string AuthorityAccount,
            [Required] string PrimaryKey,
            [Required] string ContractAddress
            )
        {
            try
            {
                _mint.authority_address = AuthorityAccount;
                _mint.owner_address = OwnerAccount;
                _mint.chainId = ChainId;
                _mint.pkey = PrimaryKey;
                _mint.contract_address = ContractAddress;
                string result_hash = _mint.Transaction();
                Bitmap result = _mint.MintCert();
                result.Save("/app/" + result_hash + ".jpg");

                //return Ok(new { result_hash });

                var image = System.IO.File.OpenRead("/app/" + result_hash + ".jpg");
                return File(image, "image/jpeg");

                //  return Ok(new { formFiles.Count, Size = _fileIPFS.SizeConverter(formFiles.Sum(f => f.Length)), _fileList, _ipfsList });
                //return  Ok(new { result_hash, VirtualFileResult = File(result_hash + ".jpg", "image/jpeg") });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}

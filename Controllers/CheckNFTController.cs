using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NFT_Cert_Issue_Check.NFTBusiness.interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NFT_Cert_Issue_Check.Controllers
{
    public class CheckNFTController : ControllerBase
    {
        private readonly ILogger<CheckNFTController> _logger;
        private readonly ICheckNFT _checkNFT;

        public CheckNFTController(ICheckNFT checkNFT)
        {
            _checkNFT = checkNFT;
        }

        [HttpPost(nameof(CheckNFT))]
        public IActionResult CheckNFT([Required] string TxHash)
        {
            List<string> result;
            result = _checkNFT.CheckByTransactionHash(TxHash);

            return Ok(new { result });

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lightning;
using Microsoft.AspNetCore.Mvc;

namespace BitarAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LightningController : ControllerBase
    {
        private readonly LightningClient _lightning;

        public LightningController(LightningClient lightningClient)
        {
            _lightning = lightningClient;
        }

        // GET lightning
        [HttpGet]
        public ActionResult Get()
        {
            return Ok(_lightning.SocketSendReceive("getinfo"));
        }

        // GET lightning/listpeers
        [HttpGet("listpeers")]
        public ActionResult<string> ListPeers()
        {
            return _lightning.SocketSendReceive("listpeers");
        }

        // GET lightning/listpeers
        [HttpGet("listinvoices")]
        public ActionResult<string> ListInvoices()
        {
            return _lightning.SocketSendReceive("listinvoices");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lightning;
using Lightning.Models;
using Microsoft.AspNetCore.Mvc;

namespace BitarAPI.Controllers
{
    [Route("[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class LightningController : ControllerBase
    {
        private readonly LightningClient _lightning;

        public LightningController(LightningClient lightning)
        {
            _lightning = lightning;
        }

        // GET api.bitar.is/lightning
        [HttpGet]
        public ActionResult<Info> Get()
        {
            if (!_lightning.GetInfo(out var info))
            {
                return NotFound();
            }
            return info;
        }

        // GET api.bitar.is/lightning/listpeers
        [HttpGet("listpeers")]
        public ActionResult<List<Peer>> ListPeers()
        {
            if (!_lightning.ListPeers(out var peers))
            {
                return NotFound();
            }
            return peers;
        }

        // GET api.bitar.is/lightning/listnodes
        [HttpGet("listnodes")]
        public ActionResult<List<Node>> ListNodes()
        {
            if (!_lightning.ListNodes(out var nodes))
            {
                return NotFound();
            }
            return nodes;
        }

        // GET api.bitar.is/lightning/listinvoices
        [HttpGet("listinvoices")]
        public ActionResult<List<Invoice>> ListInvoices()
        {
            if (!_lightning.ListInvoices(out var invoices))
            {
                return NotFound();
            }
            return invoices;
        }

        // Post api.bitar.is/lightning/createinvoice
        [HttpPost("createinvoice")]
        public ActionResult<Invoice> CreateInvoice([FromBody] Invoice inv)
        {
            if (!_lightning.CreateInvoice(inv, out var invoice))
            {
                return NotFound();
            }
            return invoice;
        }
    }
}
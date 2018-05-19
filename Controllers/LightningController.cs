using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lightning;
using Lightning.Models;
using Microsoft.AspNetCore.Mvc;

namespace BitarAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LightningController : ControllerBase
    {
        private readonly LightningClient _lightning;

        public LightningController()
        {
            _lightning = new LightningClient();
        }

        // GET lightning
        [HttpGet]
        public ActionResult<Info> Get()
        {
            if (!_lightning.GetInfo(out var info))
            {
                return NotFound();
            }
            return info;
        }

        // GET lightning/listpeers
        [HttpGet("listpeers")]
        public ActionResult<List<Peer>> ListPeers()
        {
            if (!_lightning.ListPeers(out var peers))
            {
                return NotFound();
            }
            return peers;
        }

        // GET lightning/listnodes
        [HttpGet("listnodes")]
        public ActionResult<List<Node>> ListNodes()
        {
            if (!_lightning.ListNodes(out var nodes))
            {
                return NotFound();
            }
            return nodes;
        }

        // GET lightning/listinvoices
        [HttpGet("listinvoices")]
        public ActionResult<List<Invoice>> ListInvoices()
        {
            if (!_lightning.ListInvoices(out var invoices))
            {
                return NotFound();
            }
            return invoices;
        }

        // GET lightning/createinvoice
        [HttpGet("createinvoice")]
        public ActionResult<Invoice> CreateInvoice()
        {
            if (!_lightning.CreateInvoice(25000, "guu", "lala", out var invoice))
            {
                return NotFound();
            }
            return invoice;
        }
    }
}
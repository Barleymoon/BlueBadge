using _14_GereralStore.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace _14_GereralStore.Controllers
{
    public class TransactionController : ApiController
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();
        [HttpPost]
        public async Task<IHttpActionResult> PurchaseProduct(Transaction transaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Transaction.Add(transaction);

            var product = await _context.Products.FindAsync(transaction.ProductId);

            if (product.Quantity < transaction.Quantity)
            {
                return BadRequest("Not enough stock");
            }
            product.Quantity -= transaction.Quantity;

            await _context.SaveChangesAsync();
            return Ok(transaction);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAllPurchases()
        {
            var transactions = await _context.Transaction.ToListAsync();
            return Ok(transactions);
        }
    }
}

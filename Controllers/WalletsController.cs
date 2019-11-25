using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using E_Wallet.Models;

namespace E_Wallet.Controllers
{
    public class WalletsController : Controller
    {
        private Model1 db = new Model1();

        // GET: Wallets
        public async Task<ActionResult> Index()
        {
            return View(await db.Wallets.ToListAsync());
        }

               
    }
}

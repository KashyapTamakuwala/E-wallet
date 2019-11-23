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
using Microsoft.AspNet.Identity;
using System.IO;
using ZXing;
using System.Drawing;
using System.Drawing.Imaging;

namespace E_Wallet.Controllers
{
    public class UserController : Controller
    {

        private Model1 db = new Model1();

        // GET: User
        //[Authorize(Roles= "Individual, Organization")]
        public ActionResult Index()
        {
            return View();
        }

        

        // GET: User/Pay
        public ActionResult Pay()
        {
            return View();
        }

        // POST: User/Pay
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Pay([Bind(Include = "ID,Email,Transaction_Amount,TO_Email,SID")] Transaction transaction)
        {
            
            if (ModelState.IsValid)
            {
                transaction.Email = (String)Session["mail"];
                int x = transaction.Transaction_Amount;
                String user = transaction.Email;
                String user1 = transaction.TO_Email;
                Wallet w = db.Wallets.Where(s => s.Email == user).FirstOrDefault<Wallet>();
                Wallet w2 = db.Wallets.Where(s => s.Email == user1).FirstOrDefault<Wallet>();
                if (w.Balance >= x)
                {
                    w.Balance -= x; // updating the balance
                    w2.Balance += x; // updating the balance
                    db.Transactions.Add(transaction);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Error = "Not Enough Balance";
                    return View(transaction);
                }
                
            }
            ViewBag.Error = "Not found email ";
            return View(transaction);
        }


        // GET: User/Load
        public ActionResult Load()
        {
            return View();
        }

        // POST: User/Load
        [HttpPost]
        public ActionResult Load(FormCollection collection)
        {
            try
            {
                // ammount to be loaded;
                String ammount = Request.Form["Ammount"].ToString();
                int am = Convert.ToInt32(ammount);
                var mail = (String)Session["mail"];
                var wallet = db.Wallets.Where(w => w.Email == mail).FirstOrDefault<Wallet>();
                var Bank = db.Banks.Where(B => B.Account_Number == wallet.Account_Number).FirstOrDefault<Bank>();

                if (Bank.Balance >= am)
                {
                    wallet.Balance += am;
                    Bank.Balance -= am;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }
                
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Link
        public ActionResult Link()
        {
            List<SelectListItem> BList = new List<SelectListItem>()
            {
                new SelectListItem { Text = "Alpha Bank", Value = "Alpha Bank" },
                new SelectListItem { Text = "Beta Bank", Value = "Beta Bank" },
                new SelectListItem { Text = "Gamma Bank", Value = "Gamma Bank" },
                new SelectListItem { Text = "Theta Bank", Value = "Theta Bank" },
            };
            ViewBag.Bank = BList;
            return View();
        }

        // POST: User/Link
        [HttpPost]
        public ActionResult Link(FormCollection collection)
        {
            try
            {
                List<SelectListItem> BList = new List<SelectListItem>()
                {
                    new SelectListItem { Text = "Alpha Bank", Value = "Alpha Bank" },
                    new SelectListItem { Text = "Beta Bank", Value = "Beta Bank" },
                    new SelectListItem { Text = "Gamma Bank", Value = "Gamma Bank" },
                    new SelectListItem { Text = "Theta Bank", Value = "Theta Bank" },
                };
                ViewBag.Bank = BList;
                String num = Request.Form["Phone-Number"].ToString();
                var obj = db.Banks.Where(n => n.Phone_Number == num).FirstOrDefault<Bank>();
                if (obj != null)
                {
                    String mail = (String)Session["mail"];
                    var wallet = db.Wallets.Where(w => w.Email == mail).FirstOrDefault<Wallet>();
                    wallet.Account_Number = obj.Account_Number;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Qrcodegen()
        {

            try
            {
                String mail = (String)Session["mail"].ToString();
                String path = GenerateQRCode(mail);
                ViewBag.Qr = path;
            }
            catch (Exception ex)
            {
            }
            return View();
        }

        //code to generate qrcode
        private string GenerateQRCode(string qrcodeText)


        {
            string folderPath = "~/Images/";
            string imagePath = "~/Images/QrCode.jpg";
            // create new Directory if not exist
            if (!Directory.Exists(Server.MapPath(folderPath)))
            {
                Directory.CreateDirectory(Server.MapPath(folderPath));
            }

            var barcodeWriter = new BarcodeWriter();
            barcodeWriter.Format = BarcodeFormat.QR_CODE;
            var result = barcodeWriter.Write(qrcodeText);

            string barcodePath = Server.MapPath(imagePath);
            var barcodeBitmap = new Bitmap(result);
            using (MemoryStream memory = new MemoryStream())
            {
                using (FileStream fs = new FileStream(barcodePath, FileMode.Create, FileAccess.ReadWrite))
                {
                    barcodeBitmap.Save(memory, ImageFormat.Jpeg);
                    byte[] bytes = memory.ToArray();
                    fs.Write(bytes, 0, bytes.Length);
                }
            }
            return imagePath;
        }

    }
}

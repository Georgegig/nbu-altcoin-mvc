using AltcoinPortfolio.DataModels;
using AltcoinPortfolio.Entities;
using AltcoinPortfolio.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AltcoinPortfolio.Controllers
{
    [Authorize]
    public class PortfolioController : Controller
    {
        private PortfolioContext db = new PortfolioContext();
        //private Timer timer = new Timer();
        // GET: Portfolio
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("GetUserPortfolio")]
        public JsonResult GetUserPortfolio(string email)
        {
            List<Coin> result = new List<Coin>();

            Guid portfolioId = this.db.Portfolios.Where(p => p.User.Email == email).Select(p => p.Id).FirstOrDefault();
            if (portfolioId != Guid.Empty)
            {
                result = this.db.Coins.Where(c => c.PortfolioId == portfolioId).ToList();
            }

            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        //public JsonResult AddCoin(EmailCoinDataModel coin)
        //{

        //    try
        //    {
        //        Guid portfolioId = this.db.Portfolios.Where(p => p.User.Email == coin.UserEmail).Select(p => p.Id).FirstOrDefault(); ;

        //        if (this.db.Coins.Where(c => c.PortfolioId == portfolioId && c.)
        //        {
        //            using (SqlConnection connection = new SqlConnection(_connectionString))
        //            {
        //                connection.Open();
        //                SqlCommand insertCommand = new SqlCommand(UpdateCoinAmountQuery, connection);

        //                insertCommand.Parameters.AddWithValue("@CoinId", coin.Id);
        //                insertCommand.Parameters.AddWithValue("@PortfolioId", portfolioId);
        //                insertCommand.Parameters.AddWithValue("@Amount", coin.Amount);

        //                insertCommand.ExecuteNonQuery();
        //            }
        //        }
        //        else
        //        {
        //            using (SqlConnection connection = new SqlConnection(_connectionString))
        //            {
        //                connection.Open();
        //                SqlCommand insertCommand = new SqlCommand(InsertCoinQuery, connection);

        //                insertCommand.Parameters.AddWithValue("@SysId", Guid.NewGuid());
        //                insertCommand.Parameters.AddWithValue("@Id", coin.Id);
        //                insertCommand.Parameters.AddWithValue("@PortfolioId", portfolioId);
        //                insertCommand.Parameters.AddWithValue("@Name", coin.Name);
        //                insertCommand.Parameters.AddWithValue("@Symbol", coin.Symbol);
        //                insertCommand.Parameters.AddWithValue("@Rank", coin.Rank);
        //                insertCommand.Parameters.AddWithValue("@Price_USD", coin.Price_USD);
        //                insertCommand.Parameters.AddWithValue("@Amount", coin.Amount);

        //                insertCommand.ExecuteNonQuery();
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        return Json(new { success = false });
        //    }

        //    return Json(new { success = true });
        //}

        //public JsonResult DeletePortfolio(string email)
        //{
        //    try
        //    {
        //        this._portfolioManager.DeletePortfolio(email);
        //        NotificationHub.Reload();
        //    }
        //    catch (Exception e)
        //    {
        //        return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        //    }
        //    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        //}

        //public JsonResult RefreshPortfolio(string email)
        //{
        //    try
        //    {
        //        this._portfolioManager.RefreshPortfolio(email);
        //        this.ReloadClientsPortfolio();
        //    }
        //    catch (Exception e)
        //    {
        //        return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        //    }
        //    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        //}

        //private void ReloadClientsPortfolio()
        //{
        //    this.timer.Interval = 3000;
        //    this.timer.Enabled = true;
        //    this.timer.Elapsed += new ElapsedEventHandler(OnTimedReloadPortfolio);
        //    this.timer.Start();
        //}
        //private void OnTimedReloadPortfolio(Object sender, ElapsedEventArgs e)
        //{
        //    NotificationHub.Reload();
        //    this.timer.Stop();
        //}

        //public ActionResult DownloadPortfolio(string email)
        //{
        //    byte[] fileBytes = GetPortfolio(email);
        //    return File(
        //        fileBytes,
        //         "application/x-msdownload", string.Format("User{0}_Portfolio.txt", email));
        //}

        //private byte[] GetPortfolio(string email)
        //{
        //    try
        //    {
        //        return _portfolioManager.GetPortfolioFileContents(email);
        //    }
        //    catch (Exception e)
        //    {
        //        return null;
        //    }
        //}
    }
}
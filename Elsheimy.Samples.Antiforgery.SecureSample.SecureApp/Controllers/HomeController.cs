using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elsheimy.Samples.Antiforgery.SecureSample.SecureApp.Controllers
{
  public class HomeController : Controller
  {
    private AccountController AccountController { get; set; }
    private ApiController ApiController { get; set; }



    public HomeController(AccountController accountController, ApiController apiController)
    {
      this.AccountController = accountController;
      this.ApiController = apiController;
    }

    public IActionResult Index()
    {
      this.ViewBag.Balance = this.ApiController.Balance();
      this.ViewBag.IsAuthenticated = this.AccountController.IsAuthenticated();
      return View();
    }


    [HttpPost]
    public async Task<IActionResult> Debit(int amount)
    {
      await this.ApiController.Debit(amount);

      return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Credit(int amount)
    {
      await this.ApiController.Credit(amount);

      return RedirectToAction(nameof(Index));
    }


  }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Elsheimy.Samples.Antiforgery.SecureSample.AngularApp.Controllers
{
  public class ApiController : Controller
  {
    private IServiceProvider Services { get; set; }
    private Microsoft.AspNetCore.Antiforgery.IAntiforgery Csrf { get { return Services.GetRequiredService<Microsoft.AspNetCore.Antiforgery.IAntiforgery>(); } }
    private HttpContext Context { get { return Services.GetRequiredService<IHttpContextAccessor>().HttpContext; } }

    public static int CurrentBalance = 100;

    public ApiController(IServiceProvider services)
    {
      this.Services = services;
    }

    private async Task<bool> ValidateAntiForgeryToken()
    {
      try
      {
        await Csrf.ValidateRequestAsync(this.Context);
        return true;
      }
      catch (Microsoft.AspNetCore.Antiforgery.AntiforgeryValidationException)
      {
        return false;
      }
    }


    [HttpGet]
    public int Balance()
    {
      return CurrentBalance;
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult<int>> Debit(int amount)
    {
      // the manual way
      //if (false == await ValidateAntiForgeryToken())
      //  return BadRequest();

      CurrentBalance -= amount;
      return Ok(Balance());
    }

    [HttpPost]
    public async Task<ActionResult<int>> Credit(int amount)
    {
      if (false == await ValidateAntiForgeryToken())
        return BadRequest();

      CurrentBalance += amount;
      return Ok(Balance());
    }
  }
}

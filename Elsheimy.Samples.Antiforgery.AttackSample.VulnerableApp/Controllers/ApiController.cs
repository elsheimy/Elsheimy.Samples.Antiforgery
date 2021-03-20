using Microsoft.AspNetCore.Mvc;

namespace Elsheimy.Samples.Antiforgery.SecureSample.AutoSecureApp
{
  public class ApiController : Controller
  {
    public static int CurrentBalance = 100;


    [HttpGet]
    public int Balance()
    {
      return CurrentBalance;
    }

    [HttpPost]
    public int Debit(int amount)
    {
      CurrentBalance -= amount;
      return Balance();
    }

    [HttpPost]
    public int Credit(int amount)
    {
      CurrentBalance += amount;
      return Balance();
    }
  }
}

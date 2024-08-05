using System.Diagnostics;
using Library.Models;
using Microsoft.AspNetCore.Http;

namespace Library.Services
{
    public class HomeService : IHomeService
    {
        public ErrorViewModel GetErrorViewModel(HttpContext httpContext)
        {
            return new ErrorViewModel { RequestId = Activity.Current?.Id ?? httpContext.TraceIdentifier };
        }
    }
}
using Library.Models;

namespace Library.Services
{
    public interface IHomeService
    {
        ErrorViewModel GetErrorViewModel(HttpContext httpContext);
    }

}
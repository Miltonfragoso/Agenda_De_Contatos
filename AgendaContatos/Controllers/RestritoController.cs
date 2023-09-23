using AgendaContatos.Filters;
using Microsoft.AspNetCore.Mvc;

namespace AgendaContatos.Controllers
{
    [PaginaParaUsuarioLogado]
    public class RestritoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

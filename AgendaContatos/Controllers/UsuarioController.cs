using AgendaContatos.Filters;
using AgendaContatos.Models;
using AgendaContatos.Repositorio;
using ControleDeContatos.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AgendaContatos.Controllers
{
    [PaginaRestritaSomenteAdmin]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepositorio usuarioRepositorio;
        private readonly IContatoRepositorio contatoRepositorio;

        public UsuarioController(IUsuarioRepositorio usuarioRepositorio,
                                 IContatoRepositorio contatoRepositorio)
        {
            this.usuarioRepositorio = usuarioRepositorio;
            this.contatoRepositorio = contatoRepositorio;
        }
        public IActionResult Index()
        {
            List<UsuarioModel> Usuarios = usuarioRepositorio.BuscarTodos();
            return View(Usuarios);
        }

        [HttpGet]
        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Criar(UsuarioModel usuario)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    usuarioRepositorio.Adicionar(usuario);
                    TempData["MensagemSucesso"] = "Usuário cadastrado comsucesso!";
                    return RedirectToAction("Index");
                }


                return View(usuario);
            }

            catch (System.Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos cadastrar seu Usuário, tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }


        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            UsuarioModel usuario = usuarioRepositorio.ListarPorId(id);
            return View(usuario);
        }

        [HttpPost]
        public IActionResult Editar(UsuarioSemSenhaModel usuarioSemSenhaModel)
        {
            try
            {
                UsuarioModel usuario = null;
                if (ModelState.IsValid)
                {
                    usuario = new UsuarioModel()
                    {
                        Id = usuarioSemSenhaModel.Id,
                        Nome = usuarioSemSenhaModel.Nome,
                        Login = usuarioSemSenhaModel.Login,
                        Email = usuarioSemSenhaModel.Email,
                        Perfil = usuarioSemSenhaModel.Perfil,
                    };
                    usuario = usuarioRepositorio.Atualizar(usuario);

                    TempData["MensagemSucesso"] = "Usuário alterado com comsucesso!";
                    return RedirectToAction("Index");
                }

                return View("Editar", usuario);
            }
            catch (System.Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos atualizar seu Usuário, tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }

        }

        public IActionResult ApagarConfirmacao(int id)
        {
            UsuarioModel usuario = usuarioRepositorio.ListarPorId(id);
            return View(usuario);

        }

        public IActionResult Apagar(int id)
        {
            try
            {
                bool apagado = usuarioRepositorio.Apagar(id);
                if (apagado)
                {
                    TempData["MensagemSucesso"] = "Usuário apagado com comsucesso!";
                }
                else
                {
                    TempData["MensagemErro"] = "Ops, não conseguimos apagar seu Usuário!";
                }
                return RedirectToAction("Index");
            }
            catch (System.Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos apagar seu Usuário, mais detalhes do erro: {erro.Message}";
                return RedirectToAction("Index");
            }

        }

        [HttpGet]
        public IActionResult ListarContatosPorUsuarioId(int id)
        {
            List<ContatoModel> contatos = contatoRepositorio.BuscarTodos(id);
            return PartialView("_ContatosUsuarios", contatos);
        }

    }
}

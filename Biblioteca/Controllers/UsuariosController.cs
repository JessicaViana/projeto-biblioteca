using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Biblioteca.Models;
using Microsoft.AspNetCore.Http;

namespace Biblioteca.Controllers
{
    public class UsuariosController : Controller
    {              
        public IActionResult RegistrarUsuarios()
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.verificarSeUsuarioEAdmin(this);

            return View();
        }

        [HttpPost]
        public IActionResult RegistrarUsuarios(Usuario novoUser)
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.verificarSeUsuarioEAdmin(this);

            novoUser.Senha = Criptografo.TextoCriptografado(novoUser.Senha);

            new UsuarioService().incluirUsuario(novoUser);
            
            return RedirectToAction("cadastroRealizado");
        }

        public IActionResult ListaDeUsuarios()
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.verificarSeUsuarioEAdmin(this);

            return View(new UsuarioService().ListarTodos());
        }

        public IActionResult editarUsuario(int id)
        {
            Usuario u = new UsuarioService().Listar(id);

            return View(u);
        }

        [HttpPost]
        public IActionResult editarUsuario(Usuario userEditado)
        {
            UsuarioService u = new UsuarioService();
            u.editarUsuario(userEditado);

            return RedirectToAction("ListaDeUsuarios");

        }

         public IActionResult excluirUsuario(int id)
        {
            return View(new UsuarioService().Listar(id));
        }

        [HttpPost]
        public IActionResult excluirUsuario(string decisao, int id)
        {
            if(decisao == "EXCLUIR")
            {
                ViewData["Mensagem"] = "Exclusão do usuário" +new UsuarioService().Listar(id).Nome+" realizada com sucesso";
                new UsuarioService().excluirUsuario(id);
                return View("ListaDeUsuarios",new UsuarioService().ListarTodos());
            }else
            {
                ViewData["Mensagem"] = "Exclusão Cancelada";
                return View("ListaDeUsuarios",new UsuarioService().ListarTodos());
            }
        }          

        public IActionResult cadastroRealizado()
        {
            Autenticacao.CheckLogin(this);            
            Autenticacao.verificarSeUsuarioEAdmin(this);


            return View();
        }
        public IActionResult NeedAdmin()
        {
            Autenticacao.CheckLogin(this);
            return View();
        }

        public IActionResult Sair()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index","Home");
        }

        
    }
}
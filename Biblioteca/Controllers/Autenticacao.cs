using Biblioteca.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;



namespace Biblioteca.Controllers
{
    public class Autenticacao
    {
        public static void CheckLogin(Controller controller)
        {   
            if(string.IsNullOrEmpty(controller.HttpContext.Session.GetString("login")))
            {
                controller.Request.HttpContext.Response.Redirect("/Home/Login");
            }
        }

        public static bool verificaLoginSenha(string login, string senha, Controller controller)
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                verificaSeUsuarioAdminExiste(bc);

                senha = Criptografo.TextoCriptografado(senha);

                IQueryable<Usuario> UsuarioEncontrado = bc.Usuarios.Where( u => u.Login == login && u.Senha == senha);
                List<Usuario> ListaUsuarioEncontrado = UsuarioEncontrado.ToList();
                if( UsuarioEncontrado.Count() == 0){
                    return false;
                }
                else
                {
                    controller.HttpContext.Session.SetString("login", ListaUsuarioEncontrado[0].Login);
                    controller.HttpContext.Session.SetString("Nome", ListaUsuarioEncontrado[0].Nome);
                    controller.HttpContext.Session.SetInt32("Tipo", ListaUsuarioEncontrado[0].Tipo);
                    return true;
                }
            }
        }

        public static void verificaSeUsuarioAdminExiste(BibliotecaContext bc)
        {
            IQueryable<Usuario> userEncontrado = bc.Usuarios.Where(u => u.Login =="admin");
            
            //Se não existir será criado o usuário admin padrão
            if(userEncontrado.ToList().Count() ==0)
            {
                Usuario admin = new Usuario();
                admin.Login = "admin";
                admin.Nome = "Administrador";
                admin.Senha = Criptografo.TextoCriptografado("123");
                admin.Tipo = Usuario.ADMIN;

                bc.Usuarios.Add(admin);
                bc.SaveChanges();
            }            
        }

        public static void verificarSeUsuarioEAdmin(Controller controller){
            if(!(controller.HttpContext.Session.GetInt32("Tipo")==Usuario.ADMIN))
            {
                controller.Request.HttpContext.Response.Redirect("/Usuarios/NeedAdmin");
            }
        }
    }
} 
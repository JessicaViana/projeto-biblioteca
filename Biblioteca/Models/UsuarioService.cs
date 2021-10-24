using System.Collections.Generic;
using System.Linq;

namespace Biblioteca.Models
{
    public class UsuarioService
    {
        public void AtualizarUser(Livro l)
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                Livro livro = bc.Livros.Find(l.Id);
                livro.Autor = l.Autor;
                livro.Titulo = l.Titulo;
                livro.Ano = l.Ano;

                bc.SaveChanges();
            }
        }

        public List<Usuario> ListarTodos(){

            using(BibliotecaContext bc = new BibliotecaContext()){

                List<Usuario> listaDeUsers = bc.Usuarios.ToList();
                return listaDeUsers;
            }
        }

        public Usuario Listar(int id){
            using(BibliotecaContext bc = new BibliotecaContext()){

                Usuario user = bc.Usuarios.Find(id);
                return user;
            }

        }

        public void incluirUsuario(Usuario novoUser){

            using(BibliotecaContext bc = new BibliotecaContext()){

                bc.Usuarios.Add(novoUser);
                bc.SaveChanges();
            }

        }

        public void editarUsuario(Usuario userEditado){

             using(BibliotecaContext bc = new BibliotecaContext()){

                Usuario user = bc.Usuarios.Find(userEditado.Id);

                user.Login = userEditado.Login;
                user.Nome = userEditado.Nome;
                user.Senha = userEditado.Senha;
                user.Tipo = userEditado.Tipo;

                bc.SaveChanges();
            }

        }
        public void excluirUsuario(int id){

            using(BibliotecaContext bc = new BibliotecaContext()){
                bc.Usuarios.Remove(bc.Usuarios.Find(id));
                bc.SaveChanges();
            }

        }
    }
}
using AgendaContatos.Data;
using AgendaContatos.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AgendaContatos.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly BancoContext bancoContext;

        public UsuarioRepositorio(BancoContext bancoContext)
        {
            this.bancoContext = bancoContext;
        }

        public UsuarioModel BuscarPorLogin(string login)
        {
            return bancoContext.Usuarios.FirstOrDefault(x => x.Login.ToUpper() == login.ToUpper());
        }

        public UsuarioModel BuscarPorEmailLogin(string email, string login)
        {
            return bancoContext.Usuarios.FirstOrDefault(x => x.Email.ToUpper() == email.ToUpper() && x.Login.ToUpper() == login.ToUpper());
        }

        public UsuarioModel ListarPorId(int id)
        {
            return bancoContext.Usuarios.FirstOrDefault(x => x.Id == id);
        }

        public List<UsuarioModel> BuscarTodos()
        {
            return bancoContext.Usuarios
                               .Include(x => x.Contatos)
                               .ToList();
        }

        public UsuarioModel Adicionar(UsuarioModel usuario)
        {
            //  Gravar no banco 
            usuario.DataCadastro = DateTime.Now;
            usuario.SetSenhaHash();
            bancoContext.Usuarios.Add(usuario);
            bancoContext.SaveChanges();
            return usuario;
        }

        public UsuarioModel Atualizar(UsuarioModel usuario)
        {
            UsuarioModel usuarioDB = ListarPorId(usuario.Id);

            if (usuarioDB == null) throw new System.Exception("Houve  um erro na atualização do usuário ");

            usuarioDB.Nome    = usuario.Nome;
            usuarioDB.Login    = usuario.Login;
            usuarioDB.Email   = usuario.Email;
            usuarioDB.Perfil   = usuario.Perfil;
            usuarioDB.DataAtualização   = DateTime.Now;
            bancoContext.Usuarios.Update(usuarioDB);
            bancoContext.SaveChanges();

            return usuarioDB;
        }

        public UsuarioModel AlterarSenha(AlterarSenhaModel alterarSenhaModel)
        {
            UsuarioModel usuarioDB = ListarPorId(alterarSenhaModel.Id);

            if (usuarioDB == null) throw new Exception("Houve um erro na atualização da senha, usuário não encontrado!");

            if (!usuarioDB.SenhaValida(alterarSenhaModel.SenhaAtual)) throw new Exception("Senha atual não confere!");

            if (usuarioDB.SenhaValida(alterarSenhaModel.NovaSenha)) throw new Exception("Nova senha deve ser diferente da senha atual!");

            usuarioDB.DataAtualização = DateTime.Now;
            usuarioDB.SetNovaSenha(alterarSenhaModel.NovaSenha);

            bancoContext.Usuarios.Update(usuarioDB);
            bancoContext.SaveChanges();

            return usuarioDB;
        }

        public bool Apagar(int id)
        {
            UsuarioModel usuarioDB = ListarPorId(id);

            if (usuarioDB == null) throw new System.Exception("Houve  um erro na deleção do usuário ");
            bancoContext.Usuarios.Remove(usuarioDB);
            bancoContext.SaveChanges();

            return true;
        }


    }
}

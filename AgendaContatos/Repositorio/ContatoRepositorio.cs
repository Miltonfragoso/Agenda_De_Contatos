using AgendaContatos.Data;
using AgendaContatos.Models;
using System.Collections.Generic;
using System.Linq;

namespace AgendaContatos.Repositorio
{
    public class ContatoRepositorio : IContatoRepositorio
    {
        private readonly BancoContext bancoContext;

        public ContatoRepositorio(BancoContext bancoContext)
        {
            this.bancoContext = bancoContext;
        }

        public ContatoModel ListarPorId(int id)
        {
            return bancoContext.Contatos.FirstOrDefault(x => x.Id == id);
        }

        public List<ContatoModel> BuscarTodos(int UsuarioId)
        {
            return bancoContext.Contatos.Where(x => x.UsuarioId == UsuarioId).ToList();
        }

        public ContatoModel Adicionar(ContatoModel contato)
        {
            //  Gravar no banco 
            bancoContext.Contatos.Add(contato);
            bancoContext.SaveChanges();
            return contato;
        }

        public ContatoModel Atualizar(ContatoModel contato)
        {
           ContatoModel contatoDB = ListarPorId(contato.Id);

            if (contatoDB == null ) throw new System.Exception("Houve  um erro na atualização do contato ");

            contatoDB.Nome    = contato.Nome;
            contatoDB.Celular = contato.Celular;
            contatoDB.Email   = contato.Email;

            bancoContext.Contatos.Update(contatoDB);
            bancoContext.SaveChanges();

            return contatoDB;
        }

        public bool Apagar(int id)
        {
            ContatoModel contatoDB = ListarPorId(id);

            if (contatoDB == null) throw new System.Exception("Houve  um erro na deleção do contato ");
            bancoContext.Contatos.Remove(contatoDB);
            bancoContext.SaveChanges();

            return true;
        }
    }
}

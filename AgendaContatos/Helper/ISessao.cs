using AgendaContatos.Models;

namespace AgendaContatos.Helper
{
    public interface ISessao
    {
        void CriarSessaoSoUsuario(UsuarioModel usuarioModel);

        void RemoverSessaoUsuario();

        UsuarioModel BuscarSessaoDoUsuario();
    }
}

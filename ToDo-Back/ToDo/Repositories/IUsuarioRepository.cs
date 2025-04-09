using ToDo.Models;

namespace ToDo.Repositories
{
    public interface IUsuarioRepository
    {
        public Task<Usuario?> Get(long id);

        public Task<int> Create(Usuario usuario);
        public Task<Usuario> Login(Usuario usuario);
        public Task<Usuario> GetByLogin(string login);


    }
}
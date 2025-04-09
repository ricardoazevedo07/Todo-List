using ToDo.DB;
using ToDo.Models;

namespace ToDo.Repositories
{
    public class UsuarioRepository:IUsuarioRepository
    {
        private ToDoContext Context;
        public UsuarioRepository(ToDoContext context) {
        
        this.Context = context;
        
        }

        public async Task<Usuario?> Get(long id) { 
            return await Context.Usuario.FindAsync(id);
        }
        public async Task<int> Create(Usuario usuario) {
            Context.Usuario.Add(usuario);
            return await Context.SaveChangesAsync();
        }

        public async Task<Usuario> Login(Usuario usuario)
        {

            var user = Context.Usuario.FirstOrDefault(u => u.Login == usuario.Login && u.Senha == usuario.Senha);
            if (user == null)
            {
                return new Usuario();
            }
            else
            {
                return user;
            }
        }

        public async Task<Usuario> GetByLogin(string login)
        {

            var user = Context.Usuario.FirstOrDefault(u => u.Login == login);
            if (user == null)
            {
                return new Usuario();
            }
            else
            {
                return user;
            }
        }
    }
}

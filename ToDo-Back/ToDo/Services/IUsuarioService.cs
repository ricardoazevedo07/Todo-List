using ToDo.Models;
using ToDo.ViewModel;

namespace ToDo.Services
{
    public interface IUsuarioService
    {
        Task<int?> Create(UsuarioViewModel usuario);
        UsuarioViewModel Get(long id);
        public Task<UsuarioViewModel> Login(UsuarioViewModel usuario);
        public Task<UsuarioViewModel> GetByLogin(string login);
    }
}
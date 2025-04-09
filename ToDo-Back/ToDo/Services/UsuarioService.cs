using AutoMapper;
using ToDo.Models;
using ToDo.Repositories;
using ToDo.ViewModel;

namespace ToDo.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository UsuarioRepository;
        private readonly IMapper Mapper;

        public UsuarioService(IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            this.UsuarioRepository = usuarioRepository;
            this.Mapper = mapper;
        }
        public UsuarioViewModel Get(long id)
        {
            var usuario =  this.UsuarioRepository.Get(id);
            var usuarioViewModel = Mapper.Map<UsuarioViewModel>(usuario.Result);
            return usuarioViewModel;
        }
        public async Task<int?> Create(UsuarioViewModel usuario)
        {
            var usuarioModel = Mapper.Map<Usuario>(usuario);
            var result = await this.UsuarioRepository.Create(usuarioModel);
            return  result != 0 ? result : null;
        }

        public async Task<UsuarioViewModel> Login(UsuarioViewModel usuario)
        {
            var usuarioModel = Mapper.Map<Usuario>(usuario);
            var result = await this.UsuarioRepository.Login(usuarioModel);
            if(result != null)
            {
                return Mapper.Map<UsuarioViewModel>(result);
            }
            return new UsuarioViewModel();
        }

        public async Task<UsuarioViewModel> GetByLogin(string login)
        {
            var user = await UsuarioRepository.GetByLogin(login);
            return Mapper.Map<UsuarioViewModel>(user ?? new Usuario());
        }
    }
}

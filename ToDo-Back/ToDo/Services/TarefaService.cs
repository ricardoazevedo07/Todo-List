using AutoMapper;
using ToDo.Models;
using ToDo.Repositories;
using ToDo.ViewModel;

namespace ToDo.Services
{
    public class TarefaService : ITarefaService
    {
        private readonly ITarefaRepository TarefaRepository;
        private readonly IUsuarioService UsuarioService;
        private readonly IMapper Mapper;

        public TarefaService(ITarefaRepository tarefaRepository,IUsuarioService usuarioService,IMapper mapper)
        {
            TarefaRepository = tarefaRepository;
            UsuarioService = usuarioService;
            Mapper = mapper;
        }

        public void Create(TarefaViewModel tarefa)
        {
            var tarefaModel = Mapper.Map<Tarefa>(tarefa); 
            TarefaRepository.Create(tarefaModel, tarefa.UsuarioId);
        }
        public TarefaViewModel? Get(long tarefaId)
        {
            var tarefaModel= TarefaRepository.Get(tarefaId);
            return Mapper.Map<TarefaViewModel>(tarefaModel.Result);
        }
        public List<TarefaViewModel> Get(long[] tarefasId)
        {
            var tarefaModel= TarefaRepository.Get(tarefasId);
            return Mapper.Map<List<TarefaViewModel>>(tarefaModel.Result);
        }
        public List<TarefaViewModel> GetByUsuario(long usuarioId, int page, int pageSize, out int total)
        {
            var tarefaModel = TarefaRepository.GetbyUsuario(usuarioId,  page,  pageSize);
            var tarefas = Mapper.Map<List<TarefaViewModel>>(tarefaModel.Result);
            total= TarefaRepository.GetTotalByUsuario(usuarioId).Result;
            return tarefas;
            
        }
        public void Update(TarefaViewModel tarefa)
        {
            var tarefaModel = Mapper.Map<Tarefa>(tarefa);
            TarefaRepository.Update(tarefaModel);
        }
        public void Delete(long tarefaId)
        {
            TarefaRepository.Delete(tarefaId);
        }
        public void Concluir(long tarefaId)
        {
            TarefaRepository.Concluir(tarefaId);
        }
    }
}

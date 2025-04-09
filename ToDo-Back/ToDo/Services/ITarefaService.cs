using ToDo.Models;
using ToDo.ViewModel;

namespace ToDo.Services
{
    public interface ITarefaService
    {
        void Create(TarefaViewModel tarefa);
        void Delete(long tarefaId);
        void Concluir(long tarefaId);
        TarefaViewModel? Get(long tarefaId);
        List<TarefaViewModel> Get(long[] tarefasId);
        List<TarefaViewModel> GetByUsuario(long usuarioId, int page, int pageSize, out int total);
        void Update(TarefaViewModel tarefa);
    }
}
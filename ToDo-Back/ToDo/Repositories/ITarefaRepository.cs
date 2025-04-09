using ToDo.Models;

namespace ToDo.Repositories
{
    public interface ITarefaRepository
    {
        public void Create(Tarefa tarefa, long usuarioId);
        public Task<Tarefa> Get(long tarefaId);
        public Task<List<Tarefa>> Get(long[] tarefasId);
        public Task<List<Tarefa>> GetbyUsuario(long usuarioId, int page, int pageSize);
        public  Task<int> GetTotalByUsuario(long usuarioId);
        public Task Update(Tarefa tarefa);
        public void Delete(long tarefaId);
        public void Concluir(long tarefaId);

    }
}
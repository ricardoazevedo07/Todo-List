using Microsoft.EntityFrameworkCore;
using ToDo.DB;
using ToDo.Models;

namespace ToDo.Repositories
{
    public class TarefaRepository:ITarefaRepository
    {
        private ToDoContext Context;
        public TarefaRepository(ToDoContext context)
        {

            this.Context = context;

        }

        public void Create(Tarefa tarefa, long usuarioId)
        {
            var usuario = Context.Usuario.Find(usuarioId);
            if (usuario == null)
            {
                throw new Exception("Usuario não encontrado");
            }

             usuario.Tarefas.Add(tarefa);
            Context.SaveChangesAsync();

        }

        public async Task<Tarefa> Get(long tarefaId)
        {
            var tarefa= await Context.Tarefa.AsNoTracking().FirstOrDefaultAsync(t=> t.TarefaId==tarefaId && t.DataExclusao==null);
            if (tarefa == null)
            {
                throw new Exception("Tarefa não encontrada");
            }
            return tarefa;
            
        }

        public async Task<List<Tarefa>> Get(long[] tarefasId)
        {
            var tarefas =  await Context.Tarefa.Where(t => tarefasId.Contains(t.TarefaId) && t.DataExclusao==null).AsNoTracking().ToListAsync();
            if (tarefas == null)
            {
                throw new Exception("Tarefas não encontradas");
            }
            return tarefas;

        }

        public async Task<List<Tarefa>> GetbyUsuario(long usuarioId, int page, int pageSize)
        {
            var usuario = await Context.Usuario
                                       .Include(u => u.Tarefas)
                                       .AsNoTracking()
                                       .FirstOrDefaultAsync(u => u.UsuarioId == usuarioId);
                                      

            if (usuario == null)
            {
                throw new Exception("Usuario não encontrado");
            }

            return usuario?.Tarefas
                           .Where(t => t.DataExclusao == null)
                           .OrderByDescending(t => t.Data) 
                           .Skip((page - 1) * pageSize)
                           .Take(pageSize)
                           .ToList() ?? new List<Tarefa>();

        }

        public async Task<int> GetTotalByUsuario(long usuarioId)
        {
            var usuario = await Context.Usuario
                                       .Include(u => u.Tarefas)
                                       .AsNoTracking()
                                       .FirstOrDefaultAsync(u => u.UsuarioId == usuarioId);


            if (usuario == null)
            {
                throw new Exception("Usuario não encontrado");
            }

            return usuario?.Tarefas
                           .Where(t => t.DataExclusao == null)
                           .OrderBy(t => t.Data)                           
                           .ToList().Count ?? 0;

        }

        public async Task Update(Tarefa tarefa)
        {
            var tarefaAntiga = Context.Tarefa.Find(tarefa.TarefaId);

            if (tarefaAntiga == null)
            {
                throw new Exception("Tarefa não encontrada");
            }
            
            tarefaAntiga.Descricao = tarefa.Descricao;
            tarefaAntiga.Titulo = tarefa.Titulo;
            await Context.SaveChangesAsync();
        }

        public async void Delete(long tarefaId)
        {
            var tarefa = Context.Tarefa.Find(tarefaId);
            tarefa.DataExclusao = DateTime.Now;
            Context.SaveChangesAsync();

        }
        public async void Concluir(long tarefaId)
        {
            var tarefa = Context.Tarefa.Find(tarefaId);
            tarefa.DataConclusao = DateTime.Now;
            Context.SaveChangesAsync();

        }

    }
}

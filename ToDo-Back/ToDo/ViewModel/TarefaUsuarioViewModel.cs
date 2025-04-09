

namespace ToDo.ViewModel
{
    public class TarefaUsuarioViewModel
    {
        public long TarefaUsuarioId { get; set; }
        public long TarefaId { get; set; }
        public long UsuarioId { get; set; }

        public TarefaViewModel Tarefa {get;set;}
        public UsuarioViewModel Usuario { get; set; }
    }
}

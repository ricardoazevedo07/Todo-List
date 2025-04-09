namespace ToDo.Models
{
    public class Tarefa
    {
        public long TarefaId { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
        public DateTime ?DataConclusao { get; set; }
        public DateTime ?DataExclusao { get; set; }

        public ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
    }
}

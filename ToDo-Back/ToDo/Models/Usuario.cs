namespace ToDo.Models
{
    public class Usuario
    {
        public long UsuarioId { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }

        public ICollection<Tarefa> Tarefas { get; set; } = new List<Tarefa>();
    }
}

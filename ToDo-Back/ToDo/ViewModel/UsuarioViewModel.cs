

namespace ToDo.ViewModel
{
    public class UsuarioViewModel
    {
        public long UsuarioId { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }

        ICollection<TarefaUsuarioViewModel> TarefaUsuarios { get; set; }
    }
}

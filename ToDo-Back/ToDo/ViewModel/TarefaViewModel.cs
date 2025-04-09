namespace ToDo.ViewModel
{
    public class TarefaViewModel
    {
        public long TarefaId { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
        public DateTime ? DataConclusao { get; set; }
        public DateTime ? DataExclusao { get; set; }
        public long UsuarioId { get; set; }
        //public ICollection<TarefaUsuarioViewModel> Usuarios { get; set; }
    }
}

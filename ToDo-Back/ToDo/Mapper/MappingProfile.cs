

using AutoMapper;
using ToDo.Models;
using ToDo.ViewModel;

namespace ToDo.Mapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<UsuarioViewModel, Usuario>().ReverseMap();
            CreateMap<TarefaViewModel, Tarefa>().ReverseMap();
            
        }
    }
}

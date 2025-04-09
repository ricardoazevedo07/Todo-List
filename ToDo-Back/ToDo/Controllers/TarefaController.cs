using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDo.Services;
using ToDo.ViewModel;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ToDo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefaController : ControllerBase
    {
        private readonly ITarefaService TarefaService;
        public TarefaController(ITarefaService tarefaService)
        {
            TarefaService = tarefaService;
        }
        //GET: api/<TarefaController>
        [Authorize]
        [HttpGet("GetByUsuario")]       
        public IActionResult GetByUsuario(long usuarioId, int page = 1, int pageSize = 5)
        {
            var result = TarefaService.GetByUsuario(usuarioId, page, pageSize, out int total);
            return Ok(new GetTarefaByUsuarioResponse
            {
                totalItems = total,
                currentPage = page,
                pageSize = pageSize,
                tarefas = result
            });
        }
        //GET api/<TarefaController>/5
        [HttpGet("{id}")]
        [Authorize]
        public IResult Get(long id)
        {
            var result = TarefaService.Get(id);
            return result != null ? Results.Ok(result) : Results.NotFound();
        }

        // POST api/<TarefaController>
        [HttpPost("Create")]
        [Authorize]
        public IResult Create([FromBody] TarefaViewModel novaTarefa)
        {
            TarefaService.Create(novaTarefa);
            return Results.Ok();
        }

        [HttpPost("Concluir")]
        [Authorize]
        public IResult Concluir([FromBody] long tarefaId)
        {
            TarefaService.Concluir(tarefaId);
            return Results.Ok();
        }

        // PUT api/<TarefaController>/5
        
        [HttpPut("{id}")]
        [Authorize]
        public void Update([FromBody] TarefaViewModel value)
        {
           TarefaService.Update(value);           
        }

        // DELETE api/<TarefaController>/5
        [HttpDelete("{id}")]
        [Authorize]
        public void Delete(long id)
        {
            TarefaService.Delete(id);
        }
    }

    public class GetTarefaByUsuarioResponse
    {
        public int totalItems { get; set; }
        public int currentPage { get; set; }
        public int pageSize { get; set; }
        public List<TarefaViewModel> tarefas { get; set; }
    }
}

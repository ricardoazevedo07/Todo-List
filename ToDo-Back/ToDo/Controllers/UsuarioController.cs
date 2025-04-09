using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ToDo.JWT;
using ToDo.Services;
using ToDo.ViewModel;


namespace ToDo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        IUsuarioService UsuarioService;
        JwtService _jwtService;
        private readonly PasswordHasher<string> _passwordHasher = new();
        public UsuarioController(IUsuarioService usuarioService, JwtService jwtService)
        {
            this.UsuarioService = usuarioService;
            this._jwtService = jwtService;
              
        }

        // GET api/<UsuarioController>/5
        [HttpGet("{id}")]
        [Authorize]
        public IResult Get(long id)
        {
            var usuario = this.UsuarioService.Get(id);

            if (usuario == null) {
                return Results.NotFound("Usuário não encontrado");
            }
            return Results.Ok(usuario);
        }

        // POST api/<UsuarioController>
        [HttpPost]
        public async Task<IResult> Create([FromBody] UsuarioViewModel usuario)
        {
            var usuarioJaExiste = await this.UsuarioService.GetByLogin(usuario.Login);
            if(!string.IsNullOrEmpty(usuarioJaExiste.Login))
            {
                return Results.Problem("Usuario já existe");
            }
            var hashedPassword = _passwordHasher.HashPassword(usuario.Login, usuario.Senha);
            usuario.Senha = hashedPassword;
            var result = await this.UsuarioService.Create(usuario);
            
            if (result != null) {
                var novoUsuario = await this.UsuarioService.GetByLogin(usuario.Login);
                var token = _jwtService.GenerateToken(usuario.Nome);
                return Results.Ok(new { token = token, UsuarioLogin = novoUsuario.Login, UsuarioId = novoUsuario.UsuarioId });
             }
            
           return Results.Problem("Não foi possível criar o novo usuário");
            
                
        }
        [HttpPost("Login")]
        public async Task<IResult> Login([FromBody] UsuarioViewModel usuario)
        {
            
            var user = await this.UsuarioService.GetByLogin(usuario.Login);

            if (user == null || user.Nome==null || user.Senha == null)
            {
                return Results.NotFound();
            }
            var result = _passwordHasher.VerifyHashedPassword(user.Login, user.Senha, usuario.Senha);

            if (result==PasswordVerificationResult.Success)
            {
                var token = _jwtService.GenerateToken(usuario.Nome);
                return Results.Ok(new { token=token,  UsuarioLogin=user.Login, UsuarioId=user.UsuarioId});
            }

            return Results.Unauthorized();
            
        }


    }
}

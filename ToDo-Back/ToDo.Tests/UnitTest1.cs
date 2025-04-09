//using Moq;
//using Xunit;
//using ToDo.Controllers;
//using ToDo.Services;
//using ToDo.ViewModel;
//using Microsoft.AspNetCore.Mvc;
//using System.Collections.Generic;

//namespace ToDo.Tests.Controllers
//{
//    public class TarefaControllerTests
//    {
//        private readonly Mock<ITarefaService> _mockTarefaService;
//        private readonly TarefaController _controller;

//        public TarefaControllerTests()
//        {
//            _mockTarefaService = new Mock<ITarefaService>();
//            _controller = new TarefaController(_mockTarefaService.Object);
//        }

//        [Fact]
//        public void GetByUsuario_DeveRetornarOkComTarefas()
//        {
//            // Arrange
//            long usuarioId = 1;
//            int page = 1;
//            int pageSize = 5;
//            int expectedTotalItems = 2; // Valor que ser� retornado via "out"
//            var tarefasMock = new List<TarefaViewModel>
//            {
//                new TarefaViewModel { Titulo = "Tarefa 1" },
//                new TarefaViewModel { Titulo = "Tarefa 2" }
//            };

//            // Configura��o correta do Mock com par�metro out
//            _mockTarefaService.Setup(s =>
//                s.GetByUsuario(usuarioId, page, pageSize, out expectedTotalItems))
//                .Returns(tarefasMock);

//            // Act
//            var result = _controller.GetByUsuario(usuarioId, page, pageSize) as OkObjectResult;

//            // Assert
//            Assert.NotNull(result);
//            Assert.Equal(200, result.StatusCode);

//            // Op��o 1: Usando dynamic com cast expl�cito (recomendado para objetos an�nimos)
//            dynamic resposta = result.Value;
//            Assert.Equal(expectedTotalItems, (int)resposta.totalItems); // Note o camelCase
//            Assert.Equal(page, (int)resposta.currentPage);
//            Assert.Equal(pageSize, (int)resposta.pageSize);
//            Assert.Equal(tarefasMock.Count, ((IEnumerable<TarefaViewModel>)resposta.tarefas).Count());

//            // Op��o 2: Usando a classe de resposta (alternativa mais type-safe)
//            // var resposta = Newtonsoft.Json.JsonConvert.DeserializeObject<GetByUsuarioResponse>(result.Value.ToString());
//            // Assert.Equal(expectedTotalItems, resposta.totalItems);
//            // Assert.Equal(page, resposta.currentPage);
//            // Assert.Equal(pageSize, resposta.pageSize);
//            // Assert.Equal(tarefasMock.Count, resposta.tarefas.Count);
//        }
//    }

//    // Extens�o para convers�o (mantida para compatibilidade)
//    public static class EnumerableExtensions
//    {
//        public static List<T> AsList<T>(this IEnumerable<T> source) => new List<T>(source);
//    }

//    // Classe para deserializa��o (opcional)
//    public class GetByUsuarioResponse
//    {
//        public int totalItems { get; set; }
//        public int currentPage { get; set; }
//        public int pageSize { get; set; }
//        public List<TarefaViewModel> tarefas { get; set; }
//    }
//}
using Xunit;
using Moq;
using ToDo.Controllers;
using ToDo.Services;
using ToDo.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http.HttpResults;
using Azure;

namespace ToDo.Tests.Controllers
{
    public class TarefaControllerTests
    {
        private readonly Mock<ITarefaService> _mockService;
        private readonly TarefaController _controller;

        public TarefaControllerTests()
        {
            _mockService = new Mock<ITarefaService>();
            _controller = new TarefaController(_mockService.Object);
        }

        [Fact]
        public void GetByUsuario_DeveRetornarOkComTarefas()
        {
            long usuarioId = 1;
            int page = 1;
            int pageSize = 5;
            int total = 2;

            var tarefasMock = new List<TarefaViewModel>
            {
                new TarefaViewModel { Titulo = "Teste 1" },
                new TarefaViewModel { Titulo = "Teste 2" }
            };

            _mockService.Setup(s => s.GetByUsuario(usuarioId, page, pageSize, out total))
                        .Returns(tarefasMock);

            var result = _controller.GetByUsuario(usuarioId, page, pageSize) as OkObjectResult;

            Assert.NotNull(result);
            GetTarefaByUsuarioResponse resposta = result.Value as GetTarefaByUsuarioResponse;
            Assert.Equal(2, resposta.totalItems);
            Assert.Equal(1, resposta.currentPage);
            Assert.Equal(5, resposta.pageSize);
            Assert.Equal(2, resposta.tarefas.Count);
           
        }

        [Fact]
        public void Get_DeveRetornarOkQuandoTarefaExiste()
        {
            long id = 1;
            var tarefa = new TarefaViewModel { Titulo = "Tarefa existente" };

            _mockService.Setup(s => s.Get(id)).Returns(tarefa);

            var result = _controller.Get(id);

            var okResult = Assert.IsType<Ok<TarefaViewModel>>(result);
            Assert.Equal("Tarefa existente", okResult.Value.Titulo);
        }

        [Fact]
        public void Get_DeveRetornarNotFoundQuandoTarefaNaoExiste()
        {
            long id = 99;

            _mockService.Setup(s => s.Get(id)).Returns((TarefaViewModel)null);

            var result = _controller.Get(id);

            Assert.IsType<NotFound>(result);
        }

        [Fact]
        public void Create_DeveChamarServicoComNovaTarefaERetornarOk()
        {
            var novaTarefa = new TarefaViewModel { Titulo = "Nova Tarefa" };

            var result = _controller.Create(novaTarefa);

            _mockService.Verify(s => s.Create(novaTarefa), Times.Once);
            Assert.IsType<Ok>(result);
        }

        [Fact]
        public void Concluir_DeveChamarServicoComIdCorretoERetornarOk()
        {
            long tarefaId = 123;

            var result = _controller.Concluir(tarefaId);

            _mockService.Verify(s => s.Concluir(tarefaId), Times.Once);
            Assert.IsType<Ok>(result);
        }

        [Fact]
        public void Update_DeveChamarServicoComTarefaAtualizada()
        {
            var tarefaAtualizada = new TarefaViewModel { TarefaId = 1, Titulo = "Atualizada" };

            _controller.Update(tarefaAtualizada);

            _mockService.Verify(s => s.Update(tarefaAtualizada), Times.Once);
        }

        [Fact]
        public void Delete_DeveChamarServicoComId()
        {
            long tarefaId = 10;

            _controller.Delete(tarefaId);

            _mockService.Verify(s => s.Delete(tarefaId), Times.Once);
        }
    }
    //internal class GetTarefaByUsuarioResponse
    //{
    //    public int totalItems { get; set; }
    //    public int currentPage { get; set; }
    //    public int pageSize { get; set; }
    //    public List<TarefaViewModel> tarefas { get; set; }
    //}
}

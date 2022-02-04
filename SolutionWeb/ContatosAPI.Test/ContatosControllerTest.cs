using ContatosAPI.Controllers;
using Domains.Classes;
using Domains.Interfaces.Repositorio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace ContatosAPI.Test
{
    [TestClass]
    [TestCategory("UnitTest > Controller > Contatos")]
    public class ContatosControllerTest
    {
        [TestMethod]
        [DataRow(3)]
        public void Must_Retun_Status404NotFound_If_NotFound_Contatos(int contatoId)
        {
            var repositorio = new Mock<IContatoRepositorio>();
            var request = new ContatosController(repositorio.Object);

            var response = request.GetById(contatoId).Result;

            Assert.AreEqual(404, ((NotFoundResult)response).StatusCode);
        }

        [TestMethod]
        [DataRow(1)]
        public void Must_Retun_Status200OK_If_Found_Contatos(int contatoId)
        {
            // Arrange
            var repositorio = new Mock<IContatoRepositorio>();
            repositorio.Setup(rep => rep.GetById(contatoId))
                .ReturnsAsync(new Contato { ContatoId = 1, Nome = "Maurício Marcos", Email = "001.mmarcos@gmail.com", Telefone = "16992339111", Descricao = "Contato de Emergência" });            

            // Act
            var request = new ContatosController(repositorio.Object);
            var response = request.GetById(contatoId).Result;

            // Assert
            Assert.AreEqual(200, ((OkObjectResult)response).StatusCode);
        }

    }
}
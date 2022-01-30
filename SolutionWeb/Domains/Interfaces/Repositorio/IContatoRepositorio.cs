using Domains.Classes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domains.Interfaces.Repositorio
{
    public interface IContatoRepositorio
    {
        Task<List<Contato>> GetAll();
        Task<Contato> GetById(int id);
        Task<Contato> Post(Contato e);
        Task Put(Contato e);
        Task Patch(Contato e, string descricao);
        Task Delete(Contato e);
    }
}
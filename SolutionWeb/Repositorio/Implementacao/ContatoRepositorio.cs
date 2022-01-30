using Domains.Classes;
using Domains.Interfaces.Repositorio;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositorio.Implementacao
{
    public class ContatoRepositorio : IContatoRepositorio
    {
        public static List<Contato> contatos;

        public ContatoRepositorio()
        {
            if (contatos is null)
            {
                contatos = new List<Contato>() 
                { 
                    new Contato { ContatoId = 1, Nome = "Maurício Marcos", Email = "001.mmarcos@gmail.com", Telefone = "16992339111", Descricao = "Contato de Emergência"},
                    new Contato { ContatoId = 2, Nome = "Anthony de Campos Marcos", Email = "anthony.marcos@gmail.com", Telefone = "16995669455", Descricao = "Filho"},
                    new Contato { ContatoId = 3, Nome = "Pizzaria Boa Pizza", Email = "boapizza@gmail.com", Telefone = "1633445599", Descricao = "Pizza Favorita da Casa"}
                };
            }
                
        }

        public async Task Delete(Contato e)
        {
            contatos.Remove(e);
            await Task.CompletedTask;
        }

        public async Task<List<Contato>> GetAll()
        {
            return await Task.Run(() => contatos);
        }

        public async Task<Contato> GetById(int id)
        {
            return await Task.Run(() => contatos.FirstOrDefault(c => c.ContatoId == id));
        }

        public async Task Patch(Contato contato, string descricao)
        {
            var c = await GetById(contato.ContatoId);
            contatos.Remove(c);

            c.AlterarDescricao(descricao);            
            contatos.Add(c);
        }

        public async Task<Contato> Post(Contato e)
        {
            var ultimoID = contatos.Count == 0 ? 0 : contatos.Max(e => e.ContatoId);
            e.ContatoId = ++ultimoID;

            contatos.Add(e);

            return await Task.Run(() => e);
        }

        public async Task Put(Contato e)
        {
            var contato = await GetById(e.ContatoId);

            contatos.Remove(contato);
            contatos.Add(e);
        }
    }
}
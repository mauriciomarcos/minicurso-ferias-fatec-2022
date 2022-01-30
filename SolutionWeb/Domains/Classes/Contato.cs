using System;

namespace Domains.Classes
{
    public class Contato
    {
        public int ContatoId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Descricao { get; set; }

        public void AlterarDescricao(string value)
        {
            Descricao = value;
        }
    }
}
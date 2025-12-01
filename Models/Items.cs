namespace SUPERRPGWPF.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Raridade { get; set; } = string.Empty;
        public decimal Preco { get; set; }

        // Chave estrangeira para Personagem
        public int PersonagemId { get; set; }
        public Personagem? Personagem { get; set; }
    }
}

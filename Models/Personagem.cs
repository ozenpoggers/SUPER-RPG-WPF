namespace SUPERRPGWPF.Models
{
    public class Personagem
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Classe { get; set; } = string.Empty;
        public int Nivel { get; set; }

        // Relação: um personagem pode ter vários itens
        public List<Item> Itens { get; set; } = new();

        // Sobrescreve ToString para exibir o nome no ComboBox
        public override string ToString()
        {
            return Nome;
        }
    }
}

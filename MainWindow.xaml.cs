using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using SUPERRPGWPF.Models;
using SUPERRPGWPF.Data;
using Microsoft.EntityFrameworkCore;

namespace SUPERRPGWPF
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<Item> _itens;
        private ObservableCollection<Personagem> _personagens;

        public MainWindow()
        {
            InitializeComponent();

            _itens = new ObservableCollection<Item>();
            _personagens = new ObservableCollection<Personagem>();

            dgItens.ItemsSource = _itens;
            dgPersonagens.ItemsSource = _personagens;
            cbPersonagemItem.ItemsSource = _personagens;

            // ✅ Carregar dados existentes ao abrir
            using var context = new AppDbContext();
            context.Database.EnsureCreated();

            foreach (var p in context.Personagens.ToList())
                _personagens.Add(p);

            foreach (var i in context.Itens.Include(x => x.Personagem).ToList())
                _itens.Add(i);
        }

        private void AdicionarPersonagem_Click(object sender, RoutedEventArgs e)
        {
            int nivel = int.TryParse(txtNivelPersonagem.Text, out var n) ? n : 1;

            var novoPersonagem = new Personagem
            {
                Nome = txtNomePersonagem.Text,
                Classe = txtClassePersonagem.Text,
                Nivel = nivel
            };

            _personagens.Add(novoPersonagem);

            txtNomePersonagem.Clear();
            txtClassePersonagem.Clear();
            txtNivelPersonagem.Clear();
        }

        private void SalvarPersonagens_Click(object sender, RoutedEventArgs e)
        {
            using var context = new AppDbContext();
            context.Database.EnsureCreated();

            context.Personagens.AddRange(_personagens.Where(p => p.Id == 0));
            context.SaveChanges();

            _personagens.Clear();
            foreach (var p in context.Personagens.ToList())
                _personagens.Add(p);

            MessageBox.Show("Personagens salvos com sucesso!");
        }

        private void RemoverPersonagem_Click(object sender, RoutedEventArgs e)
        {
            if (dgPersonagens.SelectedItem is Personagem personagemSelecionado)
            {
                using var context = new AppDbContext();
                context.Database.EnsureCreated();

                context.Personagens.Remove(personagemSelecionado);
                context.SaveChanges();

                _personagens.Remove(personagemSelecionado);

                MessageBox.Show("Personagem removido com sucesso!");
            }
            else
            {
                MessageBox.Show("Selecione um personagem para remover.");
            }
        }

        private void AdicionarItem_Click(object sender, RoutedEventArgs e)
        {
            if (cbPersonagemItem.SelectedItem is not Personagem personagemSelecionado)
            {
                MessageBox.Show("Selecione um personagem para vincular o item!", "Aviso",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            decimal preco = decimal.TryParse(txtPrecoItem.Text, out var p) ? p : 0;

            var novoItem = new Item
            {
                Nome = txtNomeItem.Text,
                Raridade = txtRaridadeItem.Text,
                Preco = preco,
                PersonagemId = personagemSelecionado.Id
                // ❌ Não setamos Personagem = personagemSelecionado
            };

            _itens.Add(novoItem);

            txtNomeItem.Clear();
            txtRaridadeItem.Clear();
            txtPrecoItem.Clear();
        }

        private void SalvarItens_Click(object sender, RoutedEventArgs e)
        {
            using var context = new AppDbContext();
            context.Database.EnsureCreated();

            context.Itens.AddRange(_itens.Where(i => i.Id == 0));
            context.SaveChanges();

            _itens.Clear();
            foreach (var i in context.Itens.Include(x => x.Personagem).ToList())
                _itens.Add(i);

            MessageBox.Show("Itens salvos com sucesso!");
        }

        private void RemoverItem_Click(object sender, RoutedEventArgs e)
        {
            if (dgItens.SelectedItem is Item itemSelecionado)
            {
                using var context = new AppDbContext();
                context.Database.EnsureCreated();

                context.Itens.Remove(itemSelecionado);
                context.SaveChanges();

                _itens.Remove(itemSelecionado);

                MessageBox.Show("Item removido com sucesso!");
            }
            else
            {
                MessageBox.Show("Selecione um item para remover.");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projLivrosLista
{
    class Program
    {
        static Livros livros = new Livros();

        static void Main(string[] args)
        {
            string choice = "";
            do
            {
                    Console.Clear();
                    Console.SetCursorPosition(19, 3);
                    Console.Write("----------------------------------");
                    Console.SetCursorPosition(19, 4);
                    Console.Write("|0. Sair                         |");
                    Console.SetCursorPosition(19, 6);
                    Console.Write("|1. Adicionar livro              |");
                    Console.SetCursorPosition(19, 8);
                    Console.Write("|2. Pesquisar livro (sintético)* |");
                    Console.SetCursorPosition(19, 10);
                    Console.Write("|3. Pesquisar livro (analítico)**|");
                    Console.SetCursorPosition(19, 12);
                    Console.Write("|4. Adicionar exemplar           |");
                    Console.SetCursorPosition(19, 14);
                    Console.Write("|5. Registrar empréstimo         |");
                    Console.SetCursorPosition(19, 16);
                    Console.Write("|6. Registrar devolução          |");
                    Console.SetCursorPosition(19, 17);
                    Console.Write("----------------------------------");
                    Console.SetCursorPosition(19, 18);
                    Console.Write("OPÇÃO:");

                    
                try
                {
                    choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "0": break;
                        case "1": adicionarLivro(); break;
                        case "2": pesquisarLivroSintetico(); break;
                        case "3": pesquisarLivroAnalitico(); break;
                        case "4": adicionarExemplar(); break;
                        case "5": registrarEmprestimo(); break;
                        case "6": registrarDevolucao(); break;
                        default: Console.WriteLine("Opção inválida."); break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadKey();
                }
            } while (choice != "0");

            System.Environment.Exit(0);
        }

        static void adicionarLivro()
        {

            Console.Write("\nDigite o ISBN: ");
            int isbn = Int32.Parse(Console.ReadLine());
            if (livros.pesquisar(new Livro(isbn)) != null) throw new Exception("Já existe um livro com esse ISBN");

            Console.Write("Digite o título: ");
            string titulo = Console.ReadLine();
            Console.Write("Digite o autor: ");
            string autor = Console.ReadLine();
            Console.Write("Digite a editora: ");
            string editora = Console.ReadLine();

            livros.adicionar(new Livro(isbn, titulo, autor, editora));
            Console.WriteLine("Exemplar cadastrado com sucesso!");
            Console.ReadKey();
        }

        static void pesquisarLivroSintetico()
        {

            Console.Write("\nDigite o ISBN: ");
            int isbn = Int32.Parse(Console.ReadLine());
            Livro livro = livros.pesquisar(new Livro(isbn));
            if (livro == null) throw new Exception("Livro não encontrado.");

            Console.WriteLine("Total de exemplares: " + livro.qtdeExemplares());
            Console.WriteLine("Total de exemplares disponíveis: " + livro.qtdeDisponiveis());
            Console.WriteLine("Total de empréstimos: " + livro.qtdeEmprestimos());
            Console.WriteLine("Percentual de disponibilidade: " + livro.percDisponibilidade().ToString("0.00") + "%");

            Console.ReadKey();
        }

        static void pesquisarLivroAnalitico()
        {

            Console.Write("\nDigite o ISBN: ");
            int isbn = Int32.Parse(Console.ReadLine());
            Livro livro = livros.pesquisar(new Livro(isbn));
            if (livro == null) throw new Exception("Livro não encontrado.");

            Console.WriteLine("Total de exemplares: " + livro.qtdeExemplares());
            Console.WriteLine("Total de exemplares disponíveis: " + livro.qtdeDisponiveis());
            Console.WriteLine("Total de empréstimos: " + livro.qtdeEmprestimos());
            Console.WriteLine("Percentual de disponibilidade: " + livro.percDisponibilidade().ToString("0.00") + "%");

            livro.Exemplares.ForEach(i => {
                Console.WriteLine("=========================================================");
                Console.WriteLine("Tombo: " + i.Tombo);
                i.Emprestimos.ForEach(j => {
                    String devolucao = (j.DtDevolucao != DateTime.MinValue) ? j.DtDevolucao.ToString() : "-------------------";
                    Console.WriteLine("----------------------------------------------------------");
                    Console.WriteLine("Data Empréstimo: " + j.DtEmprestimo);
                    Console.WriteLine("Data Devolução:  " + devolucao);
                });
            });

            Console.ReadKey();
        }

        static void adicionarExemplar()
        {

            Console.Write("\nDigite o ISBN: ");
            int isbn = Int32.Parse(Console.ReadLine());

            Livro livro = livros.pesquisar(new Livro(isbn));
            if (livro == null) throw new Exception("Livro não encontrado.");

            Console.Write("Digite o Tombo: ");
            int tombo = Int32.Parse(Console.ReadLine());
            livro.adicionarExemplar(new Exemplar(tombo));
            Console.WriteLine("Exemplar cadastrado com sucesso!");
            Console.ReadKey();
        }

        static void registrarEmprestimo()
        {
            Console.Write("\nDigite o ISBN: ");
            int isbn = Int32.Parse(Console.ReadLine());

            Livro livro = livros.pesquisar(new Livro(isbn));
            if (livro == null) throw new Exception("Livro não encontrado.");

            Exemplar exemplar = livro.Exemplares.FirstOrDefault(i => i.emprestar());
            if (exemplar != null) Console.WriteLine("Exemplar " + exemplar.Tombo + " emprestado com sucesso!");
            else throw new Exception("Não há exemplares disponíveis.");

            Console.ReadKey();
        }

        static void registrarDevolucao()
        {
            Console.Write("\nDigite o ISBN: ");
            int isbn = Int32.Parse(Console.ReadLine());

            Livro livro = livros.pesquisar(new Livro(isbn));
            if (livro == null) throw new Exception("Livro não encontrado.");

            Exemplar exemplar = livro.Exemplares.FirstOrDefault(i => i.devolver());
            if (exemplar != null) Console.WriteLine("Exemplar " + exemplar.Tombo + " devolvido com sucesso!");
            else Console.WriteLine("Não há exemplares emprestados.");
            Console.ReadKey();
        }
    }
}
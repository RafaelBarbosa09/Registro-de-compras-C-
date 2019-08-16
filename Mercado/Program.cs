using System;
using System.Collections.Generic;
using System.Linq;

namespace Mercado {
    class Program {
        static void Main(string[] args) {


            Console.WriteLine("Registro de produtos");

            //registrar produto
            Produto produto = new Produto();

            Console.WriteLine("Entre com o código");
            produto.Codigo = Console.ReadLine();

            Console.WriteLine("Entre com o nome");
            produto.Nome = Console.ReadLine();

            Console.WriteLine("Entre com o preço");
            produto.Preco = Convert.ToDecimal(Console.ReadLine());

            //registrar compra
            Console.WriteLine("Registrar compra do cliente:\n\n\n\n");
            Console.WriteLine("Entre com o CPF:");
            string cpf = Console.ReadLine();

            Cliente clienteQueEstaComprando = clientes.First(cliente => cliente.Cpf == cpf);

            if (clienteQueEstaComprando == null) {
                Console.WriteLine("Cliente não encontrado");
            } else {
                Console.WriteLine("Registrar produto pelo código...");
                Console.WriteLine("Entre com o código do produto");

                string codigoDoProduto = Console.ReadLine();

                if (codigoDoProduto == produto.Codigo) {
                    //registra
                    clienteQueEstaComprando.AdicionarCompraDe(produto);

                    decimal total = clienteQueEstaComprando.ObterValorTotalDaCompra();

                    //receber
                    Console.WriteLine("Entre com o valor do pagamento");
                    decimal pagamento = Convert.ToDecimal(Console.ReadLine());


                    if (pagamento > total) {
                        //
                        decimal troco = pagamento - total;
                        Console.WriteLine("Dar de troco" + troco);
                        Console.WriteLine("Emitir nota fiscal");
                    } else if (pagamento == total) {
                        Console.WriteLine("Emitir nota fiscal");
                    } else {
                        Console.WriteLine("Falta dinheiro");
                    }
                } else {
                    Console.WriteLine("Código do produto não registrado");
                }
            }

            //encerrar = emitir NF
            Console.ReadKey();
        }

        static List<Cliente> clientes = new List<Cliente>()
        {
            new Cliente{ Cpf = "123"}
        };
    }

    public class Produto {
        public string Nome { get; set; }
        public string Codigo { get; set; }
        public decimal Preco { get; set; }
    }

    public class Compra {
        public Produto Produto { get; set; }
        public Cliente Cliente { get; set; }
        public DateTime Data { get; set; }
        public bool FoiPaga { get; set; }
    }

    public class Cliente {
        public string Nome { get; set; }
        public string Cpf { get; set; }

        private List<Produto> compras2;
        public IEnumerable<Produto> Compras { get { return compras2; } }

        public void AdicionarCompraDe(Produto produto) {
            this.compras2.Add(produto);
        }

        public void CancelarCompraDo(Produto produto) {
            this.compras2.Remove(produto);
        }

        public decimal ObterValorTotalDaCompra2()
            => compras2.Sum(x => x.Preco);

        public decimal ObterValorTotalDaCompra() {
            decimal total = 0;

            foreach (var produto in compras2) {
                total = total + produto.Preco;
            }

            return total;
        }

        public Cliente() {
            compras2 = new List<Produto>();
        }
    }
}
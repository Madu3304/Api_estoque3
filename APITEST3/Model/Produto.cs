using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace APITEST3.Model
{

    [Table("Produtos")]
    public class Produto
    {
        [Key]
        public int Id_produto { get; set; }
        public string? Nome { get; set; }

        public string? Descricao { get; set; }
        public decimal Preco { get; set; }
        public string? ImagemUrl { get; set; }
        public float Estoque { get; set; }
        public DateTime DataCadastro { get; set; }


        //aqui estou apenas para ficar claro que a classe categoria vai receber essa chave estrangeira.


    }
}

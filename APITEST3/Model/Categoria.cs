using System.Collections.ObjectModel;
using APITEST3.Controllers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APITEST3.Model
{

    [Table("Categorias")]
    public class Categoria
    {

        //aqui estou iniciando a chave key
        public Categoria()
        {
            Produtos = new Collection<Produto>();
        }


        [Key]
        public int Id_categoria { get; set; }

        [Required]
        [StringLength(80)]
        public string? Nome { get; set; }

        [Required]
        [StringLength(80)]
        public string? ImagemUrl { get; set; }


        //declarando que essa classe recebe a chave primria de produto. 
        public ICollection<Produto>? Produtos { get; set; }

        [JsonIgnore]
        public Categoria? Categorias { get; set; }
        
    }
}

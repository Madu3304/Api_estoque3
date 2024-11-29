using APITEST3.Context;
using APITEST3.Model;
using APITEST3.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



namespace APITEST3.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class Categoria_contro : ControllerBase
    {

        private readonly IConfiguration configuration;

        private readonly AppDbContext _context;

        public Categoria_contro(AppDbContext context, IConfiguration configuration) //aqui já foi colocado a injeção 
        {
            _context = context;
            this.configuration = configuration;
        }

        //esste estou acrescentado junto com a injeção de dadaos da leitura da configuracao, que fiz a injeçao ali de  IConfiguration. 
        [HttpGet("LerArquivoConfiguracao")]
        public string GetValores
        {
            //aqui coloquei conforme coloquei na classe "appsettings.json"
            var valor1 = _configuration ["chave1"];
            var valor2 = _configuration["chave2"];

            var secao 1 = _configuration["secao1: chave2"];

            //aqui o meu retorno desejado das chaves:
            return $"Chave1 = {valor1} \nChave2 = {valor2} \nSecao1 => Chave2 = {secao1}";
        }



        //get para trazer lista de resultados.
        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> Get()
        {
            return _context.Categorias.AsNoTracking().ToList();
        }



        // get para trazer apenas um resulto informado o id
        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public ActionResult<Categoria> Get(int id)
        {
            var categoria = _context.Categorias.FirstOrDefault(p => p.Id_categoria == id);

            if (categoria is null) //aqui verifico se o produto é nulo. 
            {
                return NotFound("Categoria não encontrada");
            }

            return Ok(categoria);
        }


        // post para criar uma nova categoria
        [HttpPost]
        public ActionResult Post(Categoria categoria)
        {
            if (categoria is null) //aqui verifico se o produto é nulo. 
                return BadRequest();

            _context.Categorias.Add(categoria);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterCategoria", new { id = categoria.Id_categoria }, categoria);
        }


        // aqui para atualizar uma nova categoria.
        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Categoria categoria)
        {
            if (categoria is null)
                return NotFound();

            if (id != categoria.Id_categoria) { return BadRequest(); }

            _context.Entry(categoria).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok(categoria);
        }


        // aqui para deletar uma categoria qu já está no banco. 
        [HttpDelete("{id:int}")]
        public ActionResult<Categoria> Delete(int id)
        {
            var categoria = _context.Categorias.FirstOrDefault(p => p.Id_categoria == id);

            if (categoria is null) //aqui verifico se o produto é nulo. 
            { return NotFound(); }


            _context.Categorias.Remove(categoria);
            _context.SaveChanges();

            return Ok(categoria);

        }

        //###################################################
        // a seguir modelo de Action Assincrona:
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Produto>>> Get2()
        //{
        //    return await _context.Produtos.AsNoTracking().ToListAsync();
        //}





        // para acessar essa rota é preciso utilizar o endereço a seguir:
        [HttpGet("UsandoFromServices/{nome}")]
        public ActionResult<string> GetSaudacaoFromServices([FromServices] IMeuServico meuServico, string nome)
        {
            return meuServico.Saudacao(nome);
        }



    }
}

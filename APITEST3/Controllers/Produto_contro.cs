using APITEST3.Context;
using APITEST3.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;


namespace APITEST3.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProdutosController(AppDbContext context)
        { _context = context; }

        //public IEnumerable<Produto> Get()
        //{
        //    var proutos = _context.Produtos.ToList();
        //    return proutos;
        //}


        //aqui estou usando o IEnumerable e ToList para retornar uma lista desta resposta do endpoint. 
        // o if aqui é para retornar uma menssagem de erro caso não localize a lista. 
        [HttpGet]
        public ActionResult<IEnumerable<Produto>> Get()
        {
             var produtos = _context.Produtos.ToList();

            if (produtos is null) //aqui verifico se o produto é nulo. 
            {
                return NotFound("Produtos não encontrados");
            }
                return produtos;
        }

        //aqui é um método para retornar da pesquia apenas com um dado. 
        [HttpGet ("{id:int}", Name="ObterUmProduto")]
        public ActionResult<Produto> GET(int id)
        {
            var produto = _context.Produtos.FirstOrDefault(p => p.Id_produto == id);
            return NotFound("Produto não encontrado");
        }

        [HttpPost]
        public ActionResult POST(Produto produto)
        {
            if (produto is null) // verificando se é nulo
                return BadRequest();

            _context.Produtos.Add(produto);
            _context.SaveChanges();

            return CreatedAtRoute("ObterProduto", new { id = produto.Id_produto }, produto);
        }

        // está com erro pois não tenho a conexão com  banco. 


        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Produto produto)
        {
            if (produto is null)
                return NotFound();

            if (id != produto.Id_produto)
            {
                return BadRequest();
            }

            _context.Entry(produto).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(produto);

        }

        //###################################################
        // a seguir modelo de Action Assincrona:
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Categoria>>> Get2()
        //{
        //    return await _context.Categorias.ToListAsync();
        //}

        ////###################################################
        ////Aui estou definindo a URL deste endponit: 
        ////agora esse método esta exigindo o parâmetro 'nome', 
        //[HttpGet("{id}", Name = "obterProduto")]
        //public async Task<ActionResult<Produto>> Get(int id, [BindRequired] string nome)
        //{
        //    //chamei aqui o novo parâmetro que adicionei na url. 
        //    var nomeProduto = nome;

        //    var produto = await _context.Produtos.AsNoTracking().FirstOrDefaultAsync(P => P.Id_produto == id);

        //    if(produto is null) //aqui verifico se o produto é nulo. 
        //    {
        //        return NotFound();
        //    }

        //    return produto;
        //}
    }
}

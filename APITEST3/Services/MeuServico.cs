using Microsoft.AspNetCore.Mvc;


namespace APITEST3.Services
   
{
    public class MeuServico : IMeuServico
    {
        public string Saudaca(string nome)
        {
            throw new NotImplementedException();
        }

        public string Saudacao(string nome)
        {
            return $"Bem Vindo, {nome} \n\n {DateTime.UtcNow}";    
        }

        ActionResult<string> IMeuServico.Saudacao(string nome)
        {
            throw new NotImplementedException();
        }
    }
}

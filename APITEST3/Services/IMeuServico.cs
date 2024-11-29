using Microsoft.AspNetCore.Mvc;

namespace APITEST3.Services
{
    public interface IMeuServico
    {

        string Saudaca(string nome);
        ActionResult<string> Saudacao(string nome);
    }
}


//para ver o comportamento do "FROM SRVICES". 
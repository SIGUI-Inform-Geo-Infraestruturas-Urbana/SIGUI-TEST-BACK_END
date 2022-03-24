using Microsoft.AspNetCore.Mvc;
using WEBAPI_SIGUI_TEST_BACKEND.Services;

namespace WEBAPI_SIGUI_TEST_BACKEND.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MapeamentoController : ControllerBase
    {
        private readonly IMapeamentoService _mapeamentoServices;
        public MapeamentoController(IMapeamentoService mapeamentoServices) {
            _mapeamentoServices = mapeamentoServices;
        }
        // GET: /Mapeamento/
        [HttpGet]
        public List<object> GetMapeamentoTranform()
        {
            var data = Task.Run(() => _mapeamentoServices.SelectTransformGeometry());
            List<object> resultado = data.Result;
            return resultado;
        }
        //public List<object> GetMapeamento()
        //{
        //    var data = Task.Run(() => _mapeamentoServices.SelectDataGeometry());
        //    List<object> resultado = data.Result;
        //    return resultado;
        //}
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace IntegracaoVendas.API.Controllers
{
    /// <summary>
    /// Classe que contém os métodos utilizados na integração entre os sistemas de rastreamento e vendas
    /// </summary>
    [RoutePrefix("Integration")]
    public class IntegrationController : ApiController
    {
        #region Public Methods

        /// <summary>
        /// Método responsável por fazer a comunicação entre as plataformas de Rastreamento e Vendas, realizando a integração entre os dados
        /// </summary>
        /// <param name="postObject">Contém os dados que deverão ser integrados e processados</param>
        /// <returns></returns>
        [HttpPost]
        [HttpGet]
        [Route("IntegrateTrackingSalesSystem")]
        public async Task<IHttpActionResult> IntegrateTrackingSalesSystem(string postObject)
        {
            try
            {

                //Retorna para o request sucesso na operação
                return await Task.FromResult(Ok());
            }
            catch(Exception)
            {
                //Retorna para o request erro no processamento
                return base.InternalServerError();
            }
        }

        #endregion

        #region Members

        #endregion
    }
}
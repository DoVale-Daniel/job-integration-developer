using IntegracaoVendas.API.Extensions;
using IntegracaoVendas.API.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;
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
        /// <param name="postObject">Contém os dados no formato do sistema de Rastreamento, que deverão ser integrados e processados</param>
        /// <returns>Novo objeto no formato esperado pelo sistema de Vendas</returns>
        [HttpPost]
        [Route("IntegrateTrackingSalesSystem")]
        public async Task<IHttpActionResult> IntegrateTrackingSalesSystem(string orderData)
        {
            try
            {
                //Deserializa o json recebido em um objeto genérico, com o objetivo de criar um novo objeto apenas com as propriedades relevantes
                JObject jsonData = JObject.Parse(orderData);

                //Cria um novo objeto no formato suportado pelo sistema de Vendas
                var salesObject = new
                {
                    //Recupera o id do pedido
                    orderId = jsonData["order_id"],
                    //Recupera a descrição do enum já traduzida entre os status dos dois sistemas
                    status = this.GetEnumDescriptionByValue<EnumStatus.OrderStatus>(jsonData["event"]["status_id"].ToString()),
                    //Recupera a data do pedido
                    date = jsonData["event"]["date"]
                };

                //Serializa o novo objeto já convertido para o formato adequado ao sistema de vendas
                string jsonResult = JsonConvert.SerializeObject(salesObject);

                //Retorna para o request o objeto traduzido
                return await Task.FromResult(Ok(jsonResult));
            }
            catch(Exception)
            {
                //Retorna para o request erro no processamento
                return base.InternalServerError();
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Método genérico responsável por retornar a descrição do enum a partir de seu valor
        /// </summary>
        /// <typeparam name="TEnum">Tipo do enum que será manipulado</typeparam>
        /// <param name="value">Valor do enum especificado</param>
        /// <returns>Descrição do Enum especificado</returns>
        private string GetEnumDescriptionByValue<TEnum>(string value)
            where TEnum : struct
        {
            try
            {
                //Recupera o enum genérico a partir do seu tipo e valor
                Enum enu = (Enum)Enum.Parse(typeof(TEnum), value);

                //Retorna a descrição do enum especificado
                return enu.GetEnumDescription();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Members

        #endregion
    }
}
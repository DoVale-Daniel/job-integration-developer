using System.ComponentModel;

namespace IntegracaoVendas.API.Util
{
    /// <summary>
    /// Contém os enumeradores referentes ao status dos pedidos (valor numérico e descrição correspondente)
    /// </summary>
    public static class EnumStatus
    {
        public enum OrderStatus : int
        {
            /// <summary>
            /// Pedido Em Trânsito
            /// </summary>
            [Description("in_transit")]
            EmTransito      = 1,
            /// <summary>
            /// Pedido Saiu Para Entrega
            /// </summary>
            [Description("to_be_delivered")]
            SaiuParaEntrega = 2,
            /// <summary>
            /// Pedido Entregue
            /// </summary>
            [Description("delivered")]
            Entregue        = 3
        }
    }
}
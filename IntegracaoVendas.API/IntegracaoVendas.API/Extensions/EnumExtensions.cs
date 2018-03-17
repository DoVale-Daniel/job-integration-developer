using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web;

namespace IntegracaoVendas.API.Extensions
{
    /// <summary>
    /// Contém métodos de extensão para recuperação de valores dos Enums especificados
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Recupera o atributo de Descrição associado ao enum. Caso o atributo não esteja presente, retorna o próprio nome do enum
        /// </summary>
        /// <param name="enu">Enum que terá a descrição extraída</param>
        /// <returns>Descrição do Enum</returns>
        public static string GetEnumDescription(this Enum enu)
        {
            try
            {
                //Obtém acesso aos metadados do enum
                FieldInfo fieldInfo = enu.GetType().GetField(enu.ToString());

                //Recupera o atributo de descrição associado ao enum
                DescriptionAttribute description = (DescriptionAttribute)fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault();

                /*Caso o enum especificado possua uma descrição personalizada, então retorna seu valor.
                Caso contrário, retorna o próprio nome do enum*/
                return description != null ? description.Description : enu.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("Enum not speficied.", ex);
            }
        }
    }
}
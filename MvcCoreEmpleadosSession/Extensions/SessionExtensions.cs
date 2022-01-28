using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreEmpleadosSession.Extensions
{
    /*IMPORTANTE QUE ESTE STATIC*/
    public static  class SessionExtensions
    {
        public static void SetObject(this ISession session, string key, object value)
        {
            string data = JsonConvert.SerializeObject(value);
            session.SetString(key, data);
        }
        //HttpContext.Session.GetObject<T>("Key");
        public static T GetObject<T>(this ISession session, string key)
        {
            string data = session.GetString(key);
            //QUE SUCEDE EN NUESTRA APP CUANDO BUSCAMOS UNA CLAVE QUE NO EXISTE?  
            //SI NO ENCUENTRA LA KEY DEVOLVEMOS UN NULL
            if (data == null)
            {
                //SE DEVUELVE EL VALOR POR DEFECTO DEL GENERICO
                return default(T);
            }
            else
            {
                return JsonConvert.DeserializeObject<T>(data);
            }
        }
    }
}

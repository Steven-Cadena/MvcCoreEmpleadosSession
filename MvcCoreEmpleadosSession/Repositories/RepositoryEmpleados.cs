using MvcCoreEmpleadosSession.Data;
using MvcCoreEmpleadosSession.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreEmpleadosSession.Repositories
{
    public class RepositoryEmpleados
    {
        private EmpleadosContext context;
        public RepositoryEmpleados(EmpleadosContext context) 
        {
            this.context = context;
        }

        public List<Empleado> GetEmpleados() 
        {
            var consulta = from datos in this.context.Empleados
                           select datos;
            return consulta.ToList();
        }

        //METODO PARA DEVOLVER UN EMPLEADO
        public Empleado FindEmpleado(int idempleado) 
        {
            return this.context.Empleados.SingleOrDefault(x => x.IdEmpleado == idempleado);
        }
        //METODO PARA LA VERSION 3 
        //RECIBIREMOS UNA COLECCION DE INT Y DEVOLVEMOS LA COLECCION DE EMPLEADOS
        public List<Empleado> GetEmpleadosSession(List<int> idsEmpleados) 
        {
            //CUANDO UTILIZAMOS BUSQUEDA EN COLECCIONES SE UTILIZA EL METODO Contains
            //contains muy importante, para comprobar si están en la bbdd esos id
            var consulta = from datos in this.context.Empleados
                           where idsEmpleados.Contains(datos.IdEmpleado)
                           select datos;
            /*importante devolver null si no hay empleados en la session*/
            if (consulta.Count() == 0)
            {
                return null;  
            }
            return consulta.ToList();
        } 
    }

}

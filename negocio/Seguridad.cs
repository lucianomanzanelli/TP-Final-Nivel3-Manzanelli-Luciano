using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public static class Seguridad
    {
        public static bool SesionActiva(object user)
        {
            Persona persona = user != null ? (Persona)user : null;
            if (persona != null && persona.Id != 0)
                return true;
            else
                return false;
        }

        public static bool EsAdmin(object user)
        {
            Persona persona = user != null ? (Persona)user : null;
            return persona != null ? persona.Admin : false;
        }
    }
}

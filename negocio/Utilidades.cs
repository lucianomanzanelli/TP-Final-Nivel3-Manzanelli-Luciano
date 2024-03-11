using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public static class Utilidades
    {
        public static string ObtenerUrlImagen(object imagenUrl)
        {
            string url = imagenUrl as string;

            // Verificar si la URL es válida
            if (Uri.TryCreate(url, UriKind.Absolute, out Uri result) &&
                (result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps))
            {

                return url; // La URL es válida

            }
            else
            {
                // La URL no es válida, usar una URL predeterminada
                return ImagenDeRespaldo();
            }
        }

        public static string ImagenDeRespaldo()
        {
            return "https://grupoact.com.ar/wp-content/uploads/2020/04/placeholder.png";
        }

        
    }

}

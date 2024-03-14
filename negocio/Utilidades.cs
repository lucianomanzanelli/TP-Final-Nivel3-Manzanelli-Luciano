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
        public static string ObtenerUrlImagen(string imagenUrl)
        {
            string url = imagenUrl;

            if (Uri.TryCreate(url, UriKind.Absolute, out Uri result) &&
                (result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps))
            {
                if (UrlImagenEsValida(imagenUrl))
                    return url;
                else
                    return ImagenDeRespaldo();
            }
            else
                return ImagenDeRespaldo();
        }

        private static bool UrlImagenEsValida(string imageUrl)
        {
            try
            {
                // Intenta hacer una solicitud HTTP a la URL de la imagen
                using (WebClient client = new WebClient())
                {
                    byte[] imageData = client.DownloadData(imageUrl);
                    // Si la solicitud se realiza correctamente, la URL es válida
                        return true;
                    
                }
            }
            catch (WebException ex)
            {
                if (ex.Response is HttpWebResponse response && response.StatusCode == HttpStatusCode.Forbidden)
                    return true;
                else
                    return false;
            }
        }

        public static string ImagenDeRespaldo()
        {
            return "https://grupoact.com.ar/wp-content/uploads/2020/04/placeholder.png";
        }

        
    }

}

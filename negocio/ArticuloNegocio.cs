﻿using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using dominio;

namespace negocio
{
    public class ArticuloNegocio
    {
        public List<Articulo> listar(string id = "")
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = "select a.Id, a.Codigo, a.Nombre, a.Descripcion, a.ImagenUrl, " +
                    "a.Precio, c.Descripcion categoria, a.IdCategoria, a.IdMarca, m.Descripcion marca " +
                    "from ARTICULOS A, CATEGORIAS C, MARCAS M " +
                    "where a.IdCategoria = c.Id and a.IdMarca = m.Id ";

                if (id != "")
                {
                    consulta += "and a.id = " + id;
                }

                consulta += " order by a.nombre";

                datos.setearConsulta(consulta);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Codigo = Convert.ToString(datos.Lector["codigo"]);
                    aux.Nombre = Convert.ToString(datos.Lector["nombre"]);
                    aux.Descripcion = (string)datos.Lector["descripcion"];
                    aux.Precio = (decimal)datos.Lector["precio"];

                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                        aux.ImagenUrl = (string)datos.Lector["ImagenUrl"];

                    aux.Marca = new Marca();
                    aux.Marca.Id = (int)datos.Lector["IdMarca"];
                    aux.Marca.Descripcion = (string)datos.Lector["Marca"];

                    aux.Categoria = new Categoria();
                    aux.Categoria.Id = (int)datos.Lector["IdCategoria"];
                    aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];

                    lista.Add(aux);

                }

                datos.cerrarConexion();
                return lista;

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                datos.cerrarConexion();
            }

        }


        public List<Articulo> listarFavs(string id)
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = "select f.Id, a.Id 'idArt', a.Codigo, a.Nombre, a.Descripcion, a.ImagenUrl, " +
                    " a.Precio, c.Descripcion categoria, a.IdCategoria, a.IdMarca, m.Descripcion marca " +
                    "from FAVORITOS f " +
                    "join USERS u on u.Id = f.IdUser " +
                    "join ARTICULOS a on a.Id = f.IdArticulo " +
                    "join MARCAS m on m.Id = a.IdMarca " +
                    "join CATEGORIAS c on c.Id = a.IdCategoria " +
                    "where u.Id = @idUser";

                datos.setearParametro("@idUser", id);

                datos.setearConsulta(consulta);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)datos.Lector["IdArt"];
                    aux.Codigo = Convert.ToString(datos.Lector["codigo"]);
                    aux.Nombre = Convert.ToString(datos.Lector["nombre"]);
                    aux.Descripcion = (string)datos.Lector["descripcion"];
                    aux.Precio = (decimal)datos.Lector["precio"];

                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                        aux.ImagenUrl = (string)datos.Lector["ImagenUrl"];

                    aux.Marca = new Marca();
                    aux.Marca.Id = (int)datos.Lector["IdMarca"];
                    aux.Marca.Descripcion = (string)datos.Lector["Marca"];

                    aux.Categoria = new Categoria();
                    aux.Categoria.Id = (int)datos.Lector["IdCategoria"];
                    aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];

                    lista.Add(aux);

                }

                datos.cerrarConexion();
                return lista;

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                datos.cerrarConexion();
            }

        }

        public void agregarFav(int idArt, int idUser)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("insert into favoritos values (@idUser, @idArt)");
                datos.setearParametro("@idUser", idUser);
                datos.setearParametro("@idArt", idArt);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public void eliminarFav(int idArt, int idUser)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("delete from favoritos where idUser = @idUser and idArticulo = @idArt");
                datos.setearParametro("@idUser", idUser);
                datos.setearParametro("@idArt", idArt);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public List<int> IDsFavoritos(int idUser)
        {
            List<int> lstFavs = new List<int> {};
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT IdArticulo FROM FAVORITOS where IdUser = @idUser");
                datos.setearParametro("@idUser", idUser);

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    lstFavs.Add((int)datos.Lector["idArticulo"]);
                }

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                datos.cerrarConexion();
            }


            return lstFavs;
        }


        public void agregar(Articulo nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT INTO ARTICULOS (Codigo, Nombre, Descripcion, ImagenUrl, Precio, IdCategoria, IdMarca) VALUES (@codigo, @nombre, @descripcion, @imagenUrl, @precio, @idCategoria, @idMarca)");
                datos.setearParametro("@codigo", nuevo.Codigo);
                datos.setearParametro("@nombre", nuevo.Nombre);
                datos.setearParametro("@descripcion", nuevo.Descripcion);
                datos.setearParametro("@imagenUrl", nuevo.ImagenUrl);
                datos.setearParametro("@precio", nuevo.Precio);
                datos.setearParametro("@idCategoria", nuevo.Categoria.Id);
                datos.setearParametro("@idMarca", nuevo.Marca.Id);

                datos.ejecutarAccion();

            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                datos.cerrarConexion();
            }

        }

        public void modificar(Articulo articulo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("update ARTICULOS set Codigo = @codigo, Nombre = @nombre, Descripcion = @descripcion, IdMarca = @idMarca, IdCategoria = @idCategoria, ImagenUrl = @imagen, Precio = @precio where Id = @id");
                datos.setearParametro("@codigo", articulo.Codigo);
                datos.setearParametro("@nombre", articulo.Nombre);
                datos.setearParametro("@descripcion", articulo.Descripcion);
                datos.setearParametro("@idMarca", articulo.Marca.Id);
                datos.setearParametro("@idCategoria", articulo.Categoria.Id);
                datos.setearParametro("@imagen", articulo.ImagenUrl);
                datos.setearParametro("@precio", articulo.Precio);
                datos.setearParametro("@id", articulo.Id);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void eliminar(int id)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("delete from articulos where id = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarAccion();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Articulo> filtrar(string campo, string criterio, string filtro)
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = "select a.Id, a.Codigo, a.Nombre, a.Descripcion, a.ImagenUrl, a.Precio, c.Descripcion categoria, a.IdCategoria, a.IdMarca, m.Descripcion marca from ARTICULOS A, CATEGORIAS C, MARCAS M where a.IdCategoria = c.Id and a.IdMarca = m.Id and ";

                if (campo == "Producto")
                {
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += "Nombre like '" + filtro + "%'";
                            break;
                        case "Termina con":
                            consulta += "Nombre like '%" + filtro + "'";
                            break;
                        default:
                            consulta += "Nombre like '%" + filtro + "%'";
                            break;
                    }
                    consulta += " order by Nombre";
                }
                else if (campo == "Marca")
                {

                    consulta += "m.Descripcion = '" + filtro + "'";

                }
                else if (campo == "Precio")
                {
                    if (string.IsNullOrEmpty(filtro) || string.IsNullOrWhiteSpace(filtro))
                    {
                        consulta += "precio > 0 ";

                    }
                    else
                    {

                        switch (criterio)
                        {
                            case "Igual a":
                                consulta += "precio = " + Convert.ToInt32(filtro);
                                break;
                            case "Menor a":
                                consulta += "precio between 0 and " + Convert.ToInt32(filtro);
                                break;
                            case "Mayor a":
                                consulta += "precio > " + Convert.ToInt32(filtro);
                                break;
                            default:
                                consulta += "precio > 0 ";
                                break;
                        }
                    }
                    consulta += " order by precio";
                }
                else if (campo == "Categoría")
                {
                    consulta += "c.Descripcion = '" + filtro + "'";

                }

                datos.setearConsulta(consulta);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo articulo = new Articulo();

                    articulo.Id = (int)datos.Lector["Id"];
                    articulo.Codigo = datos.Lector["Codigo"].ToString();
                    articulo.Nombre = datos.Lector["Nombre"].ToString();
                    articulo.Descripcion = datos.Lector["Descripcion"].ToString();

                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                        articulo.ImagenUrl = (string)datos.Lector["ImagenUrl"];

                    articulo.Precio = (decimal)datos.Lector["Precio"];

                    articulo.Categoria = new Categoria();
                    articulo.Categoria.Id = (int)datos.Lector["IdCategoria"];
                    articulo.Categoria.Descripcion = (string)datos.Lector["Categoria"];

                    articulo.Marca = new Marca();
                    articulo.Marca.Id = (int)datos.Lector["IdMarca"];
                    articulo.Marca.Descripcion = (string)datos.Lector["Marca"];

                    lista.Add(articulo);
                }


                return lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}

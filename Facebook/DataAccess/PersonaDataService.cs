using Facebook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Mvc;

namespace Facebook.DataAccess
{
    public class PersonaDataService
    {
        private readonly SqlClient _client;
        private static PersonaDataService _personaDataService;
        private readonly string connectionString = "Data Source = DESKTOP-F4DEC2L\\SQLEXPRESS; initial catalog = Facebook;integrated security = True";

        private PersonaDataService()
        {
            _client = SqlClient.GetSqlClient(connectionString);
        }
        public static PersonaDataService GetPersonaDataService()
        {
            if(_personaDataService == null)
            {
                _personaDataService = new PersonaDataService();
            }
            return _personaDataService;
        }
        public List<Persona> GetAmigos(int idPersona)   
        {
            var result = new List<Persona>();
            try
            {
                if (_client.Open())
                {
                    var command = new SqlCommand
                    {
                        Connection = _client.GetConnection(),
                        CommandText = "getamigos",
                        CommandType = CommandType.StoredProcedure
                    };

                    var par1 = new SqlParameter("@idPersona", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Input,
                        Value = idPersona
                    };

                    var par2 = new SqlParameter("@hasherror", SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(par1);
                    command.Parameters.Add(par2);

                    var dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        var amigo = new Persona
                        {
                            idPersona = Convert.ToInt32(dataReader["idPersona"].ToString()),
                            Nombre = dataReader["Nombre"].ToString(),
                            Apellido = dataReader["Apellido"].ToString(),
                            correo_electronico = dataReader["correo_electronico"].ToString(),                            
                            fecha_nac = dataReader["fecha_nac"].ToString(),
                            picture = dataReader["picture"].ToString(),
                            descripcion = dataReader["descripcion"].ToString(),
                        };
                        result.Add(amigo);
                    }
                }
            }
            catch(Exception e)
            {
                Console.Write(e);
                //do something
            }
            finally
            {
                _client.Close();
            }
            return result;
        }

        public List<Persona> GetPersonas()
        {
            var result = new List<Persona>();
            try
            {
                if (_client.Open())
                {
                    var command = new SqlCommand
                    {
                        Connection = _client.GetConnection(),
                        CommandText = "getPersonas",
                        CommandType = CommandType.StoredProcedure
                    };

                    var par1 = new SqlParameter("@hasherror", SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(par1);

                    var dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        var amigo = new Persona
                        {
                            idPersona = Convert.ToInt32(dataReader["idPersona"].ToString()),
                            Nombre = dataReader["Nombre"].ToString(),
                            Apellido = dataReader["Apellido"].ToString(),
                            correo_electronico = dataReader["correo_electronico"].ToString(),
                            fecha_nac = dataReader["fecha_nac"].ToString(),
                            picture = dataReader["picture"].ToString(),
                            descripcion = dataReader["descripcion"].ToString(),
                        };
                        result.Add(amigo);
                    }
                }
            }
            catch (Exception e)
            {
                Console.Write(e);
                //do something
            }
            finally
            {
                _client.Close();
            }
            return result;
        }

        public bool RegistrarPersona(Persona persona)
        {
            var result = false;
            try
            {
                if (_client.Open())
                {
                    var command = new SqlCommand
                    {
                        Connection = _client.GetConnection(),
                        CommandText = "registrar",
                        CommandType = CommandType.StoredProcedure
                    };

                    var par1 = new SqlParameter("@Nombre", SqlDbType.NVarChar)
                    {
                        Direction = ParameterDirection.Input,
                        Value = persona.Nombre
                    };

                    var par2 = new SqlParameter("@Apellido", SqlDbType.NVarChar)
                    {
                        Direction = ParameterDirection.Input,
                        Value = persona.Apellido
                    };

                    var par3 = new SqlParameter("@correo_electronico", SqlDbType.NVarChar)
                    {
                        Direction = ParameterDirection.Input,
                        Value = persona.correo_electronico
                    };

                    var par4 = new SqlParameter("@contraseña", SqlDbType.NVarChar)
                    {
                        Direction = ParameterDirection.Input,
                        Value = persona.contraseña
                    };
                    //modifique NVarChar por Date por si despues no funciona
                    var par5 = new SqlParameter("@fecha_nac", SqlDbType.Date)
                    {
                        Direction = ParameterDirection.Input,
                        Value = persona.fecha_nac
                    };

                    var par6 = new SqlParameter("@haserror", SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(par1);
                    command.Parameters.Add(par2);
                    command.Parameters.Add(par3);
                    command.Parameters.Add(par4);
                    command.Parameters.Add(par5);
                    command.Parameters.Add(par6);

                    command.ExecuteNonQuery();

                    result = !Convert.ToBoolean(command.Parameters["@haserror"].Value.ToString());

                }
            }
            catch (Exception)
            {
                result = false;
            }
            finally
            {
                _client.Close();
            }
            return result;
        }

        public bool AgregarAmigo(int id1, int id2)
        {
            var result = false;
            try
            {
                if (_client.Open())
                {
                    var command = new SqlCommand
                    {
                        Connection = _client.GetConnection(),
                        CommandText = "agregarAmigo",
                        CommandType = CommandType.StoredProcedure
                    };

                    var par1 = new SqlParameter("@id1", SqlDbType.NVarChar)
                    {
                        Direction = ParameterDirection.Input,
                        Value = id1
                    };
                    var par2 = new SqlParameter("@id2", SqlDbType.NVarChar)
                    {
                        Direction = ParameterDirection.Input,
                        Value = id2
                    };
                    var par3 = new SqlParameter("@haserror", SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(par1);
                    command.Parameters.Add(par2);
                    command.Parameters.Add(par3);

                    command.ExecuteNonQuery();
                    result = !Convert.ToBoolean(command.Parameters["@haserror"].Value.ToString());
                }
            }
            catch
            {
                result = false;
            }
            finally
            {
                _client.Close();
            }
            return result;
        }

        public List<IPost> GetPosts(int idPersona)
        {
            var result = new List<IPost>();
            try
            {
                if (_client.Open())
                {
                    var command = new SqlCommand
                    {
                        Connection = _client.GetConnection(),
                        CommandText = "getPosts",
                        CommandType = CommandType.StoredProcedure
                    };

                    var par1 = new SqlParameter("@idPersona", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Input,
                        Value = idPersona
                    };

                    var par2 = new SqlParameter("@hasherror", SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(par1);
                    command.Parameters.Add(par2);

                    var dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        var id = Convert.ToInt32(dataReader["idPost"].ToString());
                        var mensaje = dataReader["mensaje"].ToString();
                        var likes = Convert.ToInt32(dataReader["likes"].ToString());
                        var img = dataReader["imagen"].ToString();
                        Post post;

                        if (img != "")
                        {
                             post = new ImagePostCreator(mensaje, idPersona, img);
                        }
                        else
                        {
                            post = new MessagePostCreator(mensaje, idPersona);
                        }
                        var p = post.CreatePost();
                        
                        p.idPost = id;
                        p.likes = likes;
                        result.Add(p);
                    }
                }
            }
            catch (Exception e)
            {
                Console.Write(e);
                //do something
            }
            finally
            {
                _client.Close();
            }
            return result;
        }

        public bool AgregarPost(string mensaje, int id, string img)
        {
            var result = false;
            try
            {
                if (_client.Open())
                {
                    var command = new SqlCommand
                    {
                        Connection = _client.GetConnection(),
                        CommandText = "agregarPost",
                        CommandType = CommandType.StoredProcedure
                    };

                    var par1 = new SqlParameter("@mensaje", SqlDbType.NVarChar)
                    {
                        Direction = ParameterDirection.Input,
                        Value = mensaje
                    };
                    var par2 = new SqlParameter("@imagen", SqlDbType.NVarChar)
                    {
                        Direction = ParameterDirection.Input,
                        Value = img
                    };
                    var par3 = new SqlParameter("@idPersona", SqlDbType.NVarChar)
                    {
                        Direction = ParameterDirection.Input,
                        Value = id
                    };
                    var par4 = new SqlParameter("@haserror", SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(par1);
                    command.Parameters.Add(par2);
                    command.Parameters.Add(par3);
                    command.Parameters.Add(par4);

                    command.ExecuteNonQuery();
                    result = !Convert.ToBoolean(command.Parameters["@haserror"].Value.ToString());
                }
            }
            catch(Exception e)
            {
                Console.Write("--------------EROOOOOOOOOOOOOOOOR", e,"TERMINE EL EROOOOOOR");
                result = false;
            }
            finally
            {
                _client.Close();
            }
            return result;
        }

        public List<Persona> GetPersonaWithEmail(Persona p)
        {
            var email = p.correo_electronico;
            var psw = p.contraseña;
            var result = new List<Persona>();
            try
            {
                if (_client.Open())
                {
                    var command = new SqlCommand
                    {
                        Connection = _client.GetConnection(),
                        CommandText = "getPersonasWithEmail",
                        CommandType = CommandType.StoredProcedure
                    };

                    var par1 = new SqlParameter("@email", SqlDbType.NVarChar)
                    {
                        Direction = ParameterDirection.Input,
                        Value = email
                    };

                    var par2 = new SqlParameter("@haserror", SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(par1);
                    command.Parameters.Add(par2);
                    //terminar esto
                    var dataReader = command.ExecuteReader();
                    //var x = command.ExecuteScalar();
                    while (dataReader.Read())
                    {
                        var amigo = new Persona
                        {
                            idPersona = Convert.ToInt32(dataReader["idPersona"].ToString()),
                            Nombre = dataReader["Nombre"].ToString(),
                            Apellido = dataReader["Apellido"].ToString(),
                            correo_electronico = dataReader["correo_electronico"].ToString(),
                            contraseña = dataReader["contraseña"].ToString(),
                            fecha_nac = dataReader["fecha_nac"].ToString(),
                            picture = dataReader["picture"].ToString(),
                            descripcion = dataReader["descripcion"].ToString(),
                        };
                        result.Add(amigo);
                    }
                }
            }
            catch (Exception e)
            {
                Console.Write(e);
                //do something
            }
            finally
            {
                _client.Close();
            }
            return result;
        }
        public List<Persona> GetPersonaWithId(int idPersona)
        {
            var result = new List<Persona>();
            try
            {
                if (_client.Open())
                {
                    var command = new SqlCommand
                    {
                        Connection = _client.GetConnection(),
                        CommandText = "getpersonawithid",
                        CommandType = CommandType.StoredProcedure
                    };

                    var par1 = new SqlParameter("@idPersona", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Input,
                        Value = idPersona
                    };

                    var par2 = new SqlParameter("@haserror", SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(par1);
                    command.Parameters.Add(par2);

                    var dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        var amigo = new Persona
                        {
                            idPersona = Convert.ToInt32(dataReader["idPersona"].ToString()),
                            Nombre = dataReader["Nombre"].ToString(),
                            Apellido = dataReader["Apellido"].ToString(),
                            correo_electronico = dataReader["correo_electronico"].ToString(),
                            contraseña = dataReader["contraseña"].ToString(),
                            fecha_nac = dataReader["fecha_nac"].ToString(),
                            picture = dataReader["picture"].ToString(),
                            descripcion = dataReader["descripcion"].ToString(),
                        };
                        result.Add(amigo);
                    }
                }
            }
            catch (Exception e)
            {
                Console.Write(e);
                //do something
            }
            finally
            {
                _client.Close();
            }
            return result;
        }

        public bool EditarPersona(Persona persona)
        {
            var result = false;
            try
            {
                if (_client.Open())
                {
                    var command = new SqlCommand
                    {
                        Connection = _client.GetConnection(),
                        CommandText = "editUser",
                        CommandType = CommandType.StoredProcedure
                    };

                    var par9 = new SqlParameter("@id", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Input,
                        Value = persona.idPersona
                    };

                    var par1 = new SqlParameter("@Nombre", SqlDbType.NVarChar)
                    {
                        Direction = ParameterDirection.Input,
                        Value = persona.Nombre
                    };

                    var par2 = new SqlParameter("@Apellido", SqlDbType.NVarChar)
                    {
                        Direction = ParameterDirection.Input,
                        Value = persona.Apellido
                    };

                    var par3 = new SqlParameter("@correo_electronico", SqlDbType.NVarChar)
                    {
                        Direction = ParameterDirection.Input,
                        Value = persona.correo_electronico
                    };

                    var par4 = new SqlParameter("@contraseña", SqlDbType.NVarChar)
                    {
                        Direction = ParameterDirection.Input,
                        Value = persona.contraseña
                    };

                    var par5 = new SqlParameter("@fecha_nac", SqlDbType.Date)
                    {
                        Direction = ParameterDirection.Input,
                        Value = persona.fecha_nac
                    };

                    var par6 = new SqlParameter("@picture", SqlDbType.NVarChar)
                    {
                        Direction = ParameterDirection.Input,
                        Value = persona.picture
                    };

                    var par7 = new SqlParameter("@descripcion", SqlDbType.NVarChar)
                    {
                        Direction = ParameterDirection.Input,
                        Value = persona.descripcion
                    };

                    var par8 = new SqlParameter("@haserror", SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };


                    command.Parameters.Add(par9);
                    command.Parameters.Add(par1);
                    command.Parameters.Add(par2);
                    command.Parameters.Add(par3);
                    command.Parameters.Add(par4);
                    command.Parameters.Add(par5);
                    command.Parameters.Add(par6);
                    command.Parameters.Add(par7);
                    command.Parameters.Add(par8);
                    

                    command.ExecuteNonQuery();

                    result = !Convert.ToBoolean(command.Parameters["@haserror"].Value.ToString());

                }
            }
            catch (Exception e)
            {
                result = false;
            }
            finally
            {
                _client.Close();
            }
            return result;
        }
        public List<IPost> GetPostWithId(int idPost)
        {
            var result = new List<IPost>();
            try
            {
                if (_client.Open())
                {
                    var command = new SqlCommand
                    {
                        Connection = _client.GetConnection(),
                        CommandText = "getpostwithid",
                        CommandType = CommandType.StoredProcedure
                    };

                    var par1 = new SqlParameter("@idPost", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Input,
                        Value = idPost
                    };

                    var par2 = new SqlParameter("@haserror", SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(par1);
                    command.Parameters.Add(par2);

                    var dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        var pic = dataReader["imagen"].ToString();
                        IPost post;
                        if (pic == "")
                        {
                            post = new MessagePost
                            {
                                idPost = Convert.ToInt32(dataReader["idPost"].ToString()),
                                idPersona = Convert.ToInt32(dataReader["idPersona"].ToString()),
                                mensaje = dataReader["mensaje"].ToString(),
                                imagen = pic,
                                likes = Convert.ToInt32(dataReader["likes"].ToString()),
                            };
                        }
                        else
                        {
                            post = new ImagePost()
                            {
                                idPost = Convert.ToInt32(dataReader["idPost"].ToString()),
                                idPersona = Convert.ToInt32(dataReader["idPersona"].ToString()),
                                mensaje = dataReader["mensaje"].ToString(),
                                imagen = pic,
                                likes = Convert.ToInt32(dataReader["likes"].ToString()),
                            };
                        }
                        result.Add(post);
                    }
                }
            }
            catch (Exception e)
            {
                Console.Write(e);
                //do something
            }
            finally
            {
                _client.Close();
            }
            return result;
        }
        public bool LikePost(int idPost)
        {
            var result = false;
            try
            {
                if (_client.Open())
                {
                    var command = new SqlCommand
                    {
                        Connection = _client.GetConnection(),
                        CommandText = "likePost",
                        CommandType = CommandType.StoredProcedure
                    };

                    var par1 = new SqlParameter("@idPost", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Input,
                        Value = idPost
                    };
                    
                    var par2 = new SqlParameter("@haserror", SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(par1);
                    command.Parameters.Add(par2);
                    
                    command.ExecuteNonQuery();

                    result = !Convert.ToBoolean(command.Parameters["@haserror"].Value.ToString());

                }
            }
            catch (Exception e)
            {
                result = false;
            }
            finally
            {
                _client.Close();
            }
            return result;
        }

        public bool AgregarComentario(int idPersona, string mensaje, int idPost)
        {
            var result = false;
            try
            {
                if (_client.Open())
                {
                    var command = new SqlCommand
                    {
                        Connection = _client.GetConnection(),
                        CommandText = "agregarComentario",
                        CommandType = CommandType.StoredProcedure
                    };

                    var par1 = new SqlParameter("@idPersona", SqlDbType.NVarChar)
                    {
                        Direction = ParameterDirection.Input,
                        Value = idPersona
                    };


                    var par2 = new SqlParameter("@mensaje", SqlDbType.NVarChar)
                    {
                        Direction = ParameterDirection.Input,
                        Value = mensaje
                    };

                    var par3 = new SqlParameter("@idPost", SqlDbType.NVarChar)
                    {
                        Direction = ParameterDirection.Input,
                        Value = idPost
                    };

                    var par4 = new SqlParameter("@haserror", SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(par1);
                    command.Parameters.Add(par2);
                    command.Parameters.Add(par3);
                    command.Parameters.Add(par4);

                    command.ExecuteNonQuery();
                    result = !Convert.ToBoolean(command.Parameters["@haserror"].Value.ToString());
                }
            }
            catch (Exception e)
            {
                result = false;
            }
            finally
            {
                _client.Close();
            }
            return result;
        }

        public List<Comentario> GetComentarios(int idPost)
        {
            var result = new List<Comentario>();
            try
            {
                if (_client.Open())
                {
                    var command = new SqlCommand
                    {
                        Connection = _client.GetConnection(),
                        CommandText = "getComentarios",
                        CommandType = CommandType.StoredProcedure
                    };

                    var par1 = new SqlParameter("@idPost", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Input,
                        Value = idPost
                    };

                    var par2 = new SqlParameter("@haserror", SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(par1);
                    command.Parameters.Add(par2);

                    var dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        var idPersona = Convert.ToInt32(dataReader["idPersona"].ToString());
                        var mensaje = dataReader["mensaje"].ToString();
                        var u = new Persona
                        {
                            idPersona = Convert.ToInt32(dataReader["idPersona"].ToString()),
                            Nombre = dataReader["Nombre"].ToString(),
                            Apellido = dataReader["Apellido"].ToString(),
                            correo_electronico = dataReader["correo_electronico"].ToString(),
                            contraseña = dataReader["contraseña"].ToString(),
                            fecha_nac = dataReader["fecha_nac"].ToString(),
                            picture = dataReader["picture"].ToString(),
                            descripcion = dataReader["descripcion"].ToString(),
                        };
                        Comentario coment = new Comentario
                        {
                            user = u,
                            comentario = mensaje,
                        };
                        result.Add(coment);
                    }
                }
            }
            catch (Exception e)
            {
                Console.Write(e);
                //do something
            }
            finally
            {
                _client.Close();
            }
            return result;
        }
        public bool EliminarAmigo(int id1, int id2)
        {
            var result = false;
            try
            {
                if (_client.Open())
                {
                    var command = new SqlCommand
                    {
                        Connection = _client.GetConnection(),
                        CommandText = "borrarAmigo",
                        CommandType = CommandType.StoredProcedure
                    };

                    var par1 = new SqlParameter("@id1", SqlDbType.NVarChar)
                    {
                        Direction = ParameterDirection.Input,
                        Value = id1
                    };
                    var par2 = new SqlParameter("@id2", SqlDbType.NVarChar)
                    {
                        Direction = ParameterDirection.Input,
                        Value = id2
                    };
                    var par3 = new SqlParameter("@haserror", SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(par1);
                    command.Parameters.Add(par2);
                    command.Parameters.Add(par3);

                    command.ExecuteNonQuery();
                    result = !Convert.ToBoolean(command.Parameters["@haserror"].Value.ToString());
                }
            }
            catch
            {
                result = false;
            }
            finally
            {
                _client.Close();
            }
            return result;
        }
        public List<int> GetHashtags(string hashtag)
        {
            //ids de publicaciones
            var result = new List<int>();
            try
            {
                if (_client.Open())
                {
                    var command = new SqlCommand
                    {
                        Connection = _client.GetConnection(),
                        CommandText = "getHashtag",
                        CommandType = CommandType.StoredProcedure
                    };

                    var par1 = new SqlParameter("@hashtag", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Input,
                        Value = hashtag
                    };

                    var par2 = new SqlParameter("@haserror", SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(par1);
                    command.Parameters.Add(par2);

                    var dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        var id = Convert.ToInt32(dataReader["idPost"].ToString());
                        result.Add(id);
                    }
                }
            }
            catch (Exception e)
            {
                Console.Write(e);
                //do something
            }
            finally
            {
                _client.Close();
            }
            return result;
        }
        public bool AgregarHashtag(string hashtag, int idPost)
        {
            var result = false;
            try
            {
                if (_client.Open())
                {
                    var command = new SqlCommand
                    {
                        Connection = _client.GetConnection(),
                        CommandText = "addHashtags",
                        CommandType = CommandType.StoredProcedure
                    };

                    var par1 = new SqlParameter("@mensaje", SqlDbType.NVarChar)
                    {
                        Direction = ParameterDirection.Input,
                        Value = hashtag
                    };

                    var par2 = new SqlParameter("@idPost", SqlDbType.NVarChar)
                    {
                        Direction = ParameterDirection.Input,
                        Value = idPost
                    };

                    var par3 = new SqlParameter("@haserror", SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(par1);
                    command.Parameters.Add(par2);
                    command.Parameters.Add(par3);

                    command.ExecuteNonQuery();
                    result = !Convert.ToBoolean(command.Parameters["@haserror"].Value.ToString());
                }
            }
            catch (Exception e)
            {
                result = false;
            }
            finally
            {
                _client.Close();
            }
            return result;
        }


    }
}

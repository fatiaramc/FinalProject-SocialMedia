﻿using Facebook.Models;
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

        private PersonaDataService(string connectionString)
        {
            _client = SqlClient.GetSqlClient(connectionString);
        }
        public static PersonaDataService GetPersonaDataService(string connectionString)
        {
            if(_personaDataService == null)
            {
                _personaDataService = new PersonaDataService(connectionString);
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
                            //idPersona = Convert.ToInt32(dataReader["idPersona"].ToString()),
                            Nombre = dataReader["Nombre"].ToString(),
                            Apellido = dataReader["Apellido"].ToString(),
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

                    var par5 = new SqlParameter("@fecha_nac", SqlDbType.NVarChar)
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

        /*public List<Persona> GetPosts(int idPersona)
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
                        var post = new Persona
                        {
                            //idPersona = Convert.ToInt32(dataReader["idPersona"].ToString()),
                            Nombre = dataReader["Nombre"].ToString(),
                            Apellido = dataReader["Apellido"].ToString(),
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
        }*/
    }
}
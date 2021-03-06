﻿using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Modelo;
using System.Data;
using ET12.AGBD;

namespace DAO
{
    public class DAOMySQL: IDAO
    {
        MySqlConnection conexion;
        MySqlCommand comando;
        MySqlDataAdapter adaptador;

        public DAOMySQL(string cadena)
        {
            conexion = new MySqlConnection(cadena);
        }
        public void guardarMediciones(List<Medicion> mediciones)
        {
            mediciones.ForEach(m => this.guardarMediciones(m));
        }

        public void guardarTipoMedicion(TipoMedicion tipoMedicion)
        {
            instanciarComando("altatipomedicion");
            cargarParametro("unTipoMedicion", MySqlDbType.VarChar, 45, tipoMedicion.Nombre);
            cargarParametro("idGenerado", MySqlDbType.UByte, DBNull.Value);
            setearComoSalida("idGenerado");
            ejecutarComando();
            tipoMedicion.Id = Convert.ToByte(comando.Parameters["idGenerado"].Value);
        }
        private void cargarParametro(string nombre, MySqlDbType tipoDb, int longitud, object valor)
        {
            cargarParametro(nombre, tipoDb, valor);
            comando.Parameters[nombre].Size = longitud;
        }    
        public List<TipoMedicion> traerTipoMediciones()
        {
            string query = "SELECT idTipoMedicion, tipoMedicion FROM TipoMedicion";
            DataTable tabla = traerTablaDeQuery(query);
            return Utiles.tablaALista(tabla, filaATipoMedicion);
        }

        private DataTable traerTablaDeQuery(string query)
        {
            DataTable tabla = new DataTable();
            adaptador = new MySqlDataAdapter(query, conexion);
            adaptador.Fill(tabla);
            return tabla;
        }

        private TipoMedicion filaATipoMedicion(DataRow fila)
        {
            TipoMedicion tipo = new TipoMedicion()
            {
                Nombre = fila["tipoMedicion"].ToString(),
                Id = Convert.ToByte(fila["idTipoMedicion"])
            };
            return tipo;
        }

        private void guardarMediciones(Medicion medicion)
        {
            instanciarComando("altamedicion");

            cargarParametro("unIdTipoMedicion", MySqlDbType.Byte, medicion.TipoMedicion.Id);
            cargarParametro("unValor", MySqlDbType.Float, medicion.Valor);
            cargarParametro("unaFechaHora", MySqlDbType.DateTime, medicion.FechaHora);
            cargarParametro("idGenerado", MySqlDbType.UInt32, DBNull.Value);
            setearComoSalida("idGenerado");
            ejecutarComando();
            medicion.Id = Convert.ToInt32(comando.Parameters["idGenerado"].Value);
        }
        private void cargarParametro(string nombre, MySqlDbType tipoDb, object valor)
        {
            MySqlParameter parametro = new MySqlParameter(nombre, valor);
            parametro.MySqlDbType = tipoDb;
            comando.Parameters.Add(parametro);
        }
        private void instanciarComando(string nombreComando)
        {
            comando = new MySqlCommand(nombreComando, conexion);
            comando.CommandType = CommandType.StoredProcedure;
        }
        private void setearComoSalida(string nombreParametro)
        {
            comando.Parameters[nombreParametro].Direction = ParameterDirection.Output;
        }

        private void ejecutarComando()
        {
            try
            {
                conexion.Open();
                comando.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                throw e;
            }
            finally
            {
                conexion.Close();
            }
        }
    }
}
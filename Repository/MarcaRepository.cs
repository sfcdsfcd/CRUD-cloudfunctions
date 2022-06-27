using System;
using System.Collections.Generic;
using System.Text;
using Npgsql;
using Newtonsoft.Json;
using desafio.Util;
using desafio.Entidades;

namespace desafio.Repository
{
    public class MarcaRepository
    {
        private string connectionstring = Utils.ConnectionString;
        //private readonly Context _context;
        //public MarcaRepository(Context context)
        //{
        //    _context = context;
        //}
        public List<Marca> GetAll()
        {

            using var conexaoBancoDados = new NpgsqlConnection(connectionstring);
            conexaoBancoDados.Open();

            var sqlSelectMarca = "SELECT * from marca";

            using var cmd = new NpgsqlCommand(sqlSelectMarca, conexaoBancoDados);

            using NpgsqlDataReader rdr = cmd.ExecuteReader();

            IDictionary<int, string> resultTable = new Dictionary<int, string>();

            List<Marca> marcas = new List<Marca>();
            while (rdr.Read())
            {                
                //marcas.Add(new Marca(rdr.GetInt32(0), rdr.GetString(1)));
            }

            conexaoBancoDados.Close();            

            return marcas ;
        }  
        public Marca CadastrarMarca()
        {
            return new Marca() {Descricao="teste" };
        }
    }
}

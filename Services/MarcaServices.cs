using System;
using System.Collections.Generic;
using System.Text;
using desafio.Entidades;
using desafio.Repository;

namespace desafio.Services
{
    public class MarcaServices
    {
        MarcaRepository marcaRepository = new MarcaRepository();


        public List<Marca> GetAll()
        {
            return marcaRepository.GetAll();
        }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace desafio.Entidades
{
    [Table("marca")]
    public class Marca
    {
        public Marca()
        {

        }
        [Display(Name ="id")]
        [Column("id")]
        public int Id { get; set; }

        [Display(Name = "descricao")]
        [Column("descricao")]
        public string Descricao { get; set; }

    }
}

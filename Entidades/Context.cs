using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace desafio.Entidades
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options):base(options)
        {

        }
        public DbSet<Marca> Marcas { get; set; }
    }
}

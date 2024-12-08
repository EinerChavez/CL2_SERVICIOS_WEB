using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PREGUNTA_02_EINER_CHAVEZ.Models;

namespace PREGUNTA_02_EINER_CHAVEZ.Data
{
    public class PREGUNTA_02_EINER_CHAVEZContext : DbContext
    {
        public PREGUNTA_02_EINER_CHAVEZContext (DbContextOptions<PREGUNTA_02_EINER_CHAVEZContext> options)
            : base(options)
        {
        }

        public DbSet<PREGUNTA_02_EINER_CHAVEZ.Models.Registro> Usuarios { get; set; } = default!;
    }
}

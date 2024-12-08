using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PREGUNTA_01_EINER_CHAVEZ.Models;

namespace PREGUNTA_01_EINER_CHAVEZ.Data
{
    public class PREGUNTA_01_EINER_CHAVEZContext : DbContext
    {
        public PREGUNTA_01_EINER_CHAVEZContext (DbContextOptions<PREGUNTA_01_EINER_CHAVEZContext> options)
            : base(options)
        {
        }

        public DbSet<PREGUNTA_01_EINER_CHAVEZ.Models.Videoteca> Videoteca { get; set; } = default!;
    }
}

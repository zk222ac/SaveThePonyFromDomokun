using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebApplicationGame.Model
{
    public class MazeContext : DbContext
    {
        public MazeContext(DbContextOptions<MazeContext> options) : base(options)
        {
            
        }
        public DbSet<Maze> Maze { get; set; }
    }
}

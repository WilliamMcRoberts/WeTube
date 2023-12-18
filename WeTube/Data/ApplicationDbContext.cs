using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WeTube.Models;

namespace WeTube.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<VideoUser> VideoUsers { get; set; }
    public DbSet<VideoModel> Videos { get; set; }
}

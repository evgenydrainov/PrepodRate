using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PrepodRate.Controllers;

namespace PrepodRate.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Professor> Professors { get; set; }
	public DbSet<Review> Reviews { get; set; }
}

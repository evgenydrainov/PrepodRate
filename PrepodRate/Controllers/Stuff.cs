using Microsoft.EntityFrameworkCore;
using PrepodRate.Data;

namespace PrepodRate.Controllers;

public class Stuff
{
    public class ProfessorInfo
    {
        public Professor Prof;
        public List<Review> Reviews;
        public int ReviewCount;
        public float Rating;
        
        public string Name => Prof.Name;
        public string Surname => Prof.Surname;
        public string Patronymic => Prof.Patronymic;
        public string University => Prof.University;
        public int Id => Prof.Id;

        public int RatingPercent => (int)(Rating * 20f);
    }

    public static async Task<ProfessorInfo?> GetProfessorInfo(ApplicationDbContext db, int id)
    {
        var prof = await db.Professors.FirstOrDefaultAsync(x => x.Id == id);
        if (prof is null)
        {
            return null;
        }
        return await GetProfessorInfo(db, prof);
    }
    
    public static async Task<ProfessorInfo?> GetProfessorInfo(ApplicationDbContext db, Professor prof)
    {
        int id = prof.Id;
        
        List<Review> db_reviews = await db.Reviews.ToListAsync();
        List<Review> reviews = new List<Review>();
        float rating = 0;
        foreach (var rev in db_reviews)
        {
            if (rev.ProfessorId == id)
            {
                reviews.Add(rev);
                rating += rev.Rating;
            }
        }
        rating /= (float)reviews.Count;

        ProfessorInfo result = new ProfessorInfo();
        result.Prof = prof;
        result.Reviews = reviews;
        result.ReviewCount = reviews.Count;
        result.Rating = rating;
        return result;
    }

    public static async Task<List<ProfessorInfo>> GetAllProfessors(ApplicationDbContext db)
    {
        var professors = await db.Professors.ToListAsync();
        var result = new List<ProfessorInfo>();
        foreach (var prof in professors)
        {
            var info = await GetProfessorInfo(db, prof);
            if (info is not null)
            {
                result.Add(info);
            }
        }

        return result;
    }
}
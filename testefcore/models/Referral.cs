using Microsoft.EntityFrameworkCore;



// in namespace dhs, add Entity Framework 6, entity class  Referral  and Mothers, one referral to Many mothers

namespace dhs.models
{

    public class Referral
    {
        public int ReferralId { get; set; }
        public string ReferralName { get; set; }
        public virtual ICollection<Mothers> Mothers { get; set; }
    }

// Path: testefcore/models/Mother.cs


// in namespace dhs, add Entity Framework 6, entity class  Mother  and Referral, one mother to One referral
    public class Mothers
    {
        // add FirstName and LastName
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int MothersId { get; set; }
        public int ReferralId { get; set; }
    }
    // Add DbContext with Referral and Mother
    public class ReferralContext : DbContext
    {
        public ReferralContext(DbContextOptions<ReferralContext> options) : base(options)
        {
        }
        public DbSet<Referral> Referrals { get; set; }
        public DbSet<Mothers> Mothers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Referral>().ToTable("Referral");
            modelBuilder.Entity<Mothers>().ToTable("Mothers");
        }
    }
    //add DBInitializer add 5 referrals with 2 mothers each
    public static class DbInitializer
    {
        public static void Initialize(ReferralContext context)
        {

            context.Database.EnsureCreated();
            
            if (context.Referrals.Any())
            {
                return;
            }
            var referrals = new Referral[]
            {
            new Referral{ReferralName="Referral1"},
            new Referral{ReferralName="Referral2"},
            new Referral{ReferralName="Referral3"},
            new Referral{ReferralName="Referral4"},
            new Referral{ReferralName="Referral5"}
            };
            foreach (Referral r in referrals)
            {
                context.Referrals.Add(r);
            }
            context.SaveChanges();
            var mothers = new Mothers[]
            {
            new Mothers{FirstName="Mother1",LastName="Last1", ReferralId = 1 },
            new Mothers{FirstName="Mother2",LastName="Last2", ReferralId =1 },
            new Mothers{FirstName="Mother3",LastName="Last4", ReferralId = 2},
            new Mothers{FirstName="Mother4",LastName="Last6", ReferralId = 2 },
            new Mothers{FirstName="Mother5",LastName="Last4", ReferralId = 3},
            new Mothers{FirstName="Mother6",LastName="Last6", ReferralId =3 },
            new Mothers{FirstName="Mother7",LastName="Last4", ReferralId = 4 },
            new Mothers{FirstName="Mother8",LastName="Last8", ReferralId =4 },
            new Mothers{FirstName="Mother9",LastName="Last9", ReferralId = 5 },
            new Mothers{FirstName="Mother10",LastName="Last10", ReferralId = 5 }
            };
            foreach (Mothers m in mothers)
            {
                context.Mothers.Add(m);
            }
            context.SaveChanges();
        }
    }
}

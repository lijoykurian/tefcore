using dhs.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace testefcore.Controller;

//add refererralController that gets all referalls
[ApiController]
[Route("[controller]")]
public class ReferralController : ControllerBase
{
    private readonly ReferralContext _context;
    public ReferralController(ReferralContext context)
    {
        _context = context;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Referral>>> GetReferrals()
    {
        return await _context.Referrals
            .Where(x=>x.Mothers.Any(z=>z.LastName=="Last4"))
            .Include(y=>y.Mothers)
            .Take(2)
            .ToListAsync();
    }
}
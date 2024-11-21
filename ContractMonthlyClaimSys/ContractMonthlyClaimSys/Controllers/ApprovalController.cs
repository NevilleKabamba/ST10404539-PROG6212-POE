using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContractMonthlyClaimSys.Data;
using ContractMonthlyClaimSys.Models;
using System.Linq;

namespace ContractMonthlyClaimSys.Controllers
{
    public class ApprovalController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public ApprovalController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Display claims with "Pending" status
        public IActionResult ApproveClaims()
        {
            var claims = _dbContext.Claims
                .Where(c => c.Status == "Pending")
                .ToList();

            return View(claims);
        }

        // Approve a claim
        [HttpPost]
        public IActionResult ApproveClaim(int id)
        {
            var claim = _dbContext.Claims.Find(id);
            if (claim != null)
            {
                claim.Status = "Approved";
                _dbContext.SaveChanges();
                TempData["Message"] = "Claim approved successfully.";
            }
            else
            {
                TempData["Error"] = "Claim not found.";
            }

            return RedirectToAction("ApproveClaims");
        }

        // Reject a claim
        [HttpPost]
        public IActionResult RejectClaim(int id)
        {
            var claim = _dbContext.Claims.Find(id);
            if (claim != null)
            {
                claim.Status = "Rejected";
                _dbContext.SaveChanges();
                TempData["Message"] = "Claim rejected successfully.";
            }
            else
            {
                TempData["Error"] = "Claim not found.";
            }

            return RedirectToAction("ApproveClaims");
        }
    }
}

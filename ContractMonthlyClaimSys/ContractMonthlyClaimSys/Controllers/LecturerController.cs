using ContractMonthlyClaimSys.Data;
using ContractMonthlyClaimSys.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Linq;

namespace ContractMonthlyClaimSys.Controllers
{
    public class LecturerController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public LecturerController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // View to submit a claim
        public IActionResult SubmitClaim() => View();

        // POST: SubmitClaim
        [HttpPost]
        public IActionResult SubmitClaim(Claim claim)
        {
            _dbContext.Claims.Add(claim);
            _dbContext.SaveChanges();
            TempData["Message"] = "Claim submitted successfully.";
            return RedirectToAction("ViewClaims");
        }

        // GET: UploadDocuments
        [HttpGet]
        public IActionResult UploadDocuments()
        {
            return View(); // Return the view for uploading documents
        }

        // POST: UploadDocuments
        [HttpPost]
        public IActionResult UploadDocuments(int claimId, IFormFile document)
        {
            if (document != null && document.Length > 0)
            {
                // Validate file type and size
                var allowedExtensions = new[] { ".pdf", ".docx", ".xlsx" };
                var extension = Path.GetExtension(document.FileName).ToLower();

                if (!allowedExtensions.Contains(extension))
                {
                    TempData["Error"] = "Invalid file type.";
                    return View(); // Return the view if the file type is invalid
                }

                if (document.Length > 2 * 1024 * 1024)
                {
                    TempData["Error"] = "File size exceeds 2 MB.";
                    return View(); // Return the view if the file size exceeds 2 MB
                }

                // Ensure the claim exists in the database
                var claim = _dbContext.Claims.FirstOrDefault(c => c.Id == claimId);
                if (claim == null)
                {
                    TempData["Error"] = "Claim not found.";
                    return View(); // Return if the claim doesn't exist
                }

                // Define the path to save the file
                var uploadsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

                // Ensure the uploads folder exists
                if (!Directory.Exists(uploadsFolderPath))
                {
                    Directory.CreateDirectory(uploadsFolderPath);
                }

                var filePath = Path.Combine(uploadsFolderPath, Path.GetFileName(document.FileName));
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    document.CopyTo(stream);
                }

                // Create and save the uploaded file record
                var uploadedFile = new UploadedFile
                {
                    FileName = document.FileName,
                    FilePath = Path.Combine("uploads", Path.GetFileName(document.FileName)),
                    ClaimId = claimId
                };

                _dbContext.UploadedFiles.Add(uploadedFile);
                _dbContext.SaveChanges();

                TempData["Message"] = "File uploaded successfully.";
                return RedirectToAction("ViewClaims"); // Redirect to ViewClaims after successful upload
            }

            TempData["Error"] = "No file selected or file is invalid.";
            return View(); // Return the view if the file is not valid
        }

        // View all claims submitted by the lecturer
        public IActionResult ViewClaims()
        {
            var claims = _dbContext.Claims
                .Include(c => c.UploadedFiles)
                .ToList();

            foreach (var claim in claims)
            {
                claim.UploadedFiles ??= new List<UploadedFile>();
            }

            return View(claims);
        }

        // View to check the status of claims
        public IActionResult CheckStatus()
        {
            var claims = _dbContext.Claims.ToList();
            return View(claims);
        }
    }
}

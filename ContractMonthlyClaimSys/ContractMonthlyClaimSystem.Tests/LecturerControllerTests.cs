using System;
using System.Collections.Generic;
using ContractMonthlyClaimSys.Controllers;
using ContractMonthlyClaimSys.Data;
using ContractMonthlyClaimSys.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.IO;
using System.Linq;
using Xunit;

public class LecturerControllerTests
{
    private readonly ApplicationDbContext _dbContext;

    public LecturerControllerTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;
        _dbContext = new ApplicationDbContext(options);

        // Seed the database
        _dbContext.Claims.Add(new Claim { Id = 1, LecturerName = "Test Lecturer", Status = "Pending" });
        _dbContext.SaveChanges();
    }

    [Fact]
    public void SubmitClaim_Post_ValidClaim_ShouldRedirectToViewClaims()
    {
        // Arrange
        var controller = new LecturerController(_dbContext);
        var claim = new Claim { Id = 2, LecturerName = "New Lecturer" };

        // Act
        var result = controller.SubmitClaim(claim) as RedirectToActionResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal("ViewClaims", result.ActionName);
        Assert.Single(_dbContext.Claims.Where(c => c.Id == 2));
    }

    [Fact]
    public void UploadDocuments_ValidFile_ShouldSaveFileAndRedirect()
    {
        // Arrange
        var controller = new LecturerController(_dbContext);
        var mockFile = new Mock<IFormFile>();
        var content = "Test file content";
        var fileName = "test.pdf";
        var claimId = 1;

        var ms = new MemoryStream();
        var writer = new StreamWriter(ms);
        writer.Write(content);
        writer.Flush();
        ms.Position = 0;

        mockFile.Setup(f => f.OpenReadStream()).Returns(ms);
        mockFile.Setup(f => f.FileName).Returns(fileName);
        mockFile.Setup(f => f.Length).Returns(ms.Length);

        // Act
        var result = controller.UploadDocuments(claimId, mockFile.Object) as RedirectToActionResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal("ViewClaims", result.ActionName);
        Assert.Single(_dbContext.UploadedFiles.Where(f => f.ClaimId == claimId && f.FileName == fileName));
    }

    [Fact]
    public void ViewClaims_ShouldReturnClaimsWithUploadedFiles()
    {
        // Arrange
        var controller = new LecturerController(_dbContext);

        // Act
        var result = controller.ViewClaims() as ViewResult;
        var model = result?.Model as List<Claim>;

        // Assert
        Assert.NotNull(model);
        Assert.Single(model);
        Assert.All(model, claim => Assert.NotNull(claim.UploadedFiles));
    }
}

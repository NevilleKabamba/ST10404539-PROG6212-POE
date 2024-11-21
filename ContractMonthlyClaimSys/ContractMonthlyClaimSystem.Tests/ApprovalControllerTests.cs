using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;
using ContractMonthlyClaimSys.Controllers;
using ContractMonthlyClaimSys.Data;
using ContractMonthlyClaimSys.Models;
using Xunit;

public class ApprovalControllerTests
{
    private readonly Mock<ApplicationDbContext> _mockDbContext;
    private readonly ApprovalController _controller;

    public ApprovalControllerTests()
    {
        // Mocking the DbContext
        var mockClaims = new List<Claim>
        {
            new Claim { Id = 1, LecturerName = "John Doe", Status = "Pending" },
            new Claim { Id = 2, LecturerName = "Jane Smith", Status = "Approved" },
            new Claim { Id = 3, LecturerName = "Robert Brown", Status = "Pending" }
        }.AsQueryable();

        var mockSet = new Mock<DbSet<Claim>>();
        mockSet.As<IQueryable<Claim>>().Setup(m => m.Provider).Returns(mockClaims.Provider);
        mockSet.As<IQueryable<Claim>>().Setup(m => m.Expression).Returns(mockClaims.Expression);
        mockSet.As<IQueryable<Claim>>().Setup(m => m.ElementType).Returns(mockClaims.ElementType);
        mockSet.As<IQueryable<Claim>>().Setup(m => m.GetEnumerator()).Returns(mockClaims.GetEnumerator());

        _mockDbContext = new Mock<ApplicationDbContext>();
        _mockDbContext.Setup(db => db.Claims).Returns(mockSet.Object);

        _controller = new ApprovalController(_mockDbContext.Object);
    }

    [Fact]
    public void ApproveClaims_ReturnsViewResult_WithPendingClaims()
    {
        // Act
        var result = _controller.ApproveClaims() as ViewResult;
        var model = result?.Model as List<Claim>;

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(model);
        Assert.Equal(2, model.Count); 
        Assert.All(model, claim => Assert.Equal("Pending", claim.Status));
    }

    [Fact]
    public void ApproveClaim_ValidClaim_ShouldUpdateStatusAndRedirect()
    {
        // Arrange
        var claimId = 1;

        // Act
        var result = _controller.ApproveClaim(claimId) as RedirectToActionResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal("ApproveClaims", result.ActionName);
        _mockDbContext.Verify(db => db.SaveChanges(), Moq.Times.Once()); 
        _mockDbContext.Verify(db => db.Claims.Find(claimId), Moq.Times.Once()); 
    }

    [Fact]
    public void ApproveClaim_InvalidClaim_ShouldRedirectWithErrorMessage()
    {
        // Arrange
        var claimId = 999; 

        // Act
        var result = _controller.ApproveClaim(claimId) as RedirectToActionResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal("ApproveClaims", result.ActionName);
        Assert.Equal("Claim not found.", _controller.TempData["Error"]);
    }

    [Fact]
    public void RejectClaim_ValidClaim_ShouldUpdateStatusAndRedirect()
    {
        // Arrange
        var claimId = 1;

        // Act
        var result = _controller.RejectClaim(claimId) as RedirectToActionResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal("ApproveClaims", result.ActionName);
        _mockDbContext.Verify(db => db.SaveChanges(), Moq.Times.Once()); 
        _mockDbContext.Verify(db => db.Claims.Find(claimId), Moq.Times.Once()); 
    }

    [Fact]
    public void RejectClaim_InvalidClaim_ShouldRedirectWithErrorMessage()
    {
        // Arrange
        var claimId = 999; 

        // Act
        var result = _controller.RejectClaim(claimId) as RedirectToActionResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal("ApproveClaims", result.ActionName);
        Assert.Equal("Claim not found.", _controller.TempData["Error"]);
    }
}

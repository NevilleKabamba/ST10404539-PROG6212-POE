namespace ContractMonthlyClaimSys.Models
{
    public class UploadedFile
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }

        // Foreign key to the related claim
        public int ClaimId { get; set; }
        public Claim Claim { get; set; }
    }
}

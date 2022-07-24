namespace Film_Management_System_API.DataModels
{
    public class FilmDTO
    {
       
        public string Description { get; set; } = null!;
        public string Title { get; set; } = null!;
        public decimal? LanguageId { get; set; }
        public decimal? OriginalLanguageId { get; set; }
        public decimal Length { get; set; }
        public decimal ReplacementCost { get; set; }
        public decimal Rating { get; set; }
        public string SpecialFeatures { get; set; } = null!;
        public decimal? ActorId { get; set; }
        public decimal? CategoryId { get; set; }
        public string? ReleaseYear { get; set; }
        public string? RentalDuration { get; set; }
    }
}

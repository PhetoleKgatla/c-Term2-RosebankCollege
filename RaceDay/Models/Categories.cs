namespace RaceDay.Models
{
    public class Categories
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; } = "";

        public string? Age { get; set; }

        public string? Distance { get; set; }

        public int OrganiserId { get; set; }

        public int EventId { get; set; }
    }
}

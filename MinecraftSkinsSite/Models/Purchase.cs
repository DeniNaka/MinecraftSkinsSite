namespace MinecraftSkinsSite.Models
{
    public class Purchase
    {
        public int Id { get; set; }
        public int SkinId { get; set; }
        public string? SkinName { get; set; } 
        public decimal FinalPriceUsd { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

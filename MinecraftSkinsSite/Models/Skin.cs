namespace MinecraftSkinsSite.Models
{
    public class Skin
    {
        public int Id { get; set; }
        public string? Name { get; set; } 
        public decimal BasePriceUsd { get; set; }
        public bool IsAvailable { get; set; }
    }
}

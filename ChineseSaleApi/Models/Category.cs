namespace ChineseSaleApi.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Gift> Gifts { get; set; } = new List<Gift>();
    }
}

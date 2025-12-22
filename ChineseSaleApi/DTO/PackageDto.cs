namespace ChineseSaleApi.DTO
{
    public class CreatePackageDto
    {
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public int TicketsCount { get; set; }
    }
    public class PackageDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public int TicketsCount { get; set; }
    }

}

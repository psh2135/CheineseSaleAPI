using ChineseSaleApi.Models;

public enum PurchaseStatus { Draft = 0, Completed = 1, Cancelled = 2 }

public class Purchase
{
    public int Id { get; set; }
    public int BuyerId { get; set; }
    public User Buyer { get; set; } = null!;
    public PurchaseStatus Status { get; set; } = PurchaseStatus.Draft;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // רשימת ה-IDs שנמצאים כרגע בסל (טיוטה)
    public List<int> GiftsAtCart { get; set; } = new List<int>();

    // כרטיסים שנוצרו לאחר רכישה סופית
    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
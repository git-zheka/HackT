namespace Hack.Models
{
    public class HistoryCryptoEvent
    {
        public Guid Id { get; set; }

        public string Description { get; set; }
        public float Amount { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }
    }
}

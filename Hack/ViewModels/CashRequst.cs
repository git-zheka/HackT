using Hack.Models;

namespace Hack.ViewModels
{
    public class CashRequst
    {
        public float Balance { get; set; }
        public List<HistoryEvent> CashEvents { get; set; }

    }
}

using Hack.Models;

namespace Hack.ViewModels
{
    public class CryptoRequst
    {
        public List<UserCryptoState> Balance { get; set; }
        public List<HistoryCryptoEvent> HistoryCryptoEvents { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Hack.Models
{
    public class UserCryptoState
    {
        [Key]
        public int IdCoin { get; set; }

        public int IdCoinIT { get; set; }

        public float Balance { get; set; }
    }
}

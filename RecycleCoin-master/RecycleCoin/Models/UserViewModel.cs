using System.Security.Cryptography.X509Certificates;

namespace RecycleCoin.Models;

public class UserViewModel
{
    public string PublicKey { get; set; }
    public decimal Amount { get; set; }

}
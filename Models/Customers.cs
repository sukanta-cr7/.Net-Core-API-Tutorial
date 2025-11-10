namespace Dot_Net_Core_Tutorial.Models
{
    public class Customers
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; } 
        public string Phone { get; set; } 
        public string Address { get; set; } = string.Empty;

    }
}

using System.ComponentModel.DataAnnotations;

namespace HandlebarPractice.Models
{
    public class Customer
    {

        public long SCustId { get; set; }
        
        public long? SCustALId { get; set; }
        [Display(Name = "Customer Name")]
        public string? SCustName { get; set; }
        [Display(Name = "Phone")]
        public string? SCustPhone { get; set; }
        [Display(Name = "Fax")]
        public string? SCustFax { get; set; }
        [Display(Name = "Email")]
        public string? SCustEmail { get; set; }
        [Display(Name = "Address Line 1")]
        public string? SCustAddress1 { get; set; }
        [Display(Name = "Address Line 2")]
        public string? SCustAddress2 { get; set; }
        [Display(Name = "City")]
        public string? SCustCity { get; set; }
        [Display(Name = "State")]
        public string? SCustState { get; set; }
        [Display(Name = "Zip")]
        public string? SCustZip { get; set; }
        [Display(Name = "Shipping Address")]
        public string? SShippingAddress { get; set; }
        [Display(Name = "Comments")]
        public string? SCustComments { get; set; }

        public long? SCustSetupBy { get; set; }
        
        public DateTime? SCustSetupDate { get; set; }
        
        public long? SCustUpdateBy { get; set; }
        
        public DateTime? SCustUpdateDate { get; set; }
        
        public long? SCustRemoveBy { get; set; }
        
        public DateTime? SCustRemoveDate { get; set; }
    }
    public class CustomerVM
    {
        public Customer Customer { get; set; } = new Customer();
        public List<Customer> CustomerList { get; set; } = new List<Customer>();
        public DateTime GeneratedOn { get; set; }
    }
}

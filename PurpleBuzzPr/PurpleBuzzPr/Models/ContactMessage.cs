using PurpleBuzzPr.Models.Base;

namespace PurpleBuzzPr.Models;

public class ContactMessage : BaseAuditableEntity
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string CompanyName { get; set; }
    public string Subject { get; set; }
    public string Message { get; set; }
}

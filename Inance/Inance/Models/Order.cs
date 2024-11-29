using Inance.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inance.Models;

public class Order : BaseAuditableEntity
{
    public string ClientName { get; set; }
    public string ClientSurname { get; set; }
    public string ClientPhoneNumber { get; set; }
    public string ClientEmail { get; set; }
    public int ServiceId { get; set; }

    [NotMapped]
    public Service Service { get; set; }
    public int MasterId { get; set; }

    [NotMapped]
    public Master Master { get; set; }
    public string Problem { get; set; }
    public bool IsActive { get; set; }
}

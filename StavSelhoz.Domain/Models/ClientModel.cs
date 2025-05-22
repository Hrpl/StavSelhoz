using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StavSelhoz.Domain.Models;

public class ClientModel
{
    [SqlKata.Column("company_name")]
    public string CompanyName { get; set; }
    [SqlKata.Column("concact_people")]
    public string ConcactPeople { get; set; }
    [SqlKata.Column("phone_number")]
    public string PhoneNumber { get; set; }
    [SqlKata.Column("email")]
    public string Email { get; set; }
    [SqlKata.Column("address")]
    public string Address { get; set; }
    [SqlKata.Column("created_at")]
    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
    [SqlKata.Column("updated_at")]
    public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
    [SqlKata.Column("is_deleted")]
    public bool IsDeleted { get; set; } = false;
}


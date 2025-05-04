using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StavSelhoz.Domain.Models;

public class ProviderModel
{
    [SqlKata.Column("company_name")]
    public string CompanyName { get; set; }
    [SqlKata.Column("contact_people")]
    public string ContactPeople { get; set; }
    [SqlKata.Column("сonditions")]
    public string Сonditions { get; set; }
    [SqlKata.Column("created_at")]
    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
    [SqlKata.Column("updated_at")]
    public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
    [SqlKata.Column("is_deleted")]
    public bool IsDeleted { get; set; } = false;
}

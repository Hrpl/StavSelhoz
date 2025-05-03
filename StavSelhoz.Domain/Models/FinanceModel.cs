using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StavSelhoz.Domain.Models;

public class FinanceModel
{
    [SqlKata.Column("type")]
    public string Type { get; set; }
    [SqlKata.Column("summary")]
    public double Summary { get; set; }
    [SqlKata.Column("responsible_person")]
    public string ResponsiblePerson { get; set; }
    [SqlKata.Column("created_at")]
    public DateTime CreatedAt { get; set; }
    [SqlKata.Column("updated_at")]
    public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
    [SqlKata.Column("is_deleted")]
    public bool IsDeleted { get; set; } = false;
}

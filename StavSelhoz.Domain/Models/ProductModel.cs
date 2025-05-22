using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StavSelhoz.Domain.Models;

public class ProductModel
{
    [SqlKata.Column("name")]
    public string Name { get; set; }
    [SqlKata.Column("code")]
    public string Code { get; set; }
    [SqlKata.Column("measure")]
    public string Measure { get; set; }
    [SqlKata.Column("price")]
    public double Price { get; set; }
    [SqlKata.Column("in_storage")]
    public int InStorage { get; set; }
    [SqlKata.Column("created_at")]
    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
    [SqlKata.Column("updated_at")]
    public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
    [SqlKata.Column("is_deleted")]
    public bool IsDeleted { get; set; } = false;

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StavSelhoz.Domain.Entities;

public class ProviderEntity : BaseEntity
{
    public string CompanyName { get; set; }
    public string ContactPeople { get; set; }
    public string Сonditions { get; set; }
}

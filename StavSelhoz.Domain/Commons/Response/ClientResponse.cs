﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StavSelhoz.Domain.Commons.Response;

public class ClientResponse
{
    public int Id { get; set; }
    public string CompanyName { get; set; }
    public string ConcactPeople { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
}

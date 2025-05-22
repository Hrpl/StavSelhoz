﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StavSelhoz.Domain.Models;

public class UserModel
{
    [SqlKata.Column("name")]
    public string Name { get; set; }
    [SqlKata.Column("surname")]
    public string Surname { get; set; }
    [SqlKata.Column("patronymic")]
    public string Patronymic { get; set; }

    [SqlKata.Column("email")]
    public string Email { get; set; }
    [SqlKata.Column("password")]
    public string Password { get; set; }
    [SqlKata.Column("salt")]
    public string Salt { get; set; }
    [SqlKata.Column("role_id")]
    public int RoleId { get; set; } = 2;
    [SqlKata.Column("deportament_id")]
    public int DeportamentId { get; set; }
    [SqlKata.Column("created_at")]
    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
    [SqlKata.Column("updated_at")]
    public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
    [SqlKata.Column("is_deleted")]
    public bool IsDeleted { get; set; } = false;
}

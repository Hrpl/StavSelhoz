﻿using Npgsql;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StavSelhoz.Infrastructure.Services.Interfaces;

public interface IDbConnectionManager
{
    public QueryFactory PostgresQueryFactory { get; }
    public NpgsqlConnection PostgresDbConnection { get; }
}

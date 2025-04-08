using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SharedKernel.Infrastructure.Database;
public static class TableNames
{
    public const string Users = "users";
    public const string Roles = "roles";
    public const string Permissions = "permissions";
}

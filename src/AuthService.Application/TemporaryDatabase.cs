using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthService.Application.Abstractions.Data;
using AuthService.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Application;
public static class TemporaryDatabase 
{
    private static readonly List<User> _users = new();
    public static List<User> Users { get; } = _users;

    public static void AddUser(User user)
    {
        _users.Add(user);
    }

}

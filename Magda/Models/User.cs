using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Magda.Models
{
    public class User : IdentityUser
    {
        public enum Role
        {
            Manager,
            Worker,
            Client
        }
        public Role userRoleType { get; set; }
        public User()
        {
        }
    }
}
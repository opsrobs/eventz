﻿using eventz.Enums;
using System;

namespace eventz.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? CPF { get; set; }
        public string? CNPJ { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public RolesEnum Roles { get; set; }

        public User()
        {
            Id = Guid.NewGuid();
            Roles = RolesEnum.User;
        }

        public User(string username, string email, string password, string firstName, string lastName, DateTime dateOfBirth, string? cpf, string? cnpj, RolesEnum roles)
        {
            Id = Guid.NewGuid();
            Username = username;
            Email = email;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            CPF = cpf;
            CNPJ = cnpj;
            CreatedAt = DateTime.Now;
            UpdatedAt = null;
            Roles = roles;
        }
    }
}

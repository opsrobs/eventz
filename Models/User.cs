using eventz.Enums;

namespace eventz.Models
{
    
   

    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string? Nome { get; set; }
        public string? Sobrenome { get; set; }
        public DateTime? DataDeNascimento { get; set; }
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

        public User(string username, string email, string senha, string nome, string sobrenome, DateTime dataDeNascimento, string? cpf, string? cnpj, RolesEnum roles)
        {
            Id = Guid.NewGuid(); 
            Username = username;
            Email = email;
            Senha = senha;
            Nome = nome;
            Sobrenome = sobrenome;
            DataDeNascimento = dataDeNascimento;
            CPF = cpf;
            CNPJ = cnpj;
            CreatedAt = DateTime.Now;
            UpdatedAt = null; 
            Roles = roles;
        }
    }


}

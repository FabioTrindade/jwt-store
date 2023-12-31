using JwtStore.Core.Contexts.AccountContext.ValueObjects;
using JwtStore.Core.Contexts.SharedContext.Entities;

namespace JwtStore.Core.Contexts.AccountContext.Entities;

public class User : Entity
{
    protected User()
    { 
    }

    public User(string name
        , Email email
        , Password password)
    {
        Name = name;
        Email = email;
        Password = password;
    }

    public User(Email email, string? password = null)
    {
        Email = email;
        Password = new Password(password);
    }

    public string Name { get; private set; } = null!;

    public Email Email { get;private set; } = null!;

    public Password Password { get; private set; } = null!;

    public string Image { get; private set; } = string.Empty;

    public List<Role> Roles { get; set; } = new();
    
    public void UpdatePassword(string plainTextPassword, string code)
    {
        if (!string.Equals(code.Trim(), Password.ResetCode.Trim(), StringComparison.CurrentCultureIgnoreCase))
            throw new Exception("Código de restauração inválido");
            
        Password = new Password(plainTextPassword);
    }

    public void UpdateEmail(Email email)
        => Email = email;

    public void ChangePassword(string plainTextPassword)
        => Password = new Password(plainTextPassword);
    
}
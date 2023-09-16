namespace JwtStore.Core.AccountContext.UseCases.Create;

public record Request(
    string Name,
    string Email,
    string Password
);
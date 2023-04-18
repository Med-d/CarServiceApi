using System.Data;
using System.Security.Authentication;
using CarServices.Api.Core.Exceptions;
using CarServices.Api.Core.UnitOfWork;
using CarServices.Api.Core.UnitOfWork.Models;
using CarServices.Api.Models;
using CarServices.Api.Models.Tokens;
using Microsoft.Extensions.Primitives;
using OperationResult;

namespace CarServices.Api.Core.AuthServices;

public class IdentityService
{
    private readonly IUnitOfWork unitOfWork;

    public IdentityService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public async Task<bool> RegisterIdentity(
            string name,
            string surname,
            string login,
            Token token,
            Role role
    )
    {
        var dbItem = new User()
        {
            Name = name,
            Surname = surname,
            Login = login,
            Token = token.Value,
            Role = role
        };

        var userRepository = unitOfWork.UserRepository;
        userRepository.Add(dbItem);

        return await unitOfWork.Complete();
    }

    public async Task<Result<User, Exception>> GetIdentity(Token token)
    {
        var identity = await unitOfWork.UserRepository.Find(user => user.Token.Equals(token!.Value));

        await unitOfWork.Complete();
        
        return identity is null
            ? Result<User, Exception>.Fail<User>(new UserNotFoundException())
            : Result<User, Exception>.Success(identity);
    }

    
}
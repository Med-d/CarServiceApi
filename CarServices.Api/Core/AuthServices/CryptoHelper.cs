using System.Security.Cryptography;
using System.Text;
using CarServices.Api.Models;
using CarServices.Api.Models.Requests;
using CarServices.Api.Models.Tokens;

namespace CarServices.Api.Core.AuthServices;

public class CryptoHelper
{
    private readonly SHA256 sha256;
    
    public CryptoHelper()
    {
        sha256 = SHA256.Create();
    }

    public Token GetToken(string login, string password)
    {
        
        var sourceBytes = Encoding.ASCII.GetBytes(login + password);
        var token = sha256.ComputeHash(sourceBytes);
        return new Token("Bearer", BitConverter.ToString(token).Replace("-", string.Empty));
    }
}
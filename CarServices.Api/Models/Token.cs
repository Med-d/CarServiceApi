using Microsoft.Extensions.Primitives;

namespace CarServices.Api.Models;

public class Token
{
    public string Value { get; }
    public string Type { get; }

    public Token(string type, string value)
    {
        Type = type;
        Value = value;
    }

    public Token(string? headerValue)
    {
        var splitedHeader = headerValue?.Split(' ') ?? throw new NullReferenceException("Wrong auth header values");
        (Type, Value) = (splitedHeader[0], splitedHeader[1]);
    }

    public static Token FromHeaders(IHeaderDictionary headers)
    {
        var authHeaders = headers.Authorization;
        if (!ValidateHeaders(authHeaders, out var token))
            throw new BadHttpRequestException("Bad auth header token");
        return token!;
    }
    
    private static bool ValidateHeaders(StringValues authHeader, out Token? token)
    {
        if (authHeader.Count != 1)
        {
            token = null;
            return false;
        }
        
        token = new Token(authHeader.First());

        return token.Type == "Bearer";
    }
}
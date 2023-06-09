﻿using System.Text.Json;

namespace CarServices.Api.Models.Notifications;

public record CreationOrder(
    string Title,
    string CustomerContact,
    string Description
) : IEvent
{
    public bool ValidateContent()
    {
        return true;
    }

    public string GetJson() => JsonSerializer.Serialize(this);
}
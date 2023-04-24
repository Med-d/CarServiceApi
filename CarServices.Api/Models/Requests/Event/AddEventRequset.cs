using CarServices.Api.Models.Notifications;
using Microsoft.AspNetCore.Mvc;

namespace CarServices.Api.Models.Requests.Event;

public record AddEventRequset(
    object Data,
    string Channel,
    Guid ParrentMessageId
);
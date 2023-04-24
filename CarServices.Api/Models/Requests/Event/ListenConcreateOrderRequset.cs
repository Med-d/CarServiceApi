using Microsoft.AspNetCore.Mvc;

namespace CarServices.Api.Models.Requests.Event;

public record ListenConcreateOrderRequset(
    [FromRoute] int OrderId
);
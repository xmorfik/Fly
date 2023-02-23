﻿namespace Fly.Shared.DataTransferObjects;

public class CreateManagerDto
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string UserName { get; init; }
    public string Password { get; init; }
    public string Email { get; init; }
    public string PhoneNumber { get; init; }
    public string AirlineId { get; init; }
}

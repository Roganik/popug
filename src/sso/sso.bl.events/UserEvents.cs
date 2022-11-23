namespace sso.bl.events;

public record UserCreated(
    Guid Id,
    string Login,
    string Name
);

public record UserRoleChanged(
    Guid Id,
    string Role
);

public record UserUpdated(
    Guid Id,
    string Login,
    string Name
);
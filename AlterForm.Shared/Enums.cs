namespace AlterForm;

public enum ContentVisibility
{
    Public = 0,
    Unlisted = 1,
    FollowersOnly = 2,
    Private = 3,
}

public enum MaturityRating
{
    General = 0,
    Mature = 1,
    Adult = 2,
    Extreme = 3,
}

public enum ModerationState
{
    Ok = 0,
    PendingReview = 1,
    Quarantined = 2,
    Removed = 3,
}

public enum TagKind
{
    General = 0,
    Species = 1,
    Character = 2,
    Creator = 3,
    Franchise = 4,
    Medium = 5,
    Meta = 6,
    Warning = 7,
}

public enum TagSource
{
    Author = 0,
    Moderator = 1,
    Implication = 2,
    System = 3,
}
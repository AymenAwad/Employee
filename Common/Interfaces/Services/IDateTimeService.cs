namespace Shared.Interfaces.Services
{
    using System;

    public interface IDateTimeService
    {
        DateTime NowUtc { get; }
    }
}

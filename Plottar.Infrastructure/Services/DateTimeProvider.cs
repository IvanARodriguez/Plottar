namespace Plottar.Infrastructure.Services;

using System;
using Plottar.Application.Common.Interfaces.Services;

public class DateTimeProvider : IDateTimeProvider
{
  public DateTime UtcNow => DateTime.UtcNow;
}

using Domain.Interfaces;
using Domain.Models;

namespace External.Providers;

public class EnvironmentDataApi : IEnvironmentDataApi
{
    public RainfallResponse GetMeasure(string stationId, int count)
    {     
        var readings = new List<Item>
        {
            new Item { DateTime = DateTime.UtcNow, Value = 12.34 },
            new Item { DateTime = DateTime.UtcNow.AddDays(-1), Value = 5.67 },
            new Item { DateTime = DateTime.UtcNow.AddDays(-2), Value = 8.91 },
            new Item { DateTime = DateTime.UtcNow.AddDays(-3), Value = 7.12 },
            new Item { DateTime = DateTime.UtcNow.AddDays(-4), Value = 3.45 },
            new Item { DateTime = DateTime.UtcNow.AddDays(-5), Value = 6.78 },
            new Item { DateTime = DateTime.UtcNow.AddDays(-6), Value = 9.01 },
            new Item { DateTime = DateTime.UtcNow.AddDays(-7), Value = 10.23 },
            new Item { DateTime = DateTime.UtcNow.AddDays(-8), Value = 11.45 },
            new Item { DateTime = DateTime.UtcNow.AddDays(-9), Value = 2.34 },
            new Item { DateTime = DateTime.UtcNow.AddDays(-10), Value = 4.56 },
            new Item { DateTime = DateTime.UtcNow.AddDays(-11), Value = 5.78 },
            new Item { DateTime = DateTime.UtcNow.AddDays(-12), Value = 7.89 },
            new Item { DateTime = DateTime.UtcNow.AddDays(-13), Value = 8.90 },
            new Item { DateTime = DateTime.UtcNow.AddDays(-14), Value = 1.23 },
            new Item { DateTime = DateTime.UtcNow.AddDays(-15), Value = 3.45 },
            new Item { DateTime = DateTime.UtcNow.AddDays(-16), Value = 6.78 },
            new Item { DateTime = DateTime.UtcNow.AddDays(-17), Value = 9.12 },
            new Item { DateTime = DateTime.UtcNow.AddDays(-18), Value = 4.34 },
            new Item { DateTime = DateTime.UtcNow.AddDays(-19), Value = 2.56 },
            new Item { DateTime = DateTime.UtcNow.AddDays(-20), Value = 5.67 },
            new Item { DateTime = DateTime.UtcNow.AddDays(-21), Value = 8.78 },
            new Item { DateTime = DateTime.UtcNow.AddDays(-22), Value = 3.89 },
            new Item { DateTime = DateTime.UtcNow.AddDays(-23), Value = 7.01 },
            new Item { DateTime = DateTime.UtcNow.AddDays(-24), Value = 6.12 },
            new Item { DateTime = DateTime.UtcNow.AddDays(-25), Value = 9.34 },
            new Item { DateTime = DateTime.UtcNow.AddDays(-26), Value = 10.56 },
            new Item { DateTime = DateTime.UtcNow.AddDays(-27), Value = 4.78 },
            new Item { DateTime = DateTime.UtcNow.AddDays(-28), Value = 2.90 },
            new Item { DateTime = DateTime.UtcNow.AddDays(-29), Value = 5.12 },
            new Item { DateTime = DateTime.UtcNow.AddDays(-30), Value = 6.34 }
        };

        readings = readings.Take(count).ToList();
        return new RainfallResponse() { Items = readings };
    }
}
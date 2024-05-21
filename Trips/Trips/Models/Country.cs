using System;
using System.Collections.Generic;

namespace Trips.Models;

public partial class Country
{
    public int IdCountry { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<CountryTrip> CountryTrips { get; set; } = new List<CountryTrip>();
}

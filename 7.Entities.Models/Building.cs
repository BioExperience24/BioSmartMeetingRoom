using System;
using System.Collections.Generic;

namespace _7.Entities.Models;

public partial class Building
{
    public int Id { get; set; }

    public int Generate { get; set; }

    public string? Name { get; set; }

    public string? Image { get; set; }

    public string? Description { get; set; }

    public string? Timezone { get; set; }

    public string? DetailAddress { get; set; }

    public string? GoogleMap { get; set; }

    public string? Koordinate { get; set; }

    public string? IsDeleted { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? UpdatedBy { get; set; }
}

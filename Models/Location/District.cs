using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MyWebsite.Models.Location;

public partial class District
{
    [Key]
    public string DistrictId { get; set; } = null!;

    public string DistrictName { get; set; } = null!;

    public string? StateId { get; set; }

    public virtual State? State { get; set; }

    [NotMapped]
    public List<SelectListItem>? StatesSelectList { get; set; }

    public virtual ICollection<Ward> Wards { get; set; } = new List<Ward>();
}

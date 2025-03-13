using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWebsite.Models.Location;

public partial class Ward
{
    [Key]
    public string WardId { get; set; } = null!;

    public string WardName { get; set; } = null!;

    public string? DistrictId { get; set; }

    public virtual District? District { get; set; }

    [NotMapped]
    public List<SelectListItem>? DistrictSelectedList { get; set; }
}

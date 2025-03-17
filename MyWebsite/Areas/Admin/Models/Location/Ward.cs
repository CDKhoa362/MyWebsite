using Microsoft.AspNetCore.Mvc.Rendering;
using MyWebsite.Models.MyInfor;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyWebsite.Areas.Admin.Models.Location;

namespace MyWebsite.Areas.Admin.Models.Location;

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

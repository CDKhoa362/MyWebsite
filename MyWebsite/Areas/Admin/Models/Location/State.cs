using Microsoft.AspNetCore.Mvc.Rendering;
using MyWebsite.Models.MyInfor;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using MyWebsite.Areas.Admin.Models.Location;

namespace MyWebsite.Areas.Admin.Models.Location;
public partial class State
{
    public string StateId { get; set; } = null!;

    public string StateName { get; set; } = null!;

    public string? CountryId { get; set; }

    public virtual Country? Country { get; set; }

    public virtual ICollection<District> Districts { get; set; } = new List<District>();

    [NotMapped]
    public List<SelectListItem>? CountriesSelectedList { get; set; }
}

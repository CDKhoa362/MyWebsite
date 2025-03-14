using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyWebsite.Models.Location;
using MyWebsite.Models.MyInfor;

namespace MyWebsite.Models.Location;
public partial class Country
{
    [Key]
    public string CountryId { get; set; } = null!;

    public string CountryName { get; set; } = null!;

    public string Alp2 { get; set; } = null!;

    public string Alp3 { get; set; } = null!;

    public string PhoneCode { get; set; } = null!;

    public virtual ICollection<State> States { get; set; } = new List<State>();


    [NotMapped]
    public List<SelectListItem>? CountriesSelectedList { get; set; }

}

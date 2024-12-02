using Inance.DTOs.MasterDTOs;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Inance.Areas.Admin.ViewModels;

public class MasterVM
{
    public GetMasterDto? Form { get; set; }
    public SelectList Services { get; set; }
}

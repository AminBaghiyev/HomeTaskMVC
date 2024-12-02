using Inance.DTOs.OrderDTOs;
using Inance.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Inance.Areas.Admin.ViewModels;

public class OrderVM
{
    public SelectList Services { get; set; }
    public Master? Master { get; set; }
    public FormOrderDto? Form { get; set; }
}

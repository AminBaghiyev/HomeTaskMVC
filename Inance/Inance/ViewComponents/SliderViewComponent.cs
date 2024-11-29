using Microsoft.AspNetCore.Mvc;

namespace Inance.ViewComponents;

public class SliderViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View();
    }
}

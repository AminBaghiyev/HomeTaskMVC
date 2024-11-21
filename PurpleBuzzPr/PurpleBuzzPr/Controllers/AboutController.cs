using Microsoft.AspNetCore.Mvc;
using PurpleBuzzPr.DAL;
using PurpleBuzzPr.Models;
using PurpleBuzzPr.ViewsModel.About;

namespace PurpleBuzzPr.Controllers;

public class AboutController : Controller
{
    private readonly AppDBContext _db;

    public AboutController(AppDBContext db)
    {
        _db = db;
    }

    public IActionResult Index()
    {
        IEnumerable<TeamMember> teamMembers = _db.TeamMembers;

        AboutVM VM = new()
        {
            TeamMembers = teamMembers
        };

        return View(VM);
    }

    public void AddTeamMember()
    {
        IEnumerable<TeamMember> teamMembers = [
            new TeamMember()
            {
                Name = "John",
                Surname = "Doe",
                JobTitle = "Business Development",
                ImagePath = "team-01.jpg"
            },
            new TeamMember()
            {
                Name = "Jane",
                Surname = "Doe",
                JobTitle = "Media Development",
                ImagePath = "team-02.jpg"
            },
            new TeamMember()
            {
                Name = "Sam",
                Surname = "",
                JobTitle = "Developer",
                ImagePath = "team-03.jpg"
            }
        ];

        _db.TeamMembers.AddRange(teamMembers);
        _db.SaveChanges();

        Response.Redirect("/About");
    }
}

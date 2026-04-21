using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectMVC.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "SuperAdmin,Admin")]
public abstract class AdminBaseController : Controller
{
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RotaMe.Web.Areas.Owner.Controllers
{
    [Authorize(Roles = "Owner")]
    [Area("Owner")]
    public class OwnerController : Controller
    {
    }
}
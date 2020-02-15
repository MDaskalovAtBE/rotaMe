using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RotaMe.Web.Controllers
{
    public class AvailabilityController : Controller
    {
        public IActionResult Create()
        {
            return View();
        }
    }
}
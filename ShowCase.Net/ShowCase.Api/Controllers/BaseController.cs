using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShowCase.Api.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    // [PreventCsrfAngular]
    public class BaseController : Controller
    {
    }
}

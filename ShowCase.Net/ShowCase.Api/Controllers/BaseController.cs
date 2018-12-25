using Microsoft.AspNetCore.Mvc;
using ShowCase.Api.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShowCase.Api.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [GenerateApiAntiForgeryToken]
    public class BaseController : Controller
    {
    }
}

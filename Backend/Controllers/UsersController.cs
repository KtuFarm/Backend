using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsersController : ApiControllerBase
    {
        public UsersController(ApiContext context) : base(context) {}

    }
}

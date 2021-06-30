using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jarvis.HostApiVersion3.Controllers
{
    public partial class Tag3Controller : Tag2Controller
    {
        [HttpGet]
        [Route("{tagId}")]
        public IActionResult Get(string tagId)
        {
            return Ok(new
            {
                Id = tagId,
                Title = "This is a TAGGG"
            });
        }
    }
}

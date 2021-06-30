
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jarvis.HostApiVersion3.Controllers
{
    public partial class Document2Controller : Document1Controller
    {
        [HttpGet]
        [Route("{documentId}/{fileId}")]
        public virtual IActionResult GetFile(string documentId, string fileId)
        {
            return Ok(new
            {
                Id = documentId,
                FileId = fileId,
                Title = "This is a simulated title and file for version 2"
            });
        }
    }
}

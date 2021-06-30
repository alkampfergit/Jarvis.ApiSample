using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jarvis.HostApiVersion3.Controllers
{
    public partial class Document3Controller : Document2Controller
    {
        public override IActionResult Get(string documentId)
        {
            return base.Get(documentId);
        }
    }
}

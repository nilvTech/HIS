﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HIS.APP.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WebHookController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> PostWebHook()
        {
            var body = Request.Body;
            return Ok(body);
        }
    }

}

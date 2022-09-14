using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using JsonCleanup.Domain;

namespace JsonCleanUp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JsonCleanupController : ControllerBase
    {
        private readonly IRemoveNode _removeNode;

        public JsonCleanupController(IRemoveNode removeNode)
        {
            _removeNode = removeNode;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(JsonResult))]
        public async Task<IActionResult> Get()
        {
            await using (var stream = new MemoryStream())
            {
                await Request.Body.CopyToAsync(stream);
                stream.Position = 0;
                var read = new StreamReader(stream);
                var text = read.ReadToEnd();
                dynamic json = JObject.Parse(text);
                _removeNode.RemoveJsonNode(json);
                return Ok(json.ToString());
            }
        }
    }
}
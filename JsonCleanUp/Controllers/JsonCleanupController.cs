using Microsoft.AspNetCore.Mvc;
using JsonCleanup.Domain;
using JsonCleanUp.Common;
using Newtonsoft.Json;

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
                var myData = JsonConvert.DeserializeObject<IDictionary<String, object>>(text);
                var result = myData.Where(item => !(item.Value is string && StringExtensions.IsStringValid(item.Value.ToString())))
                                   .Select(obj => new Dictionary<String, object> {
                                                                                     { obj.Key, obj.Value }
                                                                                 })
                                   .ToList();
                _removeNode.RemoveJsonNode(result);

                return Ok(JsonConvert.SerializeObject(result));
            }
        }
    }
}
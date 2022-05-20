using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

using QueueByPassAPI.Model;
using QueueByPassAPI.Services;

namespace QueueByPassAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ByPassCallBackController : ControllerBase
    {
        private readonly ILogger<ByPassCallBackController> _logger;
        private readonly IActorBridge _bridge;
        private readonly TestCount _testCount;

        public ByPassCallBackController(ILogger<ByPassCallBackController> logger, IActorBridge bridge, TestCount testCount)
        {
            _logger = logger;
            _bridge = bridge;
            _testCount = testCount;
        }

        [HttpPost("post/{id}")]        
        public async Task<ActionResult<string>> PostTodoItem([FromHeader(Name="CallBackUrl")][Required] string 
            callBackUrl, string id, object  todoItem)
        {
            ActionResult<string> actionResult = new ActionResult<string>("ok");            
            string jsonString = JsonConvert.SerializeObject(todoItem);
            
            _logger.LogInformation("PostTodoItem");

            //Self Test시 test사용
            string tryCallBackUrl = callBackUrl=="test" ? "http://localhost:9000/api/ByPassCallBack/test" : callBackUrl;

            _bridge.Tell(id, new Model.PostSpec()
            { 
                reqId = _testCount.callCount,
                host = tryCallBackUrl, path = null, data = todoItem 
            });
            
            _logger.LogInformation($"TestCallCount {++_testCount.callCount}");

            return actionResult;
        }


        [HttpPost("test")]
        public async Task<ActionResult<string>> TestCallBack(object todoItem, [FromQuery(Name = "reqid")] int reqid)
        {                          
            var rand = new Random();
            int randomDelay = rand.Next(500, 1500);

            await Task.Delay(randomDelay);
            ActionResult<string> actionResult = new ActionResult<string>("ok+");

            _logger.LogInformation($"[REQNO-{reqid}] Done TestCallBack {++_testCount.callBackCount} , Completed Time {randomDelay}");

            return actionResult;
        }
    }
}

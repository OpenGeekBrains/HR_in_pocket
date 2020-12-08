using System;
using System.Collections.Generic;
using System.Linq;

using HRInPocket.Interfaces;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HRInPocket.Controllers.API
{
    [Route(WebAPI.FeedBack)]
    [ApiController]
    public class FeedBackApiController : ControllerBase
    {
        private readonly ILogger<FeedBackApiController> _Logger;
        private readonly FeedBackService _feedbackService;

        public FeedBackApiController(ILogger<FeedBackApiController> Logger)
        {
            _Logger = Logger;
            _feedbackService = new FeedBackService();
        }

        [HttpGet]
        public JsonResult Get() => new JsonResult(_feedbackService.Requests.GroupBy(f=>f.email));
        
        [HttpPost("/user/feedback")]
        public IActionResult TakeFeedBack(string name, string email, string phone_number, string message)
        {
            return Ok();
        }
    }

    public record FeedbackRequest(string name, string email, string phone_number, string message)
    {
        public long id { get; set; }
        public Guid AssignedApplicant { get; set; }
        public DateTime RequestCreation { get; set; }
        public DateTime ApplicantAssignedDate { get; set; }
    }

    public class FeedBackService
    {
        public readonly List<FeedbackRequest> Requests = new();
        private static long _counter;
        private static long Counter => ++_counter;

        public void TakeFeedback(FeedbackRequest request)
        {
            request.id = Counter;
            Requests.Add(request);
        }

        public void AssignApplicant(Guid applicant_id, long request_id)
        {

        }
    }
}

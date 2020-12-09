using System.Collections.Generic;

namespace HRInPocket.Services.Services
{
    public class FeedBackService
    {
        public readonly List<FeedbackRequest> Requests = new List<FeedbackRequest>();
        private static long _counter;
        private static long Counter => ++_counter;

        public void TakeFeedback(FeedbackRequest request)
        {
            request.id = Counter;
            Requests.Add(request);
        }
    }
}
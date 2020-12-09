using System.Collections.Generic;

using HRInPocket.Infrastructure.Models.Records;

namespace HRInPocket.Infrastructure.Services
{
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
    }
}
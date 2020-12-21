namespace Aether.Models
{
    public class NotificationResult
    {
        public bool Successful { get; set; }
        public string ResponseDetails { get; set; }
        public string FailureId { get; set; }

        public static NotificationResult Success(string responseDetails) =>
            new NotificationResult()
            {
                Successful = true,
                FailureId = string.Empty,
                ResponseDetails = responseDetails
            };

        public static NotificationResult Failure(string failureID, string responseDetails) =>
            new NotificationResult()
            {
                Successful = false,
                FailureId = failureID,
                ResponseDetails = responseDetails
            };

        public static NotificationResult NoResults() =>
            new NotificationResult()
            {
                Successful = false,
                FailureId = string.Empty,
                ResponseDetails = "No Results Generated"
            };
    }
}

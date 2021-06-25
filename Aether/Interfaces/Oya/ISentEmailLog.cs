namespace Aether.Interfaces.Oya
{
    public interface ISentEmailLog
    { 
        public string EnforcementRequestId { get; set; }
        public string SystemId { get; set; }
        public string EmailAddress { get; set; }
        public long CreatedDate { get; set; }
        public long LastUpdatedDate { get; set; }
    }
}

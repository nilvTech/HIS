/*
 Date:28-11-2022
 */
namespace HIS.APP.Models
{
    /// <summary>
    /// AUDITBLOB TABLE
    /// </summary>

    #region Module

    public class AuditBlob
    {
        public int ID { get; set; }
        public string PatientId { get; set; }
        public Guid MessageGUID { get; set; }
        public string MessageTextOrRawData { get; set; }

        public DateTime CreatedDate { get; set; } 
        public DateTime? ModifiedDate { get; set; } 
        public string CreatedBy { get; set; } 
        public string ModifiedBy { get; set; } 
        public string SourceSystem { get; set; } 
        public string MessageType { get; set; } 
        public string Status { get; set; }
        public int RetryCount { get; set; } 
        public string ErrorDetails { get; set; } 
        public bool IsArchived { get; set; } 
        public string Comments { get; set; }
    }
}
#endregion
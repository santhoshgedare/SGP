namespace SGP.Core.SharedKernel
{
    public class AggregateRoot : BaseEntity
    {

        public AggregateRoot()
        {
            Uid = Guid.NewGuid().ToString();
        }

        public string Uid { get; private set; }
        public long CreatedBy { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public long? UpdatedBy { get; private set; }
        public DateTime? UpdatedDate { get; private set; }

        public void UpdateAudit(long userId)
        {
            if (CreatedDate != DateTime.MinValue)
            {
                UpdatedBy = userId;
                UpdatedDate = DateTime.UtcNow;
            }
            else
            {
                CreatedBy = userId;
                CreatedDate = DateTime.UtcNow;
            }
        }

        public void UpdateCreatedDate(DateTime createdDate)
        {
          
            CreatedDate = createdDate;
        }
        public void setCreatedBy(long userId)
        {
            CreatedBy = userId;

        }
    }
}

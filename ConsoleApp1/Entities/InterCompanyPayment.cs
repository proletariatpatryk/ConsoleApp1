using System.ComponentModel.DataAnnotations.Schema;

namespace ConsoleApp1.Entities
{
    public class InterCompanyPayment : NotifyPropertyChanged
    {
        private DateTime _receivedOn;
        private DateTime? _postedOn;
        private DateTime? _deletedOn;

        [Column("BusinessUnitId")]
        public int BusinessUnitFK { get; set; }

        [Column("BusinessEntityId")]
        public int BusinessEntityFK { get; set; }

        [Column("PaymentId")]
        public int PaymentPK { get; set; }

        [Column("ReceivedOn")]
        public DateTime ReceivedOn
        {
            get => _receivedOn;
            set => SetWithNotify(ref _receivedOn, value);
        }

        [Column("PostedOn")]
        public DateTime? PostedOn
        {
            get => _postedOn;
            set => SetWithNotify(ref _postedOn, value);
        }

        [Column("DeletedOn")]
        public DateTime? DeletedOn
        {
            get => _deletedOn;
            set => SetWithNotify(ref _deletedOn, value);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsoleApp1.Entities
{
    [PrimaryKey(nameof(BusinessUnitFK), nameof(BusinessEntityFK), nameof(PaymentPK))]
    public class CashPayment : NotifyPropertyChanged
    {
        private string? _type;
        private DateTime _receivedOn;
        private DateTime? _postedOn;
        private DateTime? _deletedOn;

        [Column("BusinessUnitId")]
        public int BusinessUnitFK { get; set; }

        [Column("BusinessEntityId")]
        public int BusinessEntityFK { get; set; }

        [Column("PaymentId")]
        public int PaymentPK { get; set; }

        [Column("CustomerName")]
        public string? CustomerName { get; set; }

        [Column("Type")]
        public string? Type
        {
            get => _type;
            set => SetWithNotify(ref _type, value);
        }

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

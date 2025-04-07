using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsoleApp1.Entities
{
    [Table("BusinessEntityConfig")]
    public class BusinessEntityConfig
    {
        [Key]
        [Column("BusinessEntityConfigId")]
        public int BusinessEntityConfigId { get; set; }

        [Column("BusinessUnitId")]
        public int BusinessUnitId { get; set; }

        [Column("BusinessEntityId")]
        public int BusinessEntityId { get; set; }

        [Column("BusinessEntityName")]
        public required string BusinessEntityName { get; set; }

        [Column("IsInactive")]
        public bool IsInactive { get; set; }

        [Column("ConnectionString")]
        public required string ConnectionString { get; set; }

        [Column("LastRunCashPaymentsOn")]
        public DateTime? LastRunCashPaymentsOn { get; set; }

        [Column("LastRunInterCompanyPaymentsOn")]
        public DateTime? LastRunInterCompanyPaymentsOn { get; set; }

        [Column("IsPostCashPayments")]
        public bool IsPostCashPayments { get; set; }

        [Column("IsPostInterCompanyPayments")]
        public bool IsPostInterCompanyPayments { get; set; }
    }
}

using ConsoleApp1.Entities;
using System.Diagnostics.CodeAnalysis;

namespace ConsoleApp1
{
    public class CashPaymentComparer : IEqualityComparer<CashPayment>
    {
        public bool Equals(CashPayment? x, CashPayment? y)
        {
            if (ReferenceEquals(x, y))
                return true;

            if (x is null || y is null)
                return false;

            return x.BusinessUnitFK == y.BusinessUnitFK &&
                   x.BusinessEntityFK == y.BusinessEntityFK &&
                   x.PaymentPK == y.PaymentPK;
        }

        public int GetHashCode([DisallowNull] CashPayment obj)
        {
            return HashCode.Combine(obj.BusinessUnitFK, obj.BusinessEntityFK, obj.PaymentPK);
        }
    }

    public class InterCompanyPaymentComparer : IEqualityComparer<InterCompanyPayment>
    {
        public bool Equals(InterCompanyPayment? x, InterCompanyPayment? y)
        {
            if (ReferenceEquals(x, y))
                return true;

            if (x is null || y is null)
                return false;

            return x.BusinessUnitFK == y.BusinessUnitFK &&
                   x.BusinessEntityFK == y.BusinessEntityFK &&
                   x.PaymentPK == y.PaymentPK;
        }

        public int GetHashCode([DisallowNull] InterCompanyPayment obj)
        {
            return HashCode.Combine(obj.BusinessUnitFK, obj.BusinessEntityFK, obj.PaymentPK);
        }
    }
}

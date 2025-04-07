using ConsoleApp1.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp1
{
    public class FinancialsService(BridgeContext bridgeContext)
    {
        protected readonly BridgeContext _bridgeContext = bridgeContext;

        public async Task PostAsync(int businessUnitId)
        {
            try
            {
                var businessEntityConfigs = await _bridgeContext.BusinessEntityConfigs
                    .Where(b => b.BusinessUnitId == businessUnitId)
                    .Where(b => b.IsInactive == false)
                    .ToListAsync();

                foreach(var businessEntityConfig in businessEntityConfigs)
                {
                    if (businessEntityConfig.IsPostCashPayments)
                    {
                        await PostCashPaymentsAsync(businessEntityConfig);
                    }

                    if(businessEntityConfig.IsPostInterCompanyPayments)
                    {
                        await PostInterCompanyPaymentsAsync(businessEntityConfig);
                    }

                }
            }
            catch
            {
                throw;
            }
        }

        public async Task ProcessAsync(int businessUnitId)
        {
            using var transaction = _bridgeContext.Database.BeginTransaction();

            try
            {
                var businessEntityConfigs = await _bridgeContext.BusinessEntityConfigs
                    .Where(b => b.BusinessUnitId == businessUnitId)
                    .Where(b => b.IsInactive == false)
                    .ToListAsync();

                await ProcessCashPaymentsAsync(businessEntityConfigs);

                await _bridgeContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        protected async Task ProcessCashPaymentsAsync(List<BusinessEntityConfig> businessEntityConfigs)
        {
            var today = DateTime.Today.Date;
            var comparer = new CashPaymentComparer();

            foreach (var businessEntityConfig in businessEntityConfigs)
            {
                var businessUnitId = businessEntityConfig.BusinessUnitId;
                var businessEntityId = businessEntityConfig.BusinessEntityId;

                var bridgePayments = await _bridgeContext.CashPayments
                    .Where(p => p.BusinessUnitFK == businessUnitId)
                    .Where(p => p.BusinessEntityFK == businessEntityId)
                    .ToListAsync();
                using var ampContext = new AmpContext(businessEntityConfig.ConnectionString);
                var ampPayments = await ampContext.CashPayments
                    .Where(p => p.BusinessUnitFK == businessUnitId)
                    .Where(p => p.BusinessEntityFK == businessEntityId)
                    .AsNoTracking()
                    .ToListAsync();

                var deletedPayments = bridgePayments.Where(p => p.DeletedOn == null).Except(ampPayments, comparer);
                foreach (var deletedPayment in deletedPayments)
                {
                    var bridgePayment = bridgePayments.SingleOrDefault(p => comparer.Equals(p, deletedPayment));

                    if (bridgePayment != null)
                        bridgePayment.DeletedOn = today;
                }

                foreach (var ampPayment in ampPayments)
                {
                    var bridgePayment = bridgePayments.SingleOrDefault(p => comparer.Equals(p, ampPayment));

                    if (bridgePayment == null)
                        bridgePayments.Add(ampPayment);

                    else
                    {
                        bridgePayment.CustomerName = ampPayment.CustomerName;
                        bridgePayment.Type = ampPayment.Type;
                        bridgePayment.ReceivedOn = ampPayment.ReceivedOn;
                        bridgePayment.DeletedOn = ampPayment.DeletedOn;
                    }
                }
            }
        }
    
        protected async Task ProcessInterCompanyPaymentsAsync(List<BusinessEntityConfig> businessEntityConfigs)
        {

        }

        protected async Task PostCashPaymentsAsync(BusinessEntityConfig businessEntityConfig)
        {
            try
            {
                var today = DateTime.Today.Date;

                var cashPayments = await _bridgeContext.CashPayments
                    .Where(p => p.BusinessUnitFK == businessEntityConfig.BusinessUnitId)
                    .Where(p => p.BusinessEntityFK == businessEntityConfig.BusinessEntityId)
                    .Where(p => p.PostedOn == null)
                    .ToListAsync();

                foreach(var cashPayment in cashPayments)
                {
                    cashPayment.PostedOn = today;
                }

                businessEntityConfig.LastRunCashPaymentsOn = today;
                await _bridgeContext.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        protected async Task PostInterCompanyPaymentsAsync(BusinessEntityConfig businessEntityConfig)
        {
            try
            {
                businessEntityConfig.LastRunInterCompanyPaymentsOn = DateTime.Now.Date;
                await _bridgeContext.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}

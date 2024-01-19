using HCN.Localization;
using Volo.Abp.Application.Services;

namespace HCN.Admin;

/* Inherit your application services from this class.
 */

public abstract class AdminAppService : ApplicationService
{
    protected AdminAppService()
    {
        LocalizationResource = typeof(HCNResource);
    }
}
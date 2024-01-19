using HCN.Localization;
using Volo.Abp.Application.Services;

namespace HCN.Public;

/* Inherit your application services from this class.
 */

public abstract class PublicAppService : ApplicationService
{
    protected PublicAppService()
    {
        LocalizationResource = typeof(HCNResource);
    }
}
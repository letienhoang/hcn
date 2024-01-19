using HCN.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace HCN.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class HCNController : AbpControllerBase
{
    protected HCNController()
    {
        LocalizationResource = typeof(HCNResource);
    }
}

using HCN.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace HCN.Public.Controllers;

/* Inherit your controllers from this class.
 */

public abstract class PublicController : AbpControllerBase
{
    protected PublicController()
    {
        LocalizationResource = typeof(HCNResource);
    }
}
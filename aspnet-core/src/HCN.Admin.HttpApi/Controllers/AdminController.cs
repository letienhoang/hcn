using HCN.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace HCN.Admin.Controllers;

/* Inherit your controllers from this class.
 */

public abstract class AdminController : AbpControllerBase
{
    protected AdminController()
    {
        LocalizationResource = typeof(HCNResource);
    }
}
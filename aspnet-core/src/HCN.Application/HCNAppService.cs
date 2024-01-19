using System;
using System.Collections.Generic;
using System.Text;
using HCN.Localization;
using Volo.Abp.Application.Services;

namespace HCN;

/* Inherit your application services from this class.
 */
public abstract class HCNAppService : ApplicationService
{
    protected HCNAppService()
    {
        LocalizationResource = typeof(HCNResource);
    }
}

using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace HCN;

[Dependency(ReplaceServices = true)]
public class HCNBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "HCN";
}

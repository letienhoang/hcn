using HCN.IdentitySettings;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace HCN.EntityManagers;

public class CodeGenerators : ITransientDependency
{
    private readonly IRepository<IdentitySetting, string> _identitySettingRepository;

    public CodeGenerators(IRepository<IdentitySetting, string> identitySettingRepository)
    {
        _identitySettingRepository = identitySettingRepository;
    }

    public async Task<string> TopicGenerateAsync()
    {
        string newCode;
        var identitySetting = await _identitySettingRepository.FindAsync(HCNConsts.TopicIdentitySettingId);
        if (identitySetting == null)
        {
            identitySetting = await _identitySettingRepository.InsertAsync(new IdentitySetting(HCNConsts.TopicIdentitySettingId, "Chủ đề", HCNConsts.TopicIdentitySettingPrefix, 1, 1));
            newCode = identitySetting.Prefix + identitySetting.CurrentNumber;
        }
        else
        {
            identitySetting.CurrentNumber += identitySetting.StepNumber;
            newCode = identitySetting.Prefix + identitySetting.CurrentNumber;

            await _identitySettingRepository.UpdateAsync(identitySetting);
        }
        return newCode;
    }

    public async Task<string> StoryGenerateAsync()
    {
        string newCode;
        var identitySetting = await _identitySettingRepository.FindAsync(HCNConsts.StoryIdentitySettingId);
        if (identitySetting == null)
        {
            identitySetting = await _identitySettingRepository.InsertAsync(new IdentitySetting(HCNConsts.StoryIdentitySettingId, "Câu chuyện", HCNConsts.StoryIdentitySettingPrefix, 1, 1));
            newCode = identitySetting.Prefix + identitySetting.CurrentNumber;
        }
        else
        {
            identitySetting.CurrentNumber += identitySetting.StepNumber;
            newCode = identitySetting.Prefix + identitySetting.CurrentNumber;

            await _identitySettingRepository.UpdateAsync(identitySetting);
        }
        return newCode;
    }

    public async Task<string> MaterialGenerateAsync()
    {
        string newCode;
        var identitySetting = await _identitySettingRepository.FindAsync(HCNConsts.MaterialIdentitySettingId);
        if (identitySetting == null)
        {
            identitySetting = await _identitySettingRepository.InsertAsync(new IdentitySetting(HCNConsts.MaterialIdentitySettingId, "Nguyên liệu", HCNConsts.MaterialIdentitySettingPrefix, 1, 1));
            newCode = identitySetting.Prefix + identitySetting.CurrentNumber;
        }
        else
        {
            identitySetting.CurrentNumber += identitySetting.StepNumber;
            newCode = identitySetting.Prefix + identitySetting.CurrentNumber;

            await _identitySettingRepository.UpdateAsync(identitySetting);
        }
        return newCode;
    }
}
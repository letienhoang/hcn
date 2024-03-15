using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace HCN.Helpers
{
    public class SlugBuilder : ITransientDependency
    {
        public SlugBuilder() { }
        public Task<string> GetSlug(string text)
        {
            Regex regex = new("\\p{IsCombiningDiacriticalMarks}+");
            string slug = text.Normalize(NormalizationForm.FormD).Trim().ToLower();

            slug = regex.Replace(slug, String.Empty)
              .Replace('\u0111', 'd').Replace('\u0110', 'D')
              .Replace(",", "-").Replace(".", "-").Replace("!", "")
              .Replace("(", "").Replace(")", "").Replace(";", "-")
              .Replace("/", "-").Replace("%", "ptram").Replace("&", "va")
              .Replace("?", "").Replace('"', '-').Replace(' ', '-');

            return Task.FromResult(slug);
        }
    }
}
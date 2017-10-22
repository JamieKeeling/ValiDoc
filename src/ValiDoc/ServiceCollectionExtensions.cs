using Microsoft.Extensions.DependencyInjection;
using ValiDoc.Core;
using ValiDoc.Utility;

namespace ValiDoc
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddValiDoc(this IServiceCollection services)
        {
            services.AddTransient<IDocumentRules, DocBuilder>();
            services.AddTransient<IRecursiveDescriptor, RecursiveDescriptor>();
            services.AddTransient<IValidatorErrorMessageBuilder, ValidatorErrorMessageBuilder>();
            services.AddTransient<IFluentValidationHelper, FluentValidationHelpers>();
            services.AddTransient<IRuleDescriptor, RuleDescriptionBuilder>();

            return services;
        }
    }
}

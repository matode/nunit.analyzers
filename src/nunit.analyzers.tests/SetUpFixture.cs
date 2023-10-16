using Gu.Roslyn.Asserts;
using NUnit.Framework;

#if NUNIT4
using NUnit.Framework.Legacy;
#endif

namespace NUnit.Analyzers.Tests
{
    [SetUpFixture]
    internal class SetUpFixture
    {
        [OneTimeSetUp]
        public void SetDefaults()
        {
            Settings.Default = Settings.Default
#if NUNIT4
                .WithMetadataReferences(MetadataReferences.Transitive(typeof(Assert), typeof(ClassicAssert)))
                .WithParseOption(Settings.Default.ParseOptions.WithPreprocessorSymbols("NUNIT4"));
#else
                .WithMetadataReferences(MetadataReferences.Transitive(typeof(Assert)));
#endif
        }
    }
}

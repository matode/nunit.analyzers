using Gu.Roslyn.Asserts;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.Diagnostics;
using NUnit.Analyzers.ClassicModelAssertUsage;
using NUnit.Analyzers.Constants;
using NUnit.Framework;

namespace NUnit.Analyzers.Tests.ClassicModelAssertUsage
{
    [TestFixture]
    public sealed class IsTrueAndTrueClassicModelAssertUsageCondensedCodeFixTests
    {
        private static readonly DiagnosticAnalyzer analyzer = new ClassicModelAssertUsageAnalyzer();
        private static readonly CodeFixProvider fix = new IsTrueAndTrueClassicModelAssertUsageCondensedCodeFix();

        [Test]
        public void VerifyGetFixableDiagnosticIds()
        {
            var fix = new IsTrueAndTrueClassicModelAssertUsageCondensedCodeFix();
            var ids = fix.FixableDiagnosticIds;

            Assert.That(ids, Is.EquivalentTo(new[] { AnalyzerIdentifiers.IsTrueUsage, AnalyzerIdentifiers.TrueUsage }));
        }

        [TestCase("IsTrue", AnalyzerIdentifiers.IsTrueUsage)]
        [TestCase("True", AnalyzerIdentifiers.TrueUsage)]
        public void VerifyIsTrueAndTrueFixes(string assertion, string diagnosticId)
        {
            var expectedDiagnostic = ExpectedDiagnostic.Create(diagnosticId);

            var code = TestUtility.WrapMethodInClassNamespaceAndAddUsings($@"
        public void TestMethod()
        {{
            ↓ClassicAssert.{assertion}(true);
        }}");
            var fixedCode = TestUtility.WrapMethodInClassNamespaceAndAddUsings(@"
        public void TestMethod()
        {
            Assert.That(true);
        }");
            RoslynAssert.CodeFix(analyzer, fix, expectedDiagnostic, code, fixedCode,
                fixTitle: ClassicModelAssertUsageCodeFix.TransformToConstraintModelDescription + IsTrueAndTrueClassicModelAssertUsageCondensedCodeFix.Suffix);
        }

        [TestCase("IsTrue", AnalyzerIdentifiers.IsTrueUsage)]
        [TestCase("True", AnalyzerIdentifiers.TrueUsage)]
        public void VerifyIsTrueAndTrueFixesWithMessage(string assertion, string diagnosticId)
        {
            var expectedDiagnostic = ExpectedDiagnostic.Create(diagnosticId);

            var code = TestUtility.WrapMethodInClassNamespaceAndAddUsings($@"
        public void TestMethod()
        {{
            ↓ClassicAssert.{assertion}(true, ""message"");
        }}");
            var fixedCode = TestUtility.WrapMethodInClassNamespaceAndAddUsings(@"
        public void TestMethod()
        {
            Assert.That(true, ""message"");
        }");
            RoslynAssert.CodeFix(analyzer, fix, expectedDiagnostic, code, fixedCode,
                fixTitle: ClassicModelAssertUsageCodeFix.TransformToConstraintModelDescription + IsTrueAndTrueClassicModelAssertUsageCondensedCodeFix.Suffix);
        }

        [TestCase("IsTrue", AnalyzerIdentifiers.IsTrueUsage)]
        [TestCase("True", AnalyzerIdentifiers.TrueUsage)]
        public void VerifyIsTrueAndTrueFixesWithMessageAndOneArgumentForParams(string assertion, string diagnosticId)
        {
            var expectedDiagnostic = ExpectedDiagnostic.Create(diagnosticId);

            var code = TestUtility.WrapMethodInClassNamespaceAndAddUsings($@"
        public void TestMethod()
        {{
            ↓ClassicAssert.{assertion}(true, ""message-id: {{0}}"", Guid.NewGuid());
        }}");
            var fixedCode = TestUtility.WrapMethodInClassNamespaceAndAddUsings(@"
        public void TestMethod()
        {
            Assert.That(true, $""message-id: {Guid.NewGuid()}"");
        }");
            RoslynAssert.CodeFix(analyzer, fix, expectedDiagnostic, code, fixedCode,
                fixTitle: ClassicModelAssertUsageCodeFix.TransformToConstraintModelDescription + IsTrueAndTrueClassicModelAssertUsageCondensedCodeFix.Suffix);
        }

        [TestCase("IsTrue", AnalyzerIdentifiers.IsTrueUsage)]
        [TestCase("True", AnalyzerIdentifiers.TrueUsage)]
        public void VerifyIsTrueAndTrueFixesWithMessageAndTwoArgumentsForParams(string assertion, string diagnosticId)
        {
            var expectedDiagnostic = ExpectedDiagnostic.Create(diagnosticId);

            var code = TestUtility.WrapMethodInClassNamespaceAndAddUsings($@"
        public void TestMethod()
        {{
            ↓ClassicAssert.{assertion}(true, ""{{0}}, {{1}}"", ""first"", ""second"");
        }}");
            var fixedCode = TestUtility.WrapMethodInClassNamespaceAndAddUsings(@"
        public void TestMethod()
        {
            Assert.That(true, $""{""first""}, {""second""}"");
        }");
            RoslynAssert.CodeFix(analyzer, fix, expectedDiagnostic, code, fixedCode,
                fixTitle: ClassicModelAssertUsageCodeFix.TransformToConstraintModelDescription + IsTrueAndTrueClassicModelAssertUsageCondensedCodeFix.Suffix);
        }

        [TestCase("IsTrue", AnalyzerIdentifiers.IsTrueUsage)]
        [TestCase("True", AnalyzerIdentifiers.TrueUsage)]
        public void VerifyIsTrueAndTrueFixesWithMessageAndArrayParamsInNonstandardOrder(string assertion, string diagnosticId)
        {
            var expectedDiagnostic = ExpectedDiagnostic.Create(diagnosticId);

            var code = TestUtility.WrapMethodInClassNamespaceAndAddUsings($@"
        public void TestMethod()
        {{
            ↓ClassicAssert.{assertion}(args: new[] {{ ""first"", ""second"" }}, message: ""{{0}}, {{1}}"", condition: true);
        }}");
            var fixedCode = TestUtility.WrapMethodInClassNamespaceAndAddUsings(@"
        public void TestMethod()
        {
            Assert.That(true, $""{""first""}, {""second""}"");
        }");
            RoslynAssert.CodeFix(analyzer, fix, expectedDiagnostic, code, fixedCode,
                fixTitle: ClassicModelAssertUsageCodeFix.TransformToConstraintModelDescription + IsTrueAndTrueClassicModelAssertUsageCondensedCodeFix.Suffix);
        }
    }
}

namespace AspNetCore.MinimalApi.Sdk.CodeRules.Custom
{
    [Ignore] // remove me <3
    [TestCategory("MyFirstTestCategory")]
    [TestClass]
    public class MyFirstCustomCodeRule : MsTestBase
    {
        [TestMethod]
        public void My_First_Test_Should_Succeed()
        {
            /*
             * For guidance on implementing these tools, refer to existing code rules. These rules serve as a cornerstone for best practices, advising on the types of tests to write and the outcomes they should achieve.
             *
             * Writing Tests:
             * - Prioritize writing a multitude of simple tests over a few complex ones. This approach helps maintain focus and ensures thorough coverage.
             * - Avoid crafting a single, large test to validate your entire solution. It's less effective and harder to debug.
             * - Focus each test on a single issue. Testing multiple rules in one go can complicate understanding and fixing problems.
             *
             * Returning Information to Developers:
             * - When reporting issues, provide comprehensive details necessary for resolution. This includes the location of the error, its nature, and suggestions for correction.
             * - Document your code rules clearly. Understanding the rationale behind rules not only builds trust but also helps prevent repeated errors.
             *
             *
             * We are committed to continuous improvement and value the insights our user community brings to the table. By collaborating, we can create a more robust framework that not only addresses current needs but also anticipates future challenges.
             *
             * Please do not hesitate to reach out with your suggestions, innovations, or feedback. Together, we can build a better product for everyone involved.
             */
        }
    }
}

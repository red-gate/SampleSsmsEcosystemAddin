namespace SampleSsmsEcosystemAddin.Examples.SsmsFunctionalityProvider
{
    internal class CustomSsmsFunctionProvider : ICustomSsmsFunctionProvider
    {
        private readonly object m_SsmsDte2;

        public CustomSsmsFunctionProvider(object ssmsDte2)
        {
            m_SsmsDte2 = ssmsDte2;
        }
    }
}
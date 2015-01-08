namespace SampleSsmsEcosystemAddin.Examples.SsmsFunctionalityProvider
{
    internal class CustomSsmsFunctionProvider : ICustomSsmsFunctionProvider
    {
        private readonly object m_SsmsDte2;
        private readonly ICustomSsmsFunctionProvider m_VersionSpecificProvider;

        public CustomSsmsFunctionProvider(object ssmsDte2)
        {
            m_SsmsDte2 = ssmsDte2;
            m_VersionSpecificProvider = LoadVersionSpecific(ssmsDte2);
        }

        private ICustomSsmsFunctionProvider LoadVersionSpecific(object ssmsDte2)
        {
            var loadedSsmsAssembly = FindLoadedSsmsAssembly();
            throw new System.NotImplementedException();
        }

        private SsmsAssembly FindLoadedSsmsAssembly()
        {
            throw new System.NotImplementedException();
        }
    }
}
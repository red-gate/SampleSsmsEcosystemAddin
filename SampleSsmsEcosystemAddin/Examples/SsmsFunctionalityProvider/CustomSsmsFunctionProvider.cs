using System;
using System.Linq;
using System.Reflection;

namespace SampleSsmsEcosystemAddin.Examples.SsmsFunctionalityProvider
{
    internal class CustomSsmsFunctionProvider : ICustomSsmsFunctionProvider
    {
        private const string c_SsmsPublicKeyToken = "89-84-5D-CD-80-80-CC-91";
        private readonly string[] m_RecognisedSsmsAssemblyNames = new string[] {    "SqlWorkbench.Interfaces", 
                                                                                    "Microsoft.SqlServer.Express.SqlWorkbench.Interfaces", 
                                                                                    "Microsoft.SqlServer.Express.VSIntegration", 
                                                                                    "Microsoft.SqlServer.SqlTools.VSIntegration"};

        private readonly object m_SsmsDte2;
        private readonly ICustomSsmsFunctionProvider m_VersionSpecificProvider;
        
        public CustomSsmsFunctionProvider(object ssmsDte2)
        {
            m_SsmsDte2 = ssmsDte2;
            m_VersionSpecificProvider = LoadVersionSpecific(ssmsDte2);
        }

        private ICustomSsmsFunctionProvider LoadVersionSpecific(object ssmsDte2)
        {
            SsmsAssembly loadedSsmsAssembly;
            if (!TryFindLoadedSsmsAssembly(out loadedSsmsAssembly))
            {
                throw new Exception("Unknown version of SSMS running.");
            }

            throw new NotImplementedException();
            
        }

        private bool TryFindLoadedSsmsAssembly(out SsmsAssembly ssmsAssembly)
        {
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                AssemblyName name = assembly.GetName();
                if (HasSsmsPublicKeyToken(name) && m_RecognisedSsmsAssemblyNames.Contains(name.Name))
                {
                    ssmsAssembly = new SsmsAssembly(assembly);
                    return true;
                }
            }
            ssmsAssembly = null;
            return false;
        }

        private bool HasSsmsPublicKeyToken(AssemblyName name)
        {
            return BitConverter.ToString(name.GetPublicKeyToken()) == c_SsmsPublicKeyToken;
        }
    }
}
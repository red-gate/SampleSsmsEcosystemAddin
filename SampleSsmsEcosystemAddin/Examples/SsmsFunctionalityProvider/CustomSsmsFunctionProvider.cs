using System;
using System.Linq;
using System.Reflection;
using Ssms2012;
using Ssms2014;

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
            if(!TryLoadVersionSpecific(m_SsmsDte2, out m_VersionSpecificProvider))
                throw new Exception("Could not load custom function provider for this version of SSMS.");
        }

        private bool TryLoadVersionSpecific(object ssmsDte2, out ICustomSsmsFunctionProvider versionSpecificFunctionProvider)
        {
            versionSpecificFunctionProvider = null;

            SsmsAssembly loadedSsmsAssembly;
            if (TryFindLoadedSsmsAssembly(out loadedSsmsAssembly))
            {
                if (loadedSsmsAssembly.Version == new Version(9, 0, 242, 0))
                {
                    versionSpecificFunctionProvider = Load2005VersionSpecific(loadedSsmsAssembly, ssmsDte2);
                    return true;
                }

                if (loadedSsmsAssembly.Version == new Version(10, 0, 0, 0))
                {
                    versionSpecificFunctionProvider = Load2008VersionSpecific(loadedSsmsAssembly, ssmsDte2);
                    return true;
                }

                if (loadedSsmsAssembly.Version == new Version(11, 0, 0, 0))
                {
                    versionSpecificFunctionProvider = new Ssms2012FunctionProvider(ssmsDte2);
                    return true;
                }

                if (loadedSsmsAssembly.Version == new Version(12, 0, 0, 0))
                {
                    versionSpecificFunctionProvider = new Ssms2014FunctionProvider(ssmsDte2);
                    return true;
                }
            }

            return false;
        }

        private ICustomSsmsFunctionProvider Load2008VersionSpecific(SsmsAssembly loadedSsmsAssembly, object ssmsDte2)
        {
            if (loadedSsmsAssembly.FileVersion.FileMinorPart < 50)
                return null; //new Ssms2008FunctionProvider(ssmsDte2);
            return null; //new Ssms2008r2FunctionProvider(ssmsDte2);
        }

        private ICustomSsmsFunctionProvider Load2005VersionSpecific(SsmsAssembly loadedSsmsAssembly, object ssmsDte2)
        {
            if (loadedSsmsAssembly.FileVersion.FileBuildPart < 3000)
            {
                if (loadedSsmsAssembly.StrongName == "Microsoft.SqlServer.Express.VSIntegration" || loadedSsmsAssembly.StrongName == "Microsoft.SqlServer.Express.SqlWorkbench.Interfaces")
                    return null; //new Ssms2005ExpressP1FunctionProvider(ssmsDte2);

                return null; //new Ssms2005RtmFunctionProvider(ssmsDte2);
            }
            if (loadedSsmsAssembly.StrongName == "Microsoft.SqlServer.Express.VSIntegration" || loadedSsmsAssembly.StrongName == "Microsoft.SqlServer.Express.SqlWorkbench.Interfaces")
                return null; //new Ssms2005ExpressFunctionProvider(ssmsDte2);

            return null; //new Ssms2005Sp2FunctionProvider(ssmsDte2);
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
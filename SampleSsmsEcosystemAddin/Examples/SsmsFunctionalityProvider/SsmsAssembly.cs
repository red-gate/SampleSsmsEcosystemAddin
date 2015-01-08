using System;
using System.Reflection;

namespace SampleSsmsEcosystemAddin.Examples.SsmsFunctionalityProvider
{
    internal class SsmsAssembly
    {
        private readonly Assembly m_Assembly;

        public Version Version {get { return m_Assembly.GetName().Version; } }
        public Assembly Assembly {get { return m_Assembly; } }
        public string StrongName {get { return m_Assembly.GetName().Name; } }

        public SsmsAssembly(Assembly assembly)
        {
            m_Assembly = assembly;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SampleSsmsEcosystemAddin.Examples.SsmsFunctionalityProvider;

namespace Ssms2014
{
    public class Ssms2014FunctionProvider : ICustomSsmsFunctionProvider
    {
        private readonly object m_SsmsDte2;

        public Ssms2014FunctionProvider(object ssmsDte2)
        {
            m_SsmsDte2 = ssmsDte2;
        }
    }
}

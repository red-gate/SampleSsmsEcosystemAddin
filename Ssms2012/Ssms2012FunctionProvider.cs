using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SampleSsmsEcosystemAddin.Examples.SsmsFunctionalityProvider;

namespace Ssms2012
{
    public class Ssms2012FunctionProvider : ICustomSsmsFunctionProvider
    {
        private readonly object m_SsmsDte2;

        public Ssms2012FunctionProvider(object ssmsDte2)
        {
            m_SsmsDte2 = ssmsDte2;
        }
    }
}

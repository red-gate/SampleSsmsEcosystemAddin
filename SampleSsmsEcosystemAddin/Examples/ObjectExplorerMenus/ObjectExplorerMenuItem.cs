using System;
using RedGate.SIPFrameworkShared;

namespace SampleSsmsEcosystemAddin.Examples
{
    public class ObjectExplorerMenuItem : ActionSimpleOeMenuItemBase
    {
        private readonly string m_Label;
        private readonly ISsmsFunctionalityProvider4 m_Provider4;
        private readonly Action<string> m_LogMessage;

        public ObjectExplorerMenuItem(string label, ISsmsFunctionalityProvider4 provider4, Action<string> logMessageCallback)
        {
            m_Label = label;
            m_Provider4 = provider4;
            m_LogMessage = logMessageCallback;
        }

        public override bool AppliesTo(ObjectExplorerNodeDescriptorBase oeNode)
        {
            return true;
        }

        public override string ItemText
        {
            get { return m_Label; }
        }

        public override void OnAction(ObjectExplorerNodeDescriptorBase node)
        {
            var oeNode = (IOeNode) node;
            if (oeNode == null)
            {
                m_Provider4.QueryWindow.OpenNew("null");
                return;
            }
            m_Provider4.QueryWindow.OpenNew(string.Format("Name: {0}\nPath: {1}", oeNode.Name, oeNode.Path));
        }
    }
}

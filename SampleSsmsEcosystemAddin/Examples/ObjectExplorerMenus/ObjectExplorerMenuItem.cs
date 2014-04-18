using System;
using System.Data.SqlClient;
using RedGate.SIPFrameworkShared;

namespace SampleSsmsEcosystemAddin.Examples
{
    public class ObjectExplorerMenuItem : ActionSimpleOeMenuItemBase
    {
        private readonly string m_Label;
        private readonly ISsmsFunctionalityProvider6 m_Provider;
        private readonly Action<string> m_LogMessage;

        public ObjectExplorerMenuItem(string label, ISsmsFunctionalityProvider6 provider, Action<string> logMessageCallback)
        {
            m_Label = label;
            m_Provider = provider;
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
                m_Provider.QueryWindow.OpenNew("null");
                return;
            }
            IDatabaseObjectInfo databaseObjectInfo;
            IConnectionInfo connectionInfo;
            if (oeNode.TryGetDatabaseObject(out databaseObjectInfo) && oeNode.TryGetConnection(out connectionInfo))
            {
                using (var connection = new SqlConnection(connectionInfo.ConnectionString))
                {
                    connection.Open();
                    var sql = m_Provider.ServerManagementObjects.ScriptAsAlter(connection,
                                                                               databaseObjectInfo.DatabaseName,
                                                                               databaseObjectInfo.Schema,
                                                                               databaseObjectInfo.ObjectName);
                    m_Provider.QueryWindow.OpenNew(sql, databaseObjectInfo.ObjectName, connectionInfo.ConnectionString);
                }
            }
            m_Provider.QueryWindow.OpenNew(string.Format("Name: {0}\nPath: {1}", oeNode.Name, oeNode.Path));
        }
    }
}

using System;
using RedGate.SIPFrameworkShared;
using RedGate.SIPFrameworkShared.ObjectExplorer;
using SampleSsmsEcosystemAddin.Examples;
using SampleSsmsEcosystemAddin.Examples.CustomQueryWindow;
using SampleSsmsEcosystemAddin.Examples.MessagesWindow;
using SampleSsmsEcosystemAddin.Examples.ObjectExplorerMenus;

namespace SampleSsmsEcosystemAddin
{
    /// <summary>
    /// You must have SIPFramework installed. You can find a standalone installer here: http://www.red-gate.com/ssmsecosystem
    /// 
    /// SIPFramework hooks into SSMS and launches add ins. You will need to register this sample add-in with SIPFramework. To do this:
    /// 1. Find registry key: HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Red Gate\SIPFramework\Plugins
    /// 2. Create a new string with the name "SampleAddIn".
    /// 3. Set the value to the full file path of SampleSsmsEcosystemAddin.dll.
    ///     For example: C:\Users\david\Documents\SampleSsmsEcosystemAddin\SampleSsmsEcosystemAddin\bin\Debug\SampleSsmsEcosystemAddin.dll
    ///  
    /// </summary>
    public class SampleAddin : ISsmsAddin4
    {
        /// <summary>
        /// Add in meta data
        /// </summary>
        public string Version { get { return "1.0.0.0"; } }
        public string Description { get { return "A sample add in for Red Gate's SIPFramework."; } }
        public string Name { get { return "Sample Add in"; } }
        public string Author { get { return "Red Gate"; } }
        public string Url { get { return @"https://github.com/red-gate/SampleSsmsEcosystemAddin"; } }

        private const string c_MessageWindowGuid = "C97F1BC2-8ADD-4BED-B328-56679DBC0656";

        private ISsmsFunctionalityProvider6 m_Provider;
        private MessageLog m_MessageLog;
        private IToolWindow m_MessageLogWindow;

        /// <summary>
        /// This is the entry point for your add in.
        /// </summary>
        /// <param name="provider">This gives you access to the SSMS integrations provided by SIPFramework. If there's something missing let me know!</param>
        public void OnLoad(ISsmsExtendedFunctionalityProvider provider)
        {
            m_Provider = (ISsmsFunctionalityProvider6)provider;    //Caste to the latest version of the interface
            
            m_MessageLog = new MessageLog();
            var messagesView = new MessagesView { DataContext = m_MessageLog };
            m_MessageLogWindow = m_Provider.ToolWindow.Create(messagesView, "Sample Add-in", new Guid(c_MessageWindowGuid));
            m_MessageLogWindow.Window.Dock();
            DisplayMessages();

            AddMenuBarMenu();
            AddCustomQueryWindowButton();
            AddObjectExplorerContextMenu();
            AddObjectExplorerListener();
            AddToolbarButton();
        }

        private void AddCustomQueryWindowButton()
        {
            var command = new OpenCustomQueryWindowCommand(m_Provider);
            m_Provider.AddToolbarItem(command);
        }  

        private void AddObjectExplorerListener()
        {
            m_Provider.ObjectExplorerWatcher.ConnectionsChanged += (args) => { OnConnectionsChanged(args); };
            m_Provider.ObjectExplorerWatcher.SelectionChanged += (args) => { OnSelectionChanged(args); };
        }

        private void OnSelectionChanged(ISelectionChangedEventArgs args)
        {
            m_MessageLog.AddMessage(string.Format("Object explorer selection: {0}", args.Selection.Path));
        }

        private void OnConnectionsChanged(IConnectionsChangedEventArgs args)
        {
            m_MessageLog.AddMessage("Object explorer connections:");
            int count = 1;
            foreach (var connection in args.Connections)
            {
                m_MessageLog.AddMessage(string.Format("\t{0}: {1}", count, connection.Server));
                count++;
            }
        }

        /// <summary>
        /// Callback when SSMS is beginning to shutdown.
        /// </summary>
        public void OnShutdown()
        {
        }

        /// <summary>
        /// Deprecated. Subscribe to m_Provider.ObjectExplorerWatcher.SelectionChanged
        /// 
        /// Callback when object explorer node selection changes.
        /// </summary>
        /// <param name="node">The node that was selected.</param>
        public void OnNodeChanged(ObjectExplorerNodeDescriptorBase node)
        {
        }
        
        private void AddMenuBarMenu()
        {
            var command = new SharedCommand(m_Provider, LogAndDisplayMessage);
            m_Provider.AddGlobalCommand(command);

            m_Provider.MenuBar.MainMenu.BeginSubmenu("Sample", "Sample")
                .BeginSubmenu("Sub 1", "Sub1")
                    .AddCommand(command.Name)
                    .AddCommand(command.Name)
                .EndSubmenu()
            .EndSubmenu();
        }

        private void AddToolbarButton()
        {
            m_Provider.AddToolbarItem(new SharedCommand(m_Provider, LogAndDisplayMessage));
        }

        private void AddObjectExplorerContextMenu()
        {
            var subMenus = new SimpleOeMenuItemBase[]
            {
                new ObjectExplorerMenuItem("Command 1", m_Provider, LogMessage),
                new ObjectExplorerMenuItem("Command 2", m_Provider, LogMessage),
            };
            m_Provider.AddTopLevelMenuItem(new ObjectExplorerSubmenu(subMenus));
        }

        public void LogAndDisplayMessage(string text)
        {
            LogMessage(text);
            DisplayMessages();
        }

        public void LogMessage(string text)
        {
            m_MessageLog.AddMessage(text);
        }

        public void DisplayMessages()
        {
            m_MessageLogWindow.Activate(true);
        }
        


        
    }
}
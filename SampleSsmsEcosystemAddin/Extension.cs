using RedGate.SIPFrameworkShared;

namespace SampleSsmsEcosystemAddin
{
    /// <summary>
    /// You must have SIPFramework installed. You can find a standalone installer here: http://www.red-gate.com/ssmsecosystem
    /// 
    /// SIPFramework hooks into SSMS and launches add ins. When this project is compiled it will automatically register with SIPFramework by 
    /// creating a registry entry in: HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Red Gate\SIPFramework\Plugins
    ///  
    /// </summary>
    public class Extension : ISsmsAddin4
    {
        /// <summary>
        /// Add in meta data
        /// </summary>
        public string Version { get { return "1.0.0.0"; } }
        public string Description { get { return "A sample add in for Red Gate's SIPFramework."; } }
        public string Name { get { return "Sample Add in"; } }
        public string Author { get { return "Red Gate"; } }
        public string Url { get { return @"https://github.com/red-gate/SampleSsmsEcosystemAddin"; } }

        private ISsmsFunctionalityProvider6 m_Provider;

        /// <summary>
        /// This is the entry point for your add in.
        /// </summary>
        /// <param name="provider">This gives you access to the SSMS integrations provided by SIPFramework. If there's something missing let me know!</param>
        public void OnLoad(ISsmsExtendedFunctionalityProvider provider)
        {
            m_Provider = (ISsmsFunctionalityProvider6)provider;    //Caste to the latest version of the interface

            AddMenuBarMenu();
            AddObjectExplorerContextMenu();
            AddToolbarButton();
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
            var subMenus = new SimpleOeMenuItemBase[]
            {
                new Menu("Command 1", m_Provider),
                new Menu("Command 2", m_Provider),
            };
            m_Provider.AddTopLevelMenuItem(new Submenu(subMenus));

            var command = new SharedCommand(m_Provider);
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
            m_Provider.AddToolbarItem(new SharedCommand(m_Provider));
        }

        private void AddObjectExplorerContextMenu()
        {
            //throw new System.NotImplementedException();
        }


        


        
    }
}
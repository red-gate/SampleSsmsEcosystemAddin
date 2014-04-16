using System;
using RedGate.SIPFrameworkShared;

namespace SampleSsmsEcosystemAddin
{
    public class Extension : ISsmsAddin
    {
        private ISsmsFunctionalityProvider6 m_Provider;

        public void OnLoad(ISsmsExtendedFunctionalityProvider provider)
        {
            m_Provider = (ISsmsFunctionalityProvider6)provider;    //Caste to the latest version of the interface

            var subMenus = new SimpleOeMenuItemBase[]
            {
                new Menu("Command 1", m_Provider),
                new Menu("Command 2", m_Provider),
            };
            

            //m_Provider4.AddToolsMenuItem(new Command());

            m_Provider.AddGlobalCommand(new SharedCommand(m_Provider));

            m_Provider.MenuBar.MainMenu.BeginSubmenu("Sample", "Sample")
                .BeginSubmenu("Sub 1", "Sub1")
                .AddCommand("RedGate_Sample_Command")
                .AddCommand("RedGate_Sample_Command")
                .EndSubmenu()
                .BeginSubmenu("Sub 2", "Sub2")
                //.AddCommand("Command3")
                //.AddCommand("Command4")
                .EndSubmenu();

            
            m_Provider.AddToolbarItem(new Command());

            m_Provider.AddTopLevelMenuItem(new Submenu(subMenus));
        }

        public void OnNodeChanged(ObjectExplorerNodeDescriptorBase node)
        {
            //Called when object explorer node selection changes.
        }

        public string Version { get { return "RedGate.Sample 1.0"; } }
    }
}
using System;
using RedGate.SIPFrameworkShared;

namespace SampleSsmsEcosystemAddin
{
    public class Extension : ISsmsAddin
    {
        private ISsmsFunctionalityProvider4 m_Provider4;
        private object m_Dte2;

        public void OnLoad(ISsmsExtendedFunctionalityProvider provider)
        {
            m_Provider4 = (ISsmsFunctionalityProvider4) provider;
            
            m_Dte2 = m_Provider4.SsmsDte2;

            if(m_Provider4 == null)
                throw new ArgumentException();

            var subMenus = new SimpleOeMenuItemBase[]
            {
                new Menu("Command 1", m_Provider4),
                new Menu("Command 2", m_Provider4),
            };
            

            //m_Provider4.AddToolsMenuItem(new Command());

            m_Provider4.AddGlobalCommand(new SharedCommand(m_Provider4));

            m_Provider4.MenuBar.MainMenu.BeginSubmenu("Sample", "Sample")
                .BeginSubmenu("Sub 1", "Sub1")
                .AddCommand("RedGate_Sample_Command")
                .AddCommand("RedGate_Sample_Command")
                .EndSubmenu()
                .BeginSubmenu("Sub 2", "Sub2")
                //.AddCommand("Command3")
                //.AddCommand("Command4")
                .EndSubmenu();

            
            m_Provider4.AddToolbarItem(new Command());

            m_Provider4.AddTopLevelMenuItem(new Submenu(subMenus));
        }

        public void OnNodeChanged(ObjectExplorerNodeDescriptorBase node)
        {
            //Called when object explorer node selection changes.
        }

        public string Version { get { return "RedGate.Sample 1.0"; } }
    }
}
using System.Reflection;
using RedGate.SIPFrameworkShared;

namespace SampleSsmsEcosystemAddin.Examples
{
    public class SharedCommand : ISharedCommandWithExecuteParameter, ISharedCommand
    {
        private readonly ISsmsFunctionalityProvider4 m_Provider;
        private readonly ICommandImage m_CommandImage = new CommandImageForEmbeddedResources(Assembly.GetExecutingAssembly(), "SampleSsmsEcosystemAddin.Examples.rg_icon.ico");

        public SharedCommand(ISsmsFunctionalityProvider4 provider)
        {
            m_Provider = provider;
        }

        public string Name { get { return "RedGate_Sample_Command"; } }
        public void Execute(object parameter)
        {
            m_Provider.QueryWindow.OpenNew("hello");
        }

        public string Caption { get { return "Red Gate Sample Command"; } }
        public string Tooltip { get { return "Tooltip"; }}
        public ICommandImage Icon { get { return m_CommandImage; } }
        public string[] DefaultBindings { get { return new[] { "global::Ctrl+Alt+D" }; } }
        public bool Visible { get { return true; } }
        public bool Enabled { get { return true; } }


        public void Execute()
        {
            
        }
    }
}
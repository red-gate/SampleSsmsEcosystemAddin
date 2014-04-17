using RedGate.SIPFrameworkShared;

namespace SampleSsmsEcosystemAddin
{
    public class Command : ISharedCommand
    {
        public string Name { get { return "RedGate_SampleExtension_Example"; } }
        public void Execute()
        {
            
        }

        public string Caption { get { return "Caption"; } }
        public string Tooltip { get { return "Tooltip"; } }
        public ICommandImage Icon { get { return new CommandImageNone(); } }
        public string[] DefaultBindings { get { return new string[] {}; } }
        public bool Visible { get { return true; } }
        public bool Enabled { get { return true; } }
    }
}
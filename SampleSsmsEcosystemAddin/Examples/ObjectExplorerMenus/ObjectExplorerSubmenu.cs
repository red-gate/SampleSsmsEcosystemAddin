using System.Reflection;
using RedGate.SIPFrameworkShared;

namespace SampleSsmsEcosystemAddin.Examples.ObjectExplorerMenus
{
    class ObjectExplorerSubmenu : SubmenuSimpleOeMenuItemBase
    {
        private readonly ICommandImage m_CommandImage = new CommandImageForEmbeddedResources(Assembly.GetExecutingAssembly(), "SampleSsmsEcosystemAddin.Examples.rg_icon.ico");

        public ObjectExplorerSubmenu(SimpleOeMenuItemBase[] subMenus)
            : base(subMenus)
        {
        }

        public override string ItemText
        {
            get { return "SampleExtension"; }
        }

        public override bool AppliesTo(ObjectExplorerNodeDescriptorBase oeNode)
        {
            return GetApplicableChildren(oeNode).Length > 0;
        }
    }
}
using MetaTools.Core;
using MetaTools.Modules.Comments.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace MetaTools.Modules.Comments
{
    public class CommentsModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public CommentsModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RequestNavigate(RegionNames.Comments, nameof(CommentView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<CommentView>();
        }
    }
}
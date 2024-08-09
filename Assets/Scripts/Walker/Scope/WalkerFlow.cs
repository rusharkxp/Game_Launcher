using Utility;
using VContainer.Unity;
using Walker.Controllers;
using Walker.UI;

namespace Walker.Scope
{
    public class WalkerFlow : IStartable
    {
        private readonly LoadingService _loadingService;

        private readonly WalkerUIController _walkerUIController;

        private readonly WalkerScoreController _walkerScoreController;

        private WalkerFlow(LoadingService loadingService,
            WalkerUIController walkerUIController,
            WalkerScoreController walkerScoreController)
        {
            _loadingService = loadingService;

            _walkerUIController = walkerUIController;

            _walkerScoreController = walkerScoreController;
        }

        public async void Start()
        {
            await _loadingService.BeginLoading(_walkerUIController);
            
            await _loadingService.BeginLoading(_walkerScoreController);
            
            _walkerUIController.StartTimer();
        }
    }
}
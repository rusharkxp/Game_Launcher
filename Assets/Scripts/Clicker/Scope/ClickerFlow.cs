using Clicker.UI;
using Utility;
using VContainer.Unity;

namespace Clicker.Scope
{
    public class ClickerFlow : IStartable
    {
        private readonly LoadingService _loadingService;
        
        private readonly ClickerUIController _clickerUIController;


        private ClickerFlow(LoadingService loadingService, 
            ClickerUIController clickerUIController)
        {
            _loadingService = loadingService;

            _clickerUIController = clickerUIController;
        }

        public async void Start()
        {
            await _loadingService.BeginLoading(_clickerUIController);
        }
    }
}
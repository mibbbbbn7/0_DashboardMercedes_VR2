using System.Collections.Generic;
using System.Threading.Tasks;

namespace DashboardMercedes
{
    public class StartUpFlow
    {
        public void BeginStartUp(Client client)
        {
            StartUpFlowTask(client);
        }

        private async Task StartUpFlowTask(Client client)
        {
            ISoundFeature soundFeature = client.Features.Get<ISoundFeature>();
            IMainMenuFeature menuFeature = client.Features.Get<IMainMenuFeature>();
            ITurnOnCarFeature turnOnCar = client.Features.Get<ITurnOnCarFeature>();
            IDashboardInitializationController dashboardInitController = client.Controllers.Get<IDashboardInitializationController>();
            ITurnOnCarInitializationController turnOnCarInitController = client.Controllers.Get<ITurnOnCarInitializationController>();


            dashboardInitController.InitializeFeature();
            await dashboardInitController.LoadFeature();
            dashboardInitController.OnFeatureLoadedAndInitialized();

            //turnOnCarInitController.InitializeFeature();
            //await turnOnCarInitController.LoadFeature();
            //turnOnCarInitController.OnFeatureLoadedAndInitialized();

            //await turnOnCar.InstantiateTurnOnCar();
            await menuFeature.InstantiateMainMenu();
        }
    }
}

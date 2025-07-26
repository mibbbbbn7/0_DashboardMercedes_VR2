using System.Collections.Generic;
using System.Threading.Tasks;

namespace DashboardMercedes
{
    public class DashboardStartUpFlow
    {
        public void BeginStartUp(Client client)
        {
            StartUpFlowTask(client);
        }

        private async Task StartUpFlowTask(Client client)
        {
            // ISoundFeature soundFeature = client.Features.Get<ISoundFeature>();
            // ITurnOnCarFeature turnOnCar = client.Features.Get<ITurnOnCarFeature>();
            // IDashboardInitializationController dashboardInitController = client.Controllers.Get<IDashboardInitializationController>();
            // ITurnOnCarInitializationController turnOnCarInitController = client.Controllers.Get<ITurnOnCarInitializationController>();
            //
            //
            // dashboardInitController.InitializeFeature();
            // await dashboardInitController.LoadFeature();
            // dashboardInitController.OnFeatureLoadedAndInitialized();
            //
            // turnOnCarInitController.InitializeFeature();
            // await turnOnCarInitController.LoadFeature();
            // turnOnCarInitController.OnFeatureLoadedAndInitialized();
            //
            // await soundFeature.InstantiateSoundFeature();
            // await turnOnCar.InstantiateTurnOnCarFeature();

            ISoundFeature soundFeature = client.Features.Get<ISoundFeature>();
            IMainMenuFeature menuFeature = client.Features.Get<IMainMenuFeature>();
            IDashboardInitializationController initController = client.Controllers.Get<IDashboardInitializationController>();

            initController.InitializeFeature();
            await initController.LoadFeature();
            initController.OnFeatureLoadedAndInitialized();

            await menuFeature.InstantiateMainMenu();
        }
    }
}

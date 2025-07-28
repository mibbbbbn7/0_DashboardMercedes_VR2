using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

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

            IBroadcaster clientBroadcaster = client.Services.Get<IBroadcaster>();
            IDashboardFeature dashboardFeature = client.Features.Get<IDashboardFeature>();
            ISoundFeature soundFeature = client.Features.Get<ISoundFeature>();
            IMainMenuFeature menuFeature = client.Features.Get<IMainMenuFeature>();
            ILoadingStartFeature loadingStartFeature = client.Features.Get<ILoadingStartFeature>();
            IDashboardInitializationController DashboardinitController = client.Controllers.Get<IDashboardInitializationController>();

            DashboardinitController.InitializeFeature();
            await DashboardinitController.LoadFeature();
            DashboardinitController.OnFeatureLoadedAndInitialized();

            //await menuFeature.InstantiateMainMenu();

            await soundFeature.InstantiateSoundFeature();
            //await loadingStartFeature.InstantiateLoadingStart();
            await dashboardFeature.InstantiateDashboardFeature();

            clientBroadcaster.Broadcast(new LoadingTerminatedEvent());
        }
    }
}

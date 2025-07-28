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

            IBroadcaster clientBroadcaster = client.Services.Get<IBroadcaster>();
            IDashboardFeature dashboardFeature = client.Features.Get<IDashboardFeature>();
            ISoundFeature soundFeature = client.Features.Get<ISoundFeature>();
            ILoadingStartFeature loadingStartFeature = client.Features.Get<ILoadingStartFeature>();
            IDashboardInitializationController DashboardinitController = client.Controllers.Get<IDashboardInitializationController>();

            DashboardinitController.InitializeFeature();
            await DashboardinitController.LoadFeature();
            DashboardinitController.OnFeatureLoadedAndInitialized();

            await soundFeature.InstantiateSoundFeature();
            await loadingStartFeature.InstantiateLoadingStart();
            await dashboardFeature.InstantiateDashboardFeature();

            clientBroadcaster.Broadcast(new LoadingTerminatedEvent());
        }
    }
}

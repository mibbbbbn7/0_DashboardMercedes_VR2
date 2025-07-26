using System.Collections.Generic;
using System.Threading.Tasks;

namespace DashboardMercedes
{
    public class CarStartUpFlow
    {
        public void BeginStartUp(Client client)
        {
            StartUpFlowTask(client);
        }

        private async Task StartUpFlowTask(Client client)
        {
            ISoundFeature soundFeature = client.Features.Get<ISoundFeature>();
            ITurnOnCarFeature turnOnCar = client.Features.Get<ITurnOnCarFeature>();
            ITurnOnCarInitializationController turnOnCarInitController = client.Controllers.Get<ITurnOnCarInitializationController>();

            turnOnCarInitController.InitializeFeature();
            await turnOnCarInitController.LoadFeature();
            turnOnCarInitController.OnFeatureLoadedAndInitialized();

            await soundFeature.InstantiateSoundFeature();
            await turnOnCar.InstantiateTurnOnCarFeature();
        }
    }
}

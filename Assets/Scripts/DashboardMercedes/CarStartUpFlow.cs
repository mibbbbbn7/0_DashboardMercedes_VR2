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


            // controller lasciati per completezza dell'architettura vista in classe anche
            // se per ora non ne traggo molto utilizzo :|
            turnOnCarInitController.InitializeFeature();
            await turnOnCarInitController.LoadFeature();
            turnOnCarInitController.OnFeatureLoadedAndInitialized();

            await soundFeature.InstantiateSoundFeature();
            await turnOnCar.InstantiateTurnOnCarFeature();
        }
    }
}

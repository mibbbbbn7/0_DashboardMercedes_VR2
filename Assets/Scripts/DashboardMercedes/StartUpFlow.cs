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
            //ICameraFeature cameraFeature = client.Features.Get<ICameraFeature>();
            //IPlayerFeature playerFeature = client.Features.Get<IPlayerFeature>();
            //ILevelFeature levelFeature = client.Features.Get<ILevelFeature>();
            //INPCFeature npcFeature = client.Features.Get<INPCFeature>();
            ISoundFeature soundFeature = client.Features.Get<ISoundFeature>();
            IMainMenuFeature menuFeature = client.Features.Get<IMainMenuFeature>();
            ITurnOnCarFeature turnOnCar = client.Features.Get<ITurnOnCarFeature>();
            IInitializationController initController = client.Controllers.Get<IInitializationController>();


            initController.InitializeFeature();
            await initController.LoadFeature();
            initController.OnFeatureLoadedAndInitialized();

            //await turnOnCar.InstantiateTurnOnCar();
            await menuFeature.InstantiateMainMenu();

            //await levelFeature.InstantiateLevel();
            //await playerFeature.InstantiatePlayer();
            //await npcFeature.InstantiateNPC(1);
        }
    }
}

using TMPro;
using UnityEngine.UI;
using UnityEngine;

namespace DashboardMercedes
{
    //public class ClimaData
    //{
    //    public int BlowPower;
    //    public string Temperature;
    //}
    public class AppState : DashboardBaseState
    {
        private RawImage ClimaColorBack;
        private Button Hotter;
        private Button Colder;
        private Button Stronger;
        private Button Weaker;
        private Button Back;
        private Button Home;
        private TextMeshProUGUI Text;
        private string ClimaDataPath;
        private GameObject _climaAppObject;
        private GameObject mainMenuObj;
        private int blowValue;
        IBroadcaster clientBroadcaster;


        public AppState(DashboardStateContext context) : base(context)
        {
            clientBroadcaster = _context.Client.Services.Get<IBroadcaster>();
            ClimaColorBack = _context.MyDashboard.ClimaColorBack;
            Hotter = _context.MyDashboard.Hotter;
            Colder = _context.MyDashboard.Colder;
            Stronger = _context.MyDashboard.Stronger;
            Weaker = _context.MyDashboard.Weaker;
            Back = _context.MyDashboard.Back;
            Home = _context.MyDashboard.Home;
            Text = _context.MyDashboard.Text;
            mainMenuObj = _context.mainMenuObj;
            _climaAppObject = _context.climaAppObj;

            Back.onClick.AddListener(goToMenu);
            Home.onClick.AddListener(goToMainMenu);
            Weaker.onClick.AddListener(BlowLess);
            Stronger.onClick.AddListener(BlowMore);
            Colder.onClick.AddListener(TemperatureDown);
            Hotter.onClick.AddListener(TemperatureUp);

            blowValue = 0;
        }

        public override void StateOnEnter()
        {
            Text.text = blowValue.ToString();
        }

        public override void StateOnExit()
        {
            _climaAppObject.SetActive(false);

        }

        public override void StateOnUpdate()
        {
            Text.text = blowValue.ToString();
        }

        public void goToMainMenu()
        {
            _context.DashboardStateMachine.GoTo(DashboardData.MAIN_MENU_STATE);
            clientBroadcaster.Broadcast(new PlayClickSoundEvent());


        }

        public void goToMenu()
        {
            _context.DashboardStateMachine.GoTo(DashboardData.MENU_STATE);
            clientBroadcaster.Broadcast(new PlayClickSoundEvent());

        }

        public void BlowLess()
        {
            if (blowValue > 0)
            {
                blowValue--;
            }
            clientBroadcaster.Broadcast(new PlayClickSoundEvent());

        }
        public void BlowMore()
        {
            if (blowValue < 6)
            {
                blowValue++;
            }
            clientBroadcaster.Broadcast(new PlayClickSoundEvent());

        }
        public void TemperatureDown()
        {
            ClimaColorBack.color = Color.blue;
            clientBroadcaster.Broadcast(new PlayClickSoundEvent());

        }
        public void TemperatureUp()
        {
            ClimaColorBack.color = Color.red;
            clientBroadcaster.Broadcast(new PlayClickSoundEvent());

        }
    }
}
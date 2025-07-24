namespace DashboardMercedes
{
    public class BaseSelfInjectedBehaviour<TFeatureInternal, TFeature> : BaseMonoBehaviour<TFeatureInternal> where TFeatureInternal : IFeatureInternal where TFeature : IFeature
    {
        private void Awake()
        {
            Client client = Client.Instance;

            if (client == null)
            {
                return;
            }

            var myFeature = client.Features.Get<TFeature>();

            if (myFeature is TFeatureInternal myFeatureInternal)
            {
                Initialize(myFeatureInternal);
            }
        }
    }
}

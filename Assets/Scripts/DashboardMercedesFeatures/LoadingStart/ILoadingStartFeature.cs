using System.Threading.Tasks;
using DashboardMercedes;

public interface ILoadingStartFeature : IFeature 
{
    public Task InstantiateLoadingStart();
}
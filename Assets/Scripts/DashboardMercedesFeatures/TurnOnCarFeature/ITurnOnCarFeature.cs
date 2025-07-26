using System.Threading.Tasks;
using DashboardMercedes;

public interface ITurnOnCarFeature : IFeature
{
    public Task InstantiateTurnOnCarFeature();
}
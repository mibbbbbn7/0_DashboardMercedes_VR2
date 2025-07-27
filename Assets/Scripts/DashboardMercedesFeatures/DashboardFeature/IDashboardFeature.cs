using System.Threading.Tasks;
using DashboardMercedes;

public interface IDashboardFeature : IFeature
{
    public Task InstantiateDashboardFeature();
}
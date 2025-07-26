using System.Threading.Tasks;
using DashboardMercedes;

public interface ICarFSMFeature : IFeature
{
    public Task InstantiateCarFSMFeature();
}
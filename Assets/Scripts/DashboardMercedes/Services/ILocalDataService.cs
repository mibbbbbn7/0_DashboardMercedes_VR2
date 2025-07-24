using System;
using System.Threading.Tasks;

public interface ILocalDataService : IService
{
    public bool DoesFileExist(string fileName);

    public Task SaveLocalData<T>(string fileName, T data, Action onSuccess, Action<Exception> onFailure = null);
    public Task LoadLocalData<T>(string fileName, Action<T> onSuccess, Action<Exception> onFailure = null);
}

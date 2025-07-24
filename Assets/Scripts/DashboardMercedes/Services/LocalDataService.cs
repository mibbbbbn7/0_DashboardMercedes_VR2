using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

public class LocalDataService : ILocalDataService
{
    public bool DoesFileExist(string fileName)
    {
        var path = Path.Combine(Application.persistentDataPath, fileName);
        return File.Exists(path);
    }

    public async Task LoadLocalData<T>(string fileName, Action<T> onSuccess, Action<Exception> onFailure = null)
    {
        try
        {
            string path = Path.Combine(Application.persistentDataPath, fileName);
            string data = "";
            using (var reader = new StreamReader(path))
            {
                var task = reader.ReadToEndAsync();
                data = await task;
                reader.Close();

                if (task.IsCanceled || task.IsFaulted)
                {
                    var exception = new Exception(task.Exception.ToString());
                    Debug.LogException(exception);
                    onFailure?.Invoke(exception);
                    return;
                }
            }

            T myData = JsonConvert.DeserializeObject<T>(data);

            onSuccess?.Invoke(myData);
        }
        catch (Exception e)
        {
            Debug.LogException(e);
            onFailure?.Invoke(e);
        }
    }

    public async Task SaveLocalData<T>(string fileName, T myData, Action onSuccess, Action<Exception> onFailure = null)
    {
        try
        {
            string path = Path.Combine(Application.persistentDataPath, fileName);

            var data = JsonConvert.SerializeObject(myData);

            using (var writer = new StreamWriter(path))
            {
                var task = writer.WriteAsync(data);
                await task;
                writer.Close();

                if (task.IsCanceled || task.IsFaulted)
                {
                    var exception = new Exception("something went wrong");
                    Debug.LogException(exception);
                    onFailure?.Invoke(exception);
                    return;
                }
            }

            onSuccess?.Invoke();
        }
        catch (Exception e)
        {
            Debug.LogException(e);
            onFailure?.Invoke(e);
        }
    }
}

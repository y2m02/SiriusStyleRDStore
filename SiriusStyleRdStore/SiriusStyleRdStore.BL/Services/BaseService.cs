using System;
using System.Threading.Tasks;
using SiriusStyleRdStore.Entities.Responses;
using SiriusStyleRdStore.Entities.ViewModels;

namespace SiriusStyleRdStore.BL.Services
{
    public class BaseService
    {
        protected async Task<IViewModel> HandleErrors(Func<Task<IViewModel>> response)
        {
            try
            {
                return await response.Invoke();
            }
            catch (Exception ex)
            {
                return new Error(ex.Message);
            }
        }

        protected async Task<IViewModel> HandleErrors<T>(Func<T, Task<IViewModel>> response, T request)
        {
            try
            {
                return await response.Invoke(request);
            }
            catch (Exception ex)
            {
                return new Error(ex.Message);
            }
        }

        protected IViewModel Success<T>(T response)
        {
            return new Success<T>
            {
                Response = response
            };
        }
    }
}
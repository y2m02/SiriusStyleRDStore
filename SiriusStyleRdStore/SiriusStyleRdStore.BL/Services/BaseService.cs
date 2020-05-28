using System;
using System.Threading.Tasks;
using SiriusStyleRdStore.Entities.Responses;

namespace BillingSystem.BL.Services
{
    public class BaseService
    {
        protected async Task<IResponse> HandleErrors(Func<Task<IResponse>> response)
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

        protected async Task<IResponse> HandleErrors<T>(Func<T, Task<IResponse>> response, T request)
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

        protected IResponse Success<T>(T response)
        {
            return new Success<T>
            {
                Response = response
            };
        }
    }
}
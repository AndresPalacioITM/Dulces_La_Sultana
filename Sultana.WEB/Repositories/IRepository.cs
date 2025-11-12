namespace Sultana.WEB.Repositories
{
    public interface IRepository
    {
        Task<HttpResponseWrapper<T>> GetAsync<T>(string url);


        //Request
        Task<HttpResponseWrapper<object>> PostAsync<T>(string url, T model);

        //Response
        Task<HttpResponseWrapper<TResponse>> PostAsync<T, TResponse>(string url, T model);

        //Request
        Task<HttpResponseWrapper<object>> PutAsync<T>(string url, T model);

        //Response
        Task<HttpResponseWrapper<TResponse>> PutAsync<T, TResponse>(string url, T model);

        Task<HttpResponseWrapper<object>> DeleteAsync(string url);



    }
}

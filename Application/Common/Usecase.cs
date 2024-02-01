namespace Application.Common
{
	public abstract class Usecase<TRequest, TResponse> where TRequest : Request where TResponse : Response
	{
		public abstract Task ExecuteAsync(TRequest request, IPresenter<TResponse> presenter);
	}
}


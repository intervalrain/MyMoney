using System;
namespace Applications.Common
{
	public interface IPresenter<in TResponse>
	{
		public Task PresentAsync(TResponse response);
	}
}


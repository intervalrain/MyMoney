using Applications.Common;
using Applications.Repositories;
using Domain.Common;

namespace Applications.Usecases
{
	public record ListAllUsersRequest() : Request();

	public record ListAllUsersResponse(params string[] UserIds) : Response();

    public class ListAllUsersUsecase : Usecase<ListAllUsersRequest, ListAllUsersResponse>
	{
		private readonly IUserRepository _repository;
		private readonly IEventBus<DomainEvent> _eventBus;

		public ListAllUsersUsecase(IUserRepository repository, IEventBus<DomainEvent> eventBus)
		{
			_repository = repository;
			_eventBus = eventBus;
		}

        public override async Task ExecuteAsync(ListAllUsersRequest request, IPresenter<ListAllUsersResponse> presenter)
        {
			var users = _repository.GetAllUsers();
			var userIds = users.Select(user => user.UserName).ToArray();
			await presenter.PresentAsync(new ListAllUsersResponse(userIds));
        }
    }
}


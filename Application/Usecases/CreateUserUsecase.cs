using Applications.Common;
using Applications.Repositories;
using Domain.Common;

namespace Applications.Usecases
{
    public record CreateUserRequest(string UserName) : Request();

    public record CreateUserResponse(int UserId) : Response();

    public class CreateUserUsecase : Usecase<CreateUserRequest, CreateUserResponse>
	{
		private readonly IUserRepository _repository;
        private readonly IEventBus<DomainEvent> _eventBus;

        public CreateUserUsecase(IUserRepository repository, IEventBus<DomainEvent> eventBus)
		{
			_repository = repository;
            _eventBus = eventBus;
        }

        public override async Task ExecuteAsync(CreateUserRequest request, IPresenter<CreateUserResponse> presenter)
        {
            var userId = _repository.Create(request.UserName);
            await presenter.PresentAsync(new CreateUserResponse(userId));
        }
    }
}


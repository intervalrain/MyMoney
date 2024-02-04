using Applications.Common;
using Applications.Repositories;
using Domain;
using Domain.Common;

namespace Applications.Usecases
{
    public record CreateUserRequest(string UserId) : Request();

    public record CreateUserResponse(string UserId) : Response();

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
            User user = new User(request.UserId);
            _repository.Save(user);
            await presenter.PresentAsync(new CreateUserResponse(user.UserName));
        }
    }
}


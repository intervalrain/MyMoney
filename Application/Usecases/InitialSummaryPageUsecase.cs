using Applications.Common;
using Applications.Repositories;
using Domain.Common;

namespace Applications.Usecases
{
    public record InitialSummaryPageRequest(string UserId) : Request();

    public record InitialSummaryPageResponse(string UserId) : Response();

    public class InitialSummaryPageUsecase : Usecase<InitialSummaryPageRequest, InitialSummaryPageResponse>
    {
		private readonly IUserRepository _repository;
        private readonly IEventBus<DomainEvent> _eventBus;

        public InitialSummaryPageUsecase(IUserRepository repository, IEventBus<DomainEvent> eventBus)
		{
			_repository = repository;
            _eventBus = eventBus;
        }

        public override Task ExecuteAsync(InitialSummaryPageRequest request, IPresenter<InitialSummaryPageResponse> presenter)
        {
            throw new NotImplementedException();
        }
    }
}


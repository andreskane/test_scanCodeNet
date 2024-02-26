using Application.Dto;
using Application.Helper;
using Application.Interfaces.Repositories;
using Application.RequestModels.CommandRequestModels.ListValue;
using Application.ResponseModels.CommandResponseModels.ListValue;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Handlers.CommandHandlers.ListValue
{
    public class UpdateListValueCommandHandler : IRequestHandler<UpdateListValueCommandRequest, UpdateListValueCommandResponse>
    {
        private readonly ILogger<CreateListValueCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IListValueRepository _repositoryAsync;

        public UpdateListValueCommandHandler(ILogger<CreateListValueCommandHandler> logger,
                                             IMapper mapper,
                                             IListValueRepository repositoryAsync
                                            )
        {
            _logger = logger;
            _mapper = mapper;
            _repositoryAsync = repositoryAsync;

        }

        public async Task<UpdateListValueCommandResponse> Handle(UpdateListValueCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {

                var listID = request.ListValues.FirstOrDefault().ListId;
                if (listID == null)
                    throw new Exception("ListId is required");
                var response = new UpdateListValueCommandResponse();

                var listValuesParam = _mapper.Map<List<Domain.Entities.ListAggregate.ListValue>>(request.ListValues).ToList();

                var listValues = await _repositoryAsync.GetByListIdAsync(listID, cancellationToken);


                //Delete missing elemnts
                var listValuesToDelete = listValues.Except(listValuesParam, new ListValueComparer()).ToList();

                foreach (var value in listValuesToDelete)
                    await _repositoryAsync.DeleteListValueAsync(value, cancellationToken);


                //Add new elements
                var listValuesToAdd = listValuesParam.Except(listValues, new ListValueComparer()).ToList();

                await _repositoryAsync.AddRangeListValueAsync(listValuesToAdd.ToList(), cancellationToken);


                //Update existing ones
                var listValuesToUpdate = listValuesParam.Where(x => listValues.Any(y => y.Key == x.Key)).ToList();

                foreach (var item in listValuesToUpdate)
                {
                    var entityToUpdate = listValues.Where(x => x.Key == item.Key).FirstOrDefault();

                    item.Id = entityToUpdate.Id;
                    await _repositoryAsync.UpdateListValueAsync(item, cancellationToken);
                }


                response.ListValues = _mapper.Map<List<ListValueDto>>(listValues);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                throw ex;
            }

        }
    }
}

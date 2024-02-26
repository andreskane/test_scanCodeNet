using Application.Dto;
using Application.ResponseModels.CommandResponseModels.ListDefinition;
using MediatR;

namespace Application.RequestModels.CommandRequestModels.ListDefinition
{
    public class CreateListDefinitionCommandRequest : IRequest<CreateListDefinitionCommandResponse>
    {
        public ListDefinitionDto ListDefinition { get; set; }
    }
}

using Application.Dto.Params.DynamicForm;
using Application.ResponseModels.QueriesResponseModels;
using ConnectureOS.Framework.Message;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Application.RequestModels.QueriesRequestModels
{
    public class GetDynamicFormTemplateByFiltersQueryRequest : IRequest<GetDynamicFormTemplateByFiltersQueryResponse>
    {
        [DataMember]
        [Required(ErrorMessage = ErrorMessageText.Required)]
        public FilterDynamicForm Filter { get; set; }
    }
}

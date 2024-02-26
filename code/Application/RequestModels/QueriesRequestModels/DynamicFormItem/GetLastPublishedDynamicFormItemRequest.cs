using Application.ResponseModels.QueriesResponseModels;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Application.RequestModels.QueriesRequestModels
{
    public class GetLastPublishedDynamicFormItemRequest : IRequest<GetDynamicFormItemResponse>
    {
        [DataMember]
        [Required(ErrorMessage = "Product Id is required.")]
        public long ProductId { get; set; }

        [DataMember]
        [Required(ErrorMessage = "ZipCode is required.")]
        public string ZipCode { get; set; }
    }
}

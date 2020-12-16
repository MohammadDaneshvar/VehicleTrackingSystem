using Framework.Domain.Resource;
using System.ComponentModel.DataAnnotations;

namespace Framework.Domain.Enum
{
    public enum StatusEnum
    {
        [Display(Name = nameof(UnkownError), ResourceType = typeof(Status))]
        UnkownError = 0,
        [Display(Name = nameof(InvalidModelState), ResourceType = typeof(Status))]
        InvalidModelState = 1,
        [Display(Name = nameof(Unauthorized), ResourceType = typeof(Status))]
        Unauthorized = 2,
        [Display(Name = nameof(Forbidden), ResourceType = typeof(Status))]
        Forbidden = 403,
        [Display(Name = nameof(NotFound), ResourceType = typeof(Status))]
        NotFound = 404,
        [Display(Name = nameof(EmptyData), ResourceType = typeof(Status))]
        EmptyData = 405,
        [Display(Name = nameof(SignatureIsNotValid), ResourceType = typeof(Status))]
        SignatureIsNotValid = 406,

        [Display(Name = nameof(DuplicationData), ResourceType = typeof(Status))]
        DuplicationData = 407,


        [Display(Name = nameof(CanNotCharge), ResourceType = typeof(Status))]
        CanNotCharge = 406,
        [Display(Name = nameof(OrganizationLimitRequest), ResourceType = typeof(Status))]
        OrganizationLimitRequest = 407,
        [Display(Name = nameof(InvalidAccount), ResourceType = typeof(Status))]
        InvalidAccount = 408,
        [Display(Name = nameof(StockNotEnough), ResourceType = typeof(Status))]
        StockNotEnough = 409,
        [Display(Name = nameof(NationalCode), ResourceType = typeof(Status))]
        NationalCode = 410,
        [Display(Name = nameof(NationalCodeMinimumLength), ResourceType = typeof(Status))]
        NationalCodeMinimumLength = 411,
        [Display(Name = nameof(InvalidValue), ResourceType = typeof(Status))]
        InvalidValue = 412,
        [Display(Name = nameof(NationalCodeNotFound), ResourceType = typeof(Status))]
        NationalCodeNotFound = 413,
        [Display(Name = nameof(InvalidActionPrice), ResourceType = typeof(Status))]
        InvalidActionPrice = 414,
        [Display(Name = nameof(UseSqlServer), ResourceType = typeof(Status))]
        UseSqlServer = 500,

    }
}

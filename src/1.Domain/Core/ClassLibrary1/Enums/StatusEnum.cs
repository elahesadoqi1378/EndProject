
using System.ComponentModel.DataAnnotations;

namespace Achareh.Domain.Core.Enums
{
    public enum StatusEnum
    {
        [Display(Name = "منتظر پیشنهاد کارشناس")]
        WatingExpertOffer = 1,

        [Display(Name = "منتظر انتخاب کارشناس")]
        WatingForChoosingExpert = 2,

        [Display(Name = "منتظر آمدن کارشناس به لوکیشن شما")]
        WatingExpertComeToYourPlace = 3,

        [Display(Name = " شروع شده توسط کارشناس")]
        WorkStarted = 4,

        [Display(Name = " انجام شده توسط کارشناس")]
        WorkDoneByExpert = 5,

        [Display(Name = " پرداخت شده توسط مشتری")]
        WorkPaidByCustomer = 6
    }
}

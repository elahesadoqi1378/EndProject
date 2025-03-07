
using System.ComponentModel.DataAnnotations;

namespace Achareh.Domain.Core.Enums
{
    public enum StatusEnum
    {
        [Display(Name = "منتظر پیشنهاد کارشناس")]
        WatingExpertOffer = 1,

        [Display(Name = " منتظر انتخاب کارشناس مد نظر مشتری")]  
        WatingForCustomerToChoose = 2,                          

        [Display(Name = " آغاز کار کارشناس")] 
        WorkStarted = 3,

        [Display(Name = " پایان کار کارشناس")]
        WorkDoneByExpert = 4,

        [Display(Name = " پرداخت شده توسط مشتری")]
        WorkPaidByCustomer = 5,

        [Display(Name = " لغو شده")]
        Cancelled = 6,
    }
}

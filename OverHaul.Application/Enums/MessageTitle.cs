using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overhaul.Application.Enums;

public enum MessageTitle
{
    [Description("اطلاعات با موفقیت ثبت شد")]
    AddSuccess = 100,
    [Description("اطلاعات با موفقیت ویرایش شد")]
    EditSuccess = 101,
    [Description("اطلاعات با موفقیت حذف شد")]
    DeleteSuccess = 102,
    [Description("عملیات با موفقیت ثبت شد")]
    SaveSuccess = 103,
    [Description("خطا در ثبت اطلاعات")]
    AddError = 104,
    [Description("خطا در ویرایش اطلاعات")]
    EditError = 105,
    [Description("خطا در حذف اطلاعات")]
    DeleteError = 106,
    [Description("خطا در ثبت عمملیات")]
    SaveError = 107,
    [Description("اطلاعات با موفقیت بازگرداده شد")]
    GetSuccess = 108,
    [Description("رکوردی با این اطلاعات قبلا ثبت شده است")]
    DuplicatedRecordError = 109,
    [Description("تاریخ نمی تواند کوچکتر از امروز باشد")]
    DateCAnNotLessThanToday = 110,
    [Description("کاربر مورد نظر یافت نشد")]
    UserNotFound =111,
    [Description("رمز عبور نادرست است")]
    WrongPassword=112,
    [Description("برای کاربر مورد نظر سطح تعریف نشده است")]
    UserHasNotLevel=113,
    [Description("با موفقیت وارد شد")]
    LoginSuccessFull=114,
    [Description("ارزش افزوده تعریف نشده است")]
    ValueAddedIsNotDefined=115,
    [Description("سال ارزش افزوده فقط یک سال میتواند باشد")]
    AddedValueIsMoreThanaYear=116,
    [Description("این کاربر غیرفعال می باشد")]
    UserIsNotActive = 117,
    [Description("گروه پرسنلی این کاربر غیرفعال می باشد")]
    UserPersonalGroupIsNotActive = 118,
    [Description("شرکت این کاربر غیرفعال می باشد")]
    UserCompanyIsNotActive = 119,
    [Description("خطا در محاسبه سهمیه استفاده شده ماه جاری پرسنل")]
    GetSumQuotaInMounthInternalError = 120,
    [Description("اطلاعات با موفقیت ابطال شد")]
    CancelSuccess = 121,
    [Description("خطا در ابطال اطلاعات")]
    CancelError = 122,
    [Description("سرویس پیاده سازی نشده است")]
    NotImplementedService = 123,
    ///////////
    [Description("خطا در اتصال به بانک اطلاعاتی")]
    DatabaseNotConnect = 1000,
    #region Radif Reserve
    [Description("این ردیف رزرو شده است")]
    RadifReserveDuplicate = 500,
    #endregion
    [Description("اطلاعات یافت نشد")]
    Not_Found = 600,
    [Description("رمز پیامکی صحیح نمی باشد")]
    InvalidSmsCode = 602,
    [Description("این رکورد دارای رکورد فرزند می باشد")]
    HasChild = 606,
    [Description("بیش از مجاز تحت تکفل")]
    MoreThanDependency = 607,
    [Description("صندلی قبلا رزرو شده است")]
    ConfilictChair = 608,
    [Description("برای این کاربر رکرود جزییات ثبت نشده است")]
    UserNotHaveUserDetailsRecord = 609,
    [Description("منویی برای این کاربری تعریف نشده است")]
    UserNotHasMenu = 610,
    [Description("کاربری با این شماره موبایل یافت نشد")]
    UserNotFoundWithThisMobile=611,
    [Description("رمز عبور جدید با موفقیت ایجاد شد")]
    NewPasswordCreatedSuccessfully=612,
    [Description("خطا در ثبت رمز عبور جدید")]
    NewPasswordCreationFailed=613,
    [Description("نوع سوبسید برای این گروه پرسنل تعریف نشده است")]
    SubsidNotDefinedForThisPersonalGriup = 614,
    [Description("این نوع نسبت فقط یک مورد می تواند باشد")]
    DependentRelationTypeMustOne = 615,
    [Description("تاریخ اعتبار نمیتواند در تاریخ امروز و گذشته باشد")]
    ExpireDateIsInValid = 616,

    #region Configuration Part
    [Description("تنظیمات رکورد دستگاه پوز تعریف نشده است")]
    PcPosConfigurationReccordNotFound=700,
    [Description("رکورد تنظیمات دستگاه پوز فعال نمی باشد")]
    PcPosConfigurationReccordNotActive=701,
    [Description("ایستگاه کاری با این شناسه یافت نشد")]
    WorkStationNotFound=702,
    [Description("آدرس آی پی این ایستگاه کاری مغایرت دارد")]
    WorkStationIpNotMached = 703,
    [Description("چاپگر برای این ایستگاه کاری تعریف نشده است")]
    WorkStationPrinterNotFound=704,
    [Description("چاپگر تعریف شده برای این ایستگاه کاری فعال نمی باشد")]
    WorkStationPrinterNotActive = 705,
    [Description("این ایستگاه کاری فعال نمی باشد")]
    WorkStationNotActive = 705,
    #endregion

    #region Report Part
    [Description("فایل گزارش در مسیر فوق یافت نشد")]
    ReportFileNotFound = 800,
    [Description("فایل فونت در مسیر فوق یافت نشد")]
    ReportFontNotFound = 801,
    #endregion
    [Description("هیچ غذایی انتخاب نشده است")]
    NoRestaurantOrderItemSelected = 900,
    [Description("خطا در محاسبه سهمیه و تحت تکفل ")]
    PersonalFoodQuotaInfoNotFound = 901,
    [Description("تعداد افراد تحت تکفل صفر می باشد")]
    RestaurantDependentCountIsZero = 902,
    [Description("سهمیه رستوران باقی مانده این ماه صفر می باشد")]
    RestaurantRemainQuotaIsZero = 903,

}


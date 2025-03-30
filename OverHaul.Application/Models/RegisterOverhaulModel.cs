using Microsoft.AspNetCore.Mvc.Rendering;
using Overhaul.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overhaul.Application.Models;

public class RegisterOverhaulModel
{
    [Required(ErrorMessage = "این فیلد الزامی میباشد")]
    [Display(Name = "نام")]
    public string FirstName { get; set; }
    [Required(ErrorMessage = "این فیلد الزامی میباشد")]
    [Display(Name = "نام خانوادگی")]
    public string LastName { get; set; }
    [Required(ErrorMessage = "این فیلد الزامی میباشد")]
    [Display(Name = "نام پدر")]
    public string FatherName { get; set; }
    [Required(ErrorMessage = "این فیلد الزامی میباشد")]
    [Display(Name = "کدملی")]
    public string Nationality { get; set; }
    [Required(ErrorMessage = "این فیلد الزامی میباشد")]
    [Display(Name = "شماره شناسنامه")]
    public string shSh { get; set; }

    [Required(ErrorMessage = "این فیلد الزامی میباشد")]
    [Display(Name = "تاریخ تولد")]
    public string BirthDay { get; set; }

    [Required(ErrorMessage = "این فیلد الزامی میباشد")]
    [Display(Name = "شماره کارت بانکی")]
    public string BankCardNo { get; set; }
    [Required(ErrorMessage = "این فیلد الزامی میباشد")]
    [Display(Name = "شماره حساب بانکی")]
    public string IBAN { get; set; }

    [Display(Name = "شماره بیمه ")]
    public string SSN { get; set; }

    [Required(ErrorMessage = "این فیلد الزامی میباشد")]
    [Display(Name = " موبایل ")]
    [MaxLength(11)]
    public string Mobile { get; set; }

    [Required(ErrorMessage = "این فیلد الزامی میباشد")]
    [Display(Name = " تلفن ثابت ")]
    public string Tell { get; set; }

    [Required(ErrorMessage = "این فیلد الزامی میباشد")]
    [Display(Name = "کد پستی")]
    [MaxLength(10)]
    public string PostalCode { get; set; }


    [Required(ErrorMessage = "این فیلد الزامی میباشد")]
    [Display(Name = "آدرس")]
    public string Address { get; set; }

    [Required(ErrorMessage = "این فیلد الزامی میباشد")]
    [Display(Name = "مدرک تحصیلی")]
    public string Degree { get; set; }

    [Required(ErrorMessage = "این فیلد الزامی میباشد")]
    [Display(Name = "رشته تحصیلی")]
    public string Major { get; set; }

    [Required(ErrorMessage = "این فیلد الزامی میباشد")]
    [Display(Name = "وضعیت")]
    public bool State { get; set; }

    [Required(ErrorMessage = "این فیلد الزامی میباشد")]
    [Display(Name = " وضعیت نظام وظیفه ")]
    public int MilitaryCardTypeId { get; set; }
    public SelectList SelectListMilitaryCardType { get; set; }

    [Required(ErrorMessage = "این فیلد الزامی میباشد")]
    [Display(Name = "وضعیت تاهل")]
    public int MaritalStatusTypeId { get; set; }
    public SelectList SelectListMaritalStatusType { get; set; }


    [Display(Name = "وضعیت ایثارگری")]
    public int IsargariTypeId { get; set; }
    public SelectList SelectListIsargariType { get; set; }


}
public static partial class Extensions
{
    public static RegisterOverhaul Map(this RegisterOverhaulModel model)
    {
        return new RegisterOverhaul
        {
            Nationality = model.Nationality,
            FatherName = model.FatherName,
            Mobile = model.Mobile,
            MilitaryCardTypeId = model.MilitaryCardTypeId,
            Address = model.Address,
            BankCardNo = model.BankCardNo,
            BirthDay = model.BirthDay.JalaliToGregorianDateOnly(),
            Degree = model.Degree,
            FirstName = model.FirstName,
            IBAN = model.IBAN,
            IsargariTypeId = model.IsargariTypeId,
            LastName = model.LastName,
            Major = model.Major,
            MaritalStatusTypeId = model.MaritalStatusTypeId,
            PostalCode = model.PostalCode,
            shSh = model.shSh,
            SSN = model.SSN,
            Tell = model.Tell,
            State = model.State,





        };

    }











}
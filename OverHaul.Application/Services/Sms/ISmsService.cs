using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Overhaul.Application.AutoFac;
using Overhaul.Application;
using Overhaul.Application.Contracts;
using Overhaul.Application.Enums;
using Overhaul.Application.Models.Sms;
using Overhaul.Domain.Repositories;
using Overhaul.Domain.Entities;

namespace Overhaul.Application.Services.Sms
{
    public interface ISmsService : ITransientDependency
    {
        public bool SendAsync(SmsModel model, CancellationToken cancellationToken = default);
        public SmsCodeResults SendLoginCodeAsync(string mobile, CancellationToken cancellationToken = default);
        public SmsCodeResults SendGeneratedPasswordToUserAsync(string mobile,string password, CancellationToken cancellationToken = default);
    }
    public class SmsService
        (
        //IRepository<AppUser> _userRepository,
        IUnitOfWork unitOfWork,
        ISmsProvider _smsProvider
        ) : ISmsService
    {

        public bool SendAsync(SmsModel model, CancellationToken cancellationToken = default)
        {
            var _userRepository = unitOfWork.Repository<AppUser>();
            var user = _userRepository.Get(u => u.PhoneNumber == model.Mobile.MobileWithoutZero() || u.PhoneNumber == model.Mobile.MobileWithZero());
            if (user is null)
            {
                return false;
            }
            var smsText = model.Text;
            _smsProvider.SendSms("farzad.baradaran@gmail.com", "Tpc#2922", "30007957951793", new string[] { user.PhoneNumber.MobileWithZero() }, smsText);
            return true;
        }

        public SmsCodeResults SendGeneratedPasswordToUserAsync(string mobile, string password, CancellationToken cancellationToken = default)
        {
            _smsProvider.SendSms("farzad.baradaran@gmail.com", "Tpc#2922", "30007957951793", new string[] { mobile },$"رمز عبور شما در سامانه مجتمع رفاهی پتروشیمی تبریز : {password}");
            return SmsCodeResults.SentSuccessfully;
        }

        public SmsCodeResults SendLoginCodeAsync(string mobile, CancellationToken cancellationToken = default)
        {
            var _userRepository = unitOfWork.Repository<AppUser>();
            Random rnd = new Random();
            var newCode = rnd.Next(10000, 99999).ToString();
            var mob1 = mobile.Fa2En().MobileWithoutZero();
            var mob2 = mobile.Fa2En().MobileWithZero();
            var user = _userRepository.Get(u => u.PhoneNumber == mob1 || u.PhoneNumber == mob2);
            if (user is null)
            {
                return SmsCodeResults.UserNotFound;
            }
            user.LoginCode = newCode;
            var smsText = $"کد تایید سامانه مجتمع رفاهی پتروشیمی تبریز: {newCode}";
            _userRepository.Update(user);
            unitOfWork.SaveChanges();
            _smsProvider.SendSms("farzad.baradaran@gmail.com", "Tpc#2922", "30007957951793", new string[] { user.PhoneNumber.MobileWithZero() }, smsText);
            return SmsCodeResults.SentSuccessfully;
        }
    }
}

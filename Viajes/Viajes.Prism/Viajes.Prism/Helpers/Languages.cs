﻿using System.Globalization;
using Viajes.Prism.Interfaces;
using Viajes.Prism.Resources;
using Xamarin.Forms;

namespace Viajes.Prism.Helpers
{
    public static class Languages
    {
        static Languages()
        {
            CultureInfo ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            Resource.Culture = ci;
            Culture = ci.Name;
            DependencyService.Get<ILocalize>().SetLocale(ci);
        }

        public static string Culture { get; set; }

    

        public static string End => Resource.End;

        public static string Source => Resource.Source;

      

        public static string ConfirmNewPassword => Resource.ConfirmNewPassword;

        public static string ConfirmNewPasswordError => Resource.ConfirmNewPasswordError;

        public static string ConfirmNewPasswordError2 => Resource.ConfirmNewPasswordError2;

        public static string ConfirmNewPasswordPlaceHolder => Resource.ConfirmNewPasswordPlaceHolder;

        public static string CurrentPassword => Resource.CurrentPassword;

        public static string CurrentPasswordError => Resource.CurrentPasswordError;

        public static string CurrentPasswordPlaceHolder => Resource.CurrentPasswordPlaceHolder;

        public static string NewPassword => Resource.NewPassword;

        public static string NewPasswordError => Resource.NewPasswordError;

        public static string NewPasswordPlaceHolder => Resource.NewPasswordPlaceHolder;

        public static string UserUpdated => Resource.UserUpdated;

        public static string Save => Resource.Save;

        public static string ChangePassword => Resource.ChangePassword;

        public static string PasswordRecover => Resource.PasswordRecover;

        public static string ForgotPassword => Resource.ForgotPassword;

        public static string PictureSource => Resource.PictureSource;

        public static string Cancel => Resource.Cancel;

        public static string FromCamera => Resource.FromCamera;

        public static string FromGallery => Resource.FromGallery;

        public static string Ok => Resource.Ok;
        public static string MyTrip => Resource.MyTrip;

        public static string MyTrips => Resource.MyTrips;

        public static string Phone => Resource.Phone;

        public static string PhoneError => Resource.PhoneError;

        public static string PhonePlaceHolder => Resource.PhonePlaceHolder;

        public static string RegisterAs => Resource.RegisterAs;
        public static string Register => Resource.Register;
        public static string RegisterNewUser => Resource.RegisterNewUser;

        public static string RegisterAsError => Resource.RegisterAsError;

        public static string RegisterAsPlaceHolder => Resource.RegisterAsPlaceHolder;

        public static string PasswordConfirm => Resource.PasswordConfirm;
        public static string successful => Resource.successful;
        public static string PasswordConfirmError1 => Resource.PasswordConfirmError1;

        public static string PasswordConfirmError2 => Resource.PasswordConfirmError2;

        public static string PasswordConfirmPlaceHolder => Resource.PasswordConfirmPlaceHolder;

        public static string User => Resource.User;

        public static string DocumentError => Resource.DocumentError;

        public static string FirstNameError => Resource.FirstNameError;

        public static string LastNameError => Resource.LastNameError;

        public static string Logout => Resource.Logout;

        public static string Email => Resource.Email;

        public static string LoginError => Resource.LoginError;

        public static string EmailPlaceHolder => Resource.EmailPlaceHolder;

        public static string EmailError => Resource.EmailError;

        public static string Password => Resource.Password;

        public static string PasswordError => Resource.PasswordError;

        public static string PasswordPlaceHolder => Resource.PasswordPlaceHolder;

        
        public static string StartTrip => Resource.StartTrip;

        public static string NewTrip => Resource.NewTrip;

        public static string SeeTripHistory => Resource.SeeTripHistory;

 

        public static string ModifyUser => Resource.ModifyUser;


        public static string Login => Resource.LogIn;

        public static string Id => Resource.Id;

        public static string CheckTrip => Resource.CheckTrip;

      
        public static string Admin => Resource.Admin;

        public static string StartDate => Resource.StartDate;

   
        public static string Loading => Resource.Loading;

        public static string Accept => Resource.Accept;

        public static string ConnectionError => Resource.ConnectionError;
        public static string Category => Resource.Category;
        public static string Costs => Resource.Costs;
         public static string Value => Resource.Value;

        public static string Error => Resource.Error;
        public static string EndDate => Resource.EndDate;
        public static string Destiny => Resource.Destiny;
        public static string Description => Resource.Description;
        public static string Origin => Resource.Origin;
        public static string TripHistory => Resource.TripHistory;
        public static string AddDetailstoTrip => Resource.AddDetailstoTrip;
        

    }
}

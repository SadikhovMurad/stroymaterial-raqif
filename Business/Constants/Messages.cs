using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class Messages
    {
        public static string AuthorizationDenied = "Giriş qadağandır";
        public static string UserRegistered = "Qeydiyyat uğurla yekunlaşdı";
        public static string UserNotFound = "İstifadəçi mövcud deyildir";
        public static string PasswordError = "İstifadəçi adı və ya şifrə yanlışdır";
        public static string SuccessfulLogin = "Login uğurla yekunlaşdı";
        public static string UserAlreadyExists = "Bu email-də istifadəçi mövcuddur";
        public static string AccessTokenCreated = "Token yaradıldı";

        public static string SuccessfulLoginForTeacher = "Qeydiyyat uğurla yekunlaşdı. Təstiqlənəndən sonra email hesabınıza təstiq mesajı gələcəkdir.";


        public static string CategoryListed = "Kateqoriya uğurla Listələndi";
        public static string CategoryListedWithSubcategories = "Kateqoriya Alt kateqoriyalari ile birge listələndi";
        public static string CategoryNameExisted = "Bu adda kateqoriya hal-hazırda mövcuddur";
        public static string CategoryAdded = "Kateqoriya əlavə olundu";
        public static string CategoryUpdated = "Kateqoriya yeniləndi";
        public static string CategoryDeleted = "Kateqoriya silindi";
        public static string IdNotEntered = "Bu Id-də heç bir kateqoriya mövcud deyil";
        public static string IdNullCategory = "Id daxil edilməyib";

        public static string SubCategoryListed = "Alt Kateqoriya uğurla Listələndi";
        public static string SubCategoriesListedWithBaseCategory = "Bütün Alt kateqoriyalar üst kateqoriyaları ilə birlikdə listələndi";
        public static string SubCategoryListedWithBaseCategory = "Alt kateqoriya üst kateqoriya ilə birlikdə listələndi";
        public static string SubCategoryNameExisted = "Bu adda alt kateqoriya hal-hazırda mövcuddur";
        public static string SubCategoryAdded = "Alt Kateqoriya əlavə olundu";
        public static string SubCategoryUpdated = "Alt Kateqoriya yeniləndi";
        public static string IdNotEnteredSub = "Bu Id-də heç bir alt kateqoriya mövcud deyil";
        public static string SubCategoryDeleted = " Alt Kateqoriya silindi";



        public static string ProductNotFound = "Mehsul tapilmadi";
        public static string ProductAdded = "Mehsul ugurla elave olundu";
        public static string ProductExists = "Bu mehsul hal hazirda sistemde movcuddur";
        public static string ProductListed = "Mehsullar ugurla siyahilandi";
        public static string ProductListedByCategoryId = "Mehsullar kateqoriya ID sine gore ugurla siyahilandi";
        public static string ProductListedBySubcategoryId = "Mehsullar alt katqoriya ID sine gore ugurl siyahilandi";
        public static string GetProductById = "Mehsul ID sine gore ugurla getirildi";
        public static string ProductRemoved = "Mehsul ugurla silindi";
        public static string ProductUpdated = "Mehsul ugurla yenilendi";
        public static string ProductQuantityAdded = "Mehsulun sayi ugurla artirildi";

        

    }
}

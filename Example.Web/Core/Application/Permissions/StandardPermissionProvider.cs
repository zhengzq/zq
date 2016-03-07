//using System.Collections.Generic;
//using Zq.Domain.Permissions;

//namespace Zq.Application.Permissions
//{
//    public partial class StandardPermissionProvider
//    {

//        //admin area permissions
//        public static readonly PermissionRecord RoleShow = new PermissionRecord
//        {
//            Code = "sys.user.role.show",
//            Name = "显示(Show)",
//            NavSystemName = "sys.user.role",
//            Type = PermissionType.导航
//        };

//        public static string s = "1";

//        public virtual IEnumerable<PermissionRecord> GetPermissions()
//        {
//            return new[] 
//            {
//                AccessAdminPanel,
//                AllowCustomerImpersonation,
//                ManageProducts,
//                ManageCategories,
//                ManageManufacturers,
//                ManageProductReviews,
//                ManageProductTags,
//                ManageAttributes,
//                ManageCustomers,
//                ManageVendors,
//                ManageCurrentCarts,
//                ManageOrders,
//                ManageRecurringPayments,
//                ManageGiftCards,
//                ManageReturnRequests,
//                OrderCountryReport,
//                ManageAffiliates,
//                ManageCampaigns,
//                ManageDiscounts,
//                ManageNewsletterSubscribers,
//                ManagePolls,
//                ManageNews,
//                ManageBlog,
//                ManageWidgets,
//                ManageTopics,
//                ManageForums,
//                ManageMessageTemplates,
//                ManageCountries,
//                ManageLanguages,
//                ManageSettings,
//                ManagePaymentMethods,
//                ManageExternalAuthenticationMethods,
//                ManageTaxSettings,
//                ManageShippingSettings,
//                ManageCurrencies,
//                ManageMeasures,
//                ManageActivityLog,
//                ManageAcl,
//                ManageEmailAccounts,
//                ManageStores,
//                ManagePlugins,
//                ManageSystemLog,
//                ManageMessageQueue,
//                ManageMaintenance,
//                HtmlEditorManagePictures,
//                ManageScheduleTasks,
//                DisplayPrices,
//                EnableShoppingCart,
//                EnableWishlist,
//                PublicStoreAllowNavigation,
//            };
//        }

//    }
//}
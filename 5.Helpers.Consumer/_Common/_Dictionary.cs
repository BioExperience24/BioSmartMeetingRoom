namespace _5.Helpers.Consumer._Common
{
    public class _Dictionary
    {
        public static readonly Dictionary<string, string> ModuleAliasDictionary = new Dictionary<string, string>
        {
            { "module_pantry", "pantry" },
            { "module_automation", "automation" },
            { "module_price", "price" },
            { "module_int_365", "int_365" },
            { "module_int_google", "int_google" },
            { "module_room_advance", "room_adv" },
            { "module_user_vip", "vip" },
            { "module_invoice", "invoice" },
            { "module_email", "email" },
            { "module_loker", "loker" },
            { "module_display", "display" },
        };

        public static readonly Dictionary<int, string> PantryTransaksiOrderStatusDictionary = new Dictionary<int, string>
        {
            {0, "Order not yet processed"},
            {1, "Order processed"},
            {2, "Order Delivered"},
            {3, "Order Completed"},
            {4, "Order canceled"},
            {5, "Order rejected"}
        };

        public static readonly Dictionary<int, string> StatusCodeMessage = new Dictionary<int, string>
        {
            {400, "Invalid request. Please check your input and try again."},
            {403, "Access denied. You do not have permission to access this resource."},
            {404, "Sorry, the page you are looking for could not be found."},
            {500, "Something went wrong on our end. Please try again later."},
            {502, "The server received an invalid response. Please try again later."},
            {503, "The service is currently unavailable. Please try again later."}
        };
    }
}
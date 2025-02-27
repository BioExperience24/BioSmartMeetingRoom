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
            { "module_invoice", "invoice" }
        };

        public static readonly Dictionary<int, string> PantryTransaksiOrderStatusDictionary = new Dictionary<int, string>
        {
            { 0, "Processing" },
            { 1, "Completed" },
            { 2, "Failed" },
            { 3, "Done" }
        };
    }
}
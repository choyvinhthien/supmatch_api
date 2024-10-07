namespace eStore.Helpers
{
    public class frmFilterProduct
    {
        public int minFilter {  get; set; }
        public int maxFilter {  get; set; }
        public string? rateFilter { get; set; } = string.Empty;
        public string? categoryFilter { get; set; } = string.Empty;
    }
}

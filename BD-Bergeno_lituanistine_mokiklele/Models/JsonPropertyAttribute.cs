namespace BD_Bergeno_lituanistine_mokiklele.Platforms.Models {
    internal class JsonPropertyAttribute : Attribute {
        private string v;

        public JsonPropertyAttribute(string v) {
            this.v = v;
        }
    }
}
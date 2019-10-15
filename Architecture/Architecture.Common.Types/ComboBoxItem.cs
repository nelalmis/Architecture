namespace Architecture.Common.Types
{
    public partial class ComboBoxItem
    {
        public string Text { get; set; }
        public byte Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}

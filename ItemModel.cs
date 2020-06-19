namespace Relocation
{
    public sealed class ItemModel : ModelBase
    {
        private bool _isSelected;

        public ItemModel(CategoryModel category, string description, int points)
        {
            (this.Category, this.Description, this.Points) = (category, description, points);
        }

        public CategoryModel Category { get; }

        public string Description { get; }

        public bool IsSelected
        {
            get => this._isSelected;
            set => this.SetValue(ref this._isSelected, value);
        }

        public int Points { get; }
    }
}

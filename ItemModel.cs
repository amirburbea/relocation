using System;

namespace Relocation
{
    public sealed class ItemModel
    {
        private bool _isSelected;

        public ItemModel(CategoryModel category, string description, int points)
        {
            (this.Category, this.Description, this.Points) = (category, description, points);
        }

        public event EventHandler? IsSelectedChanged;

        public CategoryModel Category { get; }

        public string Description { get; }

        public bool IsSelected
        {
            get => this._isSelected;
            set
            {
                this._isSelected = value;
                this.IsSelectedChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public int Points { get; }
    }
}
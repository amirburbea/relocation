using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Relocation
{
    public sealed class CategoryModel
    {
        private readonly List<ItemModel> _items = new List<ItemModel>();
        private int _points;

        public CategoryModel(string name) => this.Name = name;

        public event EventHandler? PointsChanged;

        public IReadOnlyList<ItemModel> Items => this._items;

        public string Name { get; }

        public int Points
        {
            get => _points;
            private set
            {
                this._points = value;
                this.PointsChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        internal void AddItem(string description, int points)
        {
            ItemModel model = new ItemModel(this, description, points);
            model.IsSelectedChanged += this.Item_IsSelectedChanged;
            this._items.Add(model);
        }

        private void Item_IsSelectedChanged([DisallowNull] object? sender, EventArgs e)
        {
            ItemModel item = (ItemModel)sender;
            this.Points += (item.IsSelected ? 1 : -1) * item.Points;
        }
    }
}
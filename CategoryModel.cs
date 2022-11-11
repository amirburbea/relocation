using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Relocation;

public sealed class CategoryModel : ModelBase
{
    private readonly List<ItemModel> _items = new();
    private ItemModel? _selectedItem;

    public CategoryModel(string name) => this.Name = name;

    public IReadOnlyList<ItemModel> Items => this._items;

    public string Name { get; }

    public int Points => this._selectedItem == null ? 0 : this._selectedItem.Points;

    public ItemModel? SelectedItem
    {
        get => this._selectedItem;
        private set
        {
            if (this.SetValue(ref this._selectedItem, value))
            {
                this.OnPropertyChanged(nameof(this.Points));
            }
        }
    }

    internal void AddItem(string description, int points)
    {
        ItemModel item = new(this, description, points);
        PropertyChangedEventManager.AddHandler(item, this.Item_IsSelectedChanged, nameof(item.IsSelected));
        this._items.Add(item);
    }

    private void Item_IsSelectedChanged(object? sender, EventArgs e)
    {
        ItemModel item = (ItemModel)sender!;
        if (item.IsSelected)
        {
            if (this._selectedItem != null)
            {
                PropertyChangedEventManager.RemoveHandler(this._selectedItem, this.Item_IsSelectedChanged, nameof(item.IsSelected));
                this._selectedItem.IsSelected = false;
                PropertyChangedEventManager.AddHandler(this._selectedItem, this.Item_IsSelectedChanged, nameof(item.IsSelected));
            }
            this.SelectedItem = item;
        }
        else if (item == this._selectedItem)
        {
            this.SelectedItem = null;
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using Microsoft.VisualBasic.FileIO;

namespace Relocation;

public sealed class MainViewModel : ModelBase
{
    private int _points;

    public MainViewModel()
    {
        this.Categories = this.CreateCategories();
        this.ClearAllCommand = new DelegateCommand(this.ClearAll, () => this.Points != 0);
    }

    public IReadOnlyList<CategoryModel> Categories { get; }

    public DelegateCommand ClearAllCommand { get; }

    public int Points
    {
        get => this._points;
        private set => this.SetValue(ref this._points, value);
    }

    private void Category_PointsChanged(object? sender, EventArgs e) => this.Points = this.Categories.Sum(category => category.Points);

    private void ClearAll()
    {
        foreach (CategoryModel category in this.Categories)
        {
            if (category.SelectedItem is { } item)
            {
                PropertyChangedEventManager.RemoveHandler(category, this.Category_PointsChanged, nameof(category.Points));
                item.IsSelected = false;
                PropertyChangedEventManager.AddHandler(category, this.Category_PointsChanged, nameof(category.Points));
            }
        }
        this.Points = 0;
    }

    private CategoryModel[] CreateCategories()
    {
        Dictionary<string, CategoryModel> categories = new();
        if (!(bool)DesignerProperties.IsInDesignModeProperty.GetMetadata(typeof(Window)).DefaultValue)
        {
            Uri uri = new($"pack://application:,,,/{Assembly.GetExecutingAssembly().GetName().Name};component/items.csv");
            using Stream stream = Application.GetResourceStream(uri).Stream;
            using TextFieldParser parser = new(stream) { Delimiters = new[] { "," } };
            parser.ReadLine(); // Headers
            while (true)
            {
                if (parser.ReadFields() is not [string category, string description, string points])
                {
                    break;
                }
                if (!categories.TryGetValue(category, out CategoryModel? model))
                {
                    categories.Add(category, model = new(category));
                    PropertyChangedEventManager.AddHandler(model, this.Category_PointsChanged, nameof(model.Points));
                }
                model.AddItem(description, int.Parse(points));
            }
        }
        return categories.Values.ToArray();
    }
}
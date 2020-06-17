using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using Microsoft.VisualBasic.FileIO;

namespace Relocation
{
    public sealed class MainViewModel
    {
        private int _points;

        public MainViewModel()
        {
            this.Categories = (bool)DesignerProperties.IsInDesignModeProperty.GetMetadata(typeof(Window)).DefaultValue
                ? Array.Empty<CategoryModel>()
                : this.CreateCategories().ToArray();
        }

        public event EventHandler? PointsChanged;

        public IReadOnlyList<CategoryModel> Categories { get; }

        public int Points
        {
            get => this._points;
            private set
            {
                this._points = value;
                this.PointsChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private void Category_PointsChanged(object? sender, EventArgs e) => this.Points = this.Categories.Sum(item => item.Points);

        private IEnumerable<CategoryModel> CreateCategories()
        {
            Uri uri = new Uri($"pack://application:,,,/{Assembly.GetExecutingAssembly().GetName().Name};component/items.csv");
            using Stream stream = Application.GetResourceStream(uri).Stream;
            using TextFieldParser parser = new TextFieldParser(stream) { Delimiters = new[] { "," } };
            parser.ReadLine(); // Headers
            Dictionary<string, CategoryModel> categories = new Dictionary<string, CategoryModel>();
            while (true)
            {
                string[]? fields = parser.ReadFields();
                if (fields == null || fields.Length != 3)
                {
                    break;
                }
                string categoryName = fields[0].Trim();
                if (!categories.TryGetValue(categoryName, out CategoryModel? category))
                {
                    categories.Add(categoryName, category = new CategoryModel(categoryName));
                    category.PointsChanged += this.Category_PointsChanged;
                }
                category.AddItem(fields[1].Trim(), int.Parse(fields[2].Trim()));
            }
            return categories.Values;
        }
    }
}
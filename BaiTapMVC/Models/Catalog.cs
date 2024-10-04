using System;
using System.Collections.Generic;

namespace BaiTapMVC.Models;

public partial class Catalog
{
    public int Id { get; set; }

    public string? CatalogCode { get; set; }

    public string? CatalogName { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}

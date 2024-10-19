using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;
using QuanLySanPhamAPI.Models;

namespace QuanLySanPhamAPI.Models;

public partial class Product
{
    public int Id { get; set; }

    public int? CatalogId { get; set; }

    public string? ProductCode { get; set; }

    public string? ProductName { get; set; }

    public string? Picture { get; set; }

    public double? UnitPrice { get; set; }

    public virtual Catalog? Catalog { get; set; }
}

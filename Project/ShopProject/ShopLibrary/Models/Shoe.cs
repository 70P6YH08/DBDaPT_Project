using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ShopLibrary.Models;

public partial class Shoe
{
    public int ShoeId { get; set; }

    public string Article { get; set; } = null!;

    public int Price { get; set; }

    public sbyte Discount { get; set; }

    public int Quantity { get; set; }

    public string? Description { get; set; }

    public sbyte? Size { get; set; }

    public string? Color { get; set; }

    public int VendorId { get; set; }

    public int MakerId { get; set; }

    public int CategoryId { get; set; }

    public bool IsFemale { get; set; }

    public string? PhotoName { get; set; }

    [JsonIgnore]
    public virtual Category Category { get; set; } = null!;

    [JsonIgnore]
    public virtual Maker Maker { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<ShoeOrder> ShoeOrders { get; set; } = new List<ShoeOrder>();

    [JsonIgnore]
    public virtual Vendor Vendor { get; set; } = null!;
}

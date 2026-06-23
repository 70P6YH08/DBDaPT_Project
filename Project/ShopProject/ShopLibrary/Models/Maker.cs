using System;
using System.Collections.Generic;

namespace ShopLibrary.Models;

public partial class Maker
{
    public int MakerId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Shoe> Shoes { get; set; } = new List<Shoe>();
}

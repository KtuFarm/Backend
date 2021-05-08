﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models.Common;
using Backend.Models.OrderEntity;
using Backend.Models.ProductBalanceEntity;

namespace Backend.Models.Database
{
    public class OrderProductBalance: ISoftDeletable
    { 
        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int ProductBalanceId { get; set; }
        public ProductBalance ProductBalance { get; set; }

        [DefaultValue(false)] 
        public bool IsSoftDeleted { get; set; } = false;
    }
}
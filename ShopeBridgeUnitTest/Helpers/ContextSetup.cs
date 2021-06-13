using Microsoft.EntityFrameworkCore;
using ShopBridge.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopeBridgeUnitTest.Helpers
{
    public class ContextSetup
    {
        public InventoryContext GetContext()
        {
            var options = new DbContextOptionsBuilder<InventoryContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new InventoryContext(options);
        }
    }
}

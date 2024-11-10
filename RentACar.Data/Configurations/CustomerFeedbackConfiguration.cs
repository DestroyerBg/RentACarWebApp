using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentACar.Data.Models;

namespace RentACar.Data.Configurations
{
    internal class CustomerFeedbackConfiguration : IEntityTypeConfiguration<CustomerFeedback>
    {
        public void Configure(EntityTypeBuilder<CustomerFeedback> builder)
        {
            builder.HasOne(cf => cf.Reservation)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}

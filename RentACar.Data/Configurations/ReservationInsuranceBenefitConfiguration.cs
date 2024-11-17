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
    public class ReservationInsuranceBenefitConfiguration : IEntityTypeConfiguration<ReservationInsuranceBenefit>
    {
        public void Configure(EntityTypeBuilder<ReservationInsuranceBenefit> builder)
        {
            builder.HasKey(pk => new { pk.InsuranceBenefitId, pk.ReservationId });

            builder.HasOne(i => i.InsuranceBenefit)
                .WithMany(r => r.Reservations)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(i => i.Reservation)
                .WithMany(r => r.InsuranceBenefits)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}

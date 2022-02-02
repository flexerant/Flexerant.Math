using System;
using System.Collections.Generic;
using Xunit;
using Flexerant.Math;

namespace TestProject
{
    public class FinancialTests
    {
        [Fact]
        public void NPV_AsDouble()
        {
            // https://corporatefinanceinstitute.com/resources/knowledge/finance/internal-rate-return-irr/

            List<double> cashFlow = new() { -500000, 160000, 160000, 160000, 160000, 50000 };

            var npv = Financial.NPV(cashFlow, 0);

            Assert.Equal(190000, npv.WithDecimalPlaces(2));
        }

        [Fact]
        public void NPV_AsDecimal()
        {
            // https://corporatefinanceinstitute.com/resources/knowledge/finance/internal-rate-return-irr/

            List<decimal> cashFlow = new() { -500000, 160000, 160000, 160000, 160000, 50000 };

            var npv = Financial.NPV(cashFlow, 0);

            Assert.Equal(190000m, npv.WithDecimalPlaces(2));
        }

        [Fact]
        public void IRR_AsDouble()
        {
            // https://corporatefinanceinstitute.com/resources/knowledge/finance/internal-rate-return-irr/

            List<double> cashFlow = new() { -500000, 160000, 160000, 160000, 160000, 50000 };

            var irr = Financial.IRR(cashFlow, 0);

            Assert.Equal(0.13, irr.Value.WithDecimalPlaces(2));
        }

        [Fact]
        public void IRR_AsDecimal()
        {
            List<decimal> cashFlow = new() { -500000, 160000, 160000, 160000, 160000, 50000 };

            var irr = Financial.IRR(cashFlow, 0);

            Assert.Equal(0.13m, irr.Value.WithDecimalPlaces(2));
        }

        [Fact]
        public void IRR_AsDouble_NoConvergence()
        {
            // https://corporatefinanceinstitute.com/resources/knowledge/finance/internal-rate-return-irr/

            List<double> cashFlow = new() { 500000, 160000, 160000, 160000, 160000, 50000 };

            var irr = Financial.IRR(cashFlow, 0);

            Assert.False(irr.HasValue);
        }

        [Fact]
        public void IRR_AsDecimal_NoConvergence()
        {
            // https://corporatefinanceinstitute.com/resources/knowledge/finance/internal-rate-return-irr/

            List<decimal> cashFlow = new() { 500000, 160000, 160000, 160000, 160000, 50000 };

            var irr = Financial.IRR(cashFlow, 0);

            Assert.False(irr.HasValue);
        }
    }
}

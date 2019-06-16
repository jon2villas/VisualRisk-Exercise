using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Moq;
using NPVCalculator.Api.Repositories;
using NPVCalculator.Api.Models;
using NPVCalculator.Api.Controllers;

namespace NPVCalculator.Api.Tests
{
    public class CalculatorControllerTests
    {
        [Fact]
        public async void Valid_Parameter_SingleResult_CheckComputation()
        {
            var unitOfWork = new Mock<IUnitOfWork>();
            var parameterRepository = new Mock<IParameterRepository>();
            var calculationRepository = new Mock<ICalculationRepository>();
            
            unitOfWork.Setup(m => m.Parameters).Returns(parameterRepository.Object);
            unitOfWork.Setup(m => m.Calculations).Returns(calculationRepository.Object);
            unitOfWork.Setup(m => m.CompleteAsync()).Returns(Task.FromResult(1));

            var calculator = new CalculatorController(unitOfWork.Object);
            var parameter = new Parameter() 
            {
                Id = 1,
                LowerBoundDiscountRate = 5.00,
                UpperBoundDiscountRate = 5.00,
                DiscountRateIncrement = 0,
                InitialValue = -100000,
                CashFlows = new List<CashFlow>() 
                {
                    new CashFlow(1000),
                    new CashFlow(1000),
                    new CashFlow(1000),
                    new CashFlow(1000),
                    new CashFlow(1000),
                    new CashFlow(1000),
                    new CashFlow(1000),
                    new CashFlow(1000),
                    new CashFlow(1000),
                    new CashFlow(1000)
                }
            };

            var actionResult = await calculator.Calculate(parameter);

            var result = actionResult as OkObjectResult;
            Assert.NotNull(result);

            var calculations = result.Value as List<Calculation>;
            Assert.NotNull(calculations);

            Assert.Equal(-92278.27M, Math.Round(calculations[0].NetPresentValue, 2));
        }

        [Fact]
        public async void Invalid_LowerBoundDiscountRate_BadRequestResult()
        {
            var unitOfWork = new Mock<IUnitOfWork>();
            var parameterRepository = new Mock<IParameterRepository>();
            var calculationRepository = new Mock<ICalculationRepository>();

            unitOfWork.Setup(m => m.Parameters).Returns(parameterRepository.Object);
            unitOfWork.Setup(m => m.Calculations).Returns(calculationRepository.Object);
            unitOfWork.Setup(m => m.CompleteAsync()).Returns(Task.FromResult(1));

            var calculator = new CalculatorController(unitOfWork.Object);
            var parameter = new Parameter()
            {
                Id = 1,
                UpperBoundDiscountRate = 5.00,
                DiscountRateIncrement = 1.00,
                InitialValue = -100000,
                CashFlows = new List<CashFlow>()
                {
                    new CashFlow(1000),
                    new CashFlow(1000),
                    new CashFlow(1000)
                }
            };

            var actionResult = await calculator.Calculate(parameter);

            var result = actionResult as ObjectResult;
            Assert.NotNull(result);

            Assert.True(result is BadRequestObjectResult);
        }

        [Fact]
        public async void Invalid_UpperBoundDiscountRate_BadRequestResult()
        {
            var unitOfWork = new Mock<IUnitOfWork>();
            var parameterRepository = new Mock<IParameterRepository>();
            var calculationRepository = new Mock<ICalculationRepository>();

            unitOfWork.Setup(m => m.Parameters).Returns(parameterRepository.Object);
            unitOfWork.Setup(m => m.Calculations).Returns(calculationRepository.Object);
            unitOfWork.Setup(m => m.CompleteAsync()).Returns(Task.FromResult(1));

            var calculator = new CalculatorController(unitOfWork.Object);
            var parameter = new Parameter()
            {
                Id = 1,
                LowerBoundDiscountRate = 1.00,
                DiscountRateIncrement = 1.00,
                InitialValue = -100000,
                CashFlows = new List<CashFlow>()
                {
                    new CashFlow(1000),
                    new CashFlow(1000),
                    new CashFlow(1000)
                }
            };

            var actionResult = await calculator.Calculate(parameter);

            var result = actionResult as ObjectResult;
            Assert.NotNull(result);

            Assert.True(result is BadRequestObjectResult);
        }

        [Fact]
        public async void Invalid_DiscountRateIncrement_BadRequestResult()
        {
            var unitOfWork = new Mock<IUnitOfWork>();
            var parameterRepository = new Mock<IParameterRepository>();
            var calculationRepository = new Mock<ICalculationRepository>();

            unitOfWork.Setup(m => m.Parameters).Returns(parameterRepository.Object);
            unitOfWork.Setup(m => m.Calculations).Returns(calculationRepository.Object);
            unitOfWork.Setup(m => m.CompleteAsync()).Returns(Task.FromResult(1));

            var calculator = new CalculatorController(unitOfWork.Object);
            var parameter = new Parameter()
            {
                Id = 1,
                LowerBoundDiscountRate = 1.00,
                UpperBoundDiscountRate = 5.00,
                DiscountRateIncrement = -1,
                InitialValue = -100000,
                CashFlows = new List<CashFlow>()
                {
                    new CashFlow(1000),
                    new CashFlow(1000),
                    new CashFlow(1000)
                }
            };

            var actionResult = await calculator.Calculate(parameter);

            var result = actionResult as ObjectResult;
            Assert.NotNull(result);

            Assert.True(result is BadRequestObjectResult);
        }
    }
}

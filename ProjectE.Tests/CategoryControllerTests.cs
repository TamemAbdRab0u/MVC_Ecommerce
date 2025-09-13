using Xunit;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MVC___ProjectE__.Controllers; 
using MVC___ProjectE__.Models;
using MVC___ProjectE__.Repository;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Http;
using System.Linq.Expressions;

namespace ProjectE.Tests
{
    public class CategoryControllerTests
    {
        [Fact]
        public void ReturnsListOfCategories()
        {
            var mockRepo = new Mock<ICategoryRepo>();
            var sampleData = new List<Category>
            {
                new Category { Id = 1, Name = "Test A" },
                new Category { Id = 2, Name = "Test B" }
            };

            mockRepo.Setup(repo => repo.GetAll()).Returns(sampleData.AsQueryable());

            var controller = new CategoryController(mockRepo.Object);

            var result = controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<Category>>(viewResult.Model);
            Assert.Equal(2, model.Count);
        }


        [Fact]
        public void GetCreate()
        {
            var mockRepo = new Mock<ICategoryRepo>();
            var controller = new CategoryController(mockRepo.Object);

            var result = controller.Create();

            Assert.IsType<ViewResult>(result);
        }


        [Fact]
        public void PostCreate()
        {
            var mockRepo = new Mock<ICategoryRepo>();
            var controller = new CategoryController(mockRepo.Object);
            ITempDataDictionary tempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
            controller.TempData = tempData;

            var category = new Category { Id = 1, Name = "Test Category" };

            var result = controller.Create(category) as RedirectToActionResult;

            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);

            mockRepo.Verify(r => r.Add(category), Times.Once);
            mockRepo.Verify(r => r.Save(), Times.Once);
        }

        [Fact]
        public void PostEdit()
        {

            var mockRepo = new Mock<ICategoryRepo>();
            var controller = new CategoryController(mockRepo.Object);
            ITempDataDictionary tempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
            controller.TempData = tempData;

            var category = new Category { Id = 1, Name = "Updated Category" };

            var result = controller.Edit(category) as RedirectToActionResult;

            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            mockRepo.Verify(r => r.Update(category), Times.Once);
            mockRepo.Verify(r => r.Save(), Times.Once);
        }

        [Fact]
        public void SaveDelete_POST_WithValidId_RedirectsToIndex()
        {
            var mockRepo = new Mock<ICategoryRepo>();
            var controller = new CategoryController(mockRepo.Object);
            ITempDataDictionary tempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
            controller.TempData = tempData;

            var testCategory = new Category { Id = 1, Name = "Test Category" };
            mockRepo.Setup(r => r.GetById(It.IsAny<Expression<Func<Category, bool>>>())).Returns(testCategory);

            var result = controller.SaveDelete(1) as RedirectToActionResult;

            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            mockRepo.Verify(r => r.GetById(It.Is<Expression<Func<Category, bool>>>(expr => expr.Compile()(testCategory))), Times.Once);
            mockRepo.Verify(r => r.Remove(testCategory), Times.Once);
            mockRepo.Verify(r => r.Save(), Times.Once);
        }
    }
}

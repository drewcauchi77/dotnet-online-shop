using Microsoft.AspNetCore.Mvc;
using OnlineShop.Controllers;
using OnlineShop.ViewModels;
using OnlineShopTests.Mocks;

namespace OnlineShopTests.Controllers
{
    public class PieControllerTests
    {
        [Fact]
        public void List_EmptyCategory_ReturnsAllPies()
        {
            // Arrange
            var mockPieRepository = RepositoryMocks.GetPieRepository();
            var mockCategoryRepository = RepositoryMocks.GetCategoryRepository();
            PieController pieController = new(mockPieRepository.Object, mockCategoryRepository.Object);

            // Act
            var result = pieController.List("");

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var pieListViewModel = Assert.IsAssignableFrom<PieListViewModel>(viewResult.ViewData.Model);
            Assert.Equal(10, pieListViewModel.Pies.Count());
        }
    }
}

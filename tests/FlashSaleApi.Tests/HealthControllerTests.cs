using FlashSaleApi.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace FlashSaleApi.Tests;

// 第一個測試：確認測試環境可以執行（P03 再正式學 xUnit）
public class HealthControllerTests
{
    [Fact]
    public void Get_ReturnsOk()
    {
        var controller = new HealthController();

        var result = controller.Get();

        Assert.IsType<OkObjectResult>(result);
    }
}

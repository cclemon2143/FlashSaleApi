using Microsoft.AspNetCore.Mvc;

namespace FlashSaleApi.Controllers;

// 健康檢查：確認 API 有正常啟動，之後部署（A03）和 CI（A04）也會用到
// GET /api/health
[ApiController]
[Route("api/[controller]")]
public class HealthController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new { status = "ok", time = DateTimeOffset.Now });
    }
}

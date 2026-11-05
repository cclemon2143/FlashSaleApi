// =====================================================================
// 程式進入點
// CI4 對照：public/index.php + app/Config/Services.php + app/Config/Filters.php
// =====================================================================

var builder = WebApplication.CreateBuilder(args);

// ---------------------------------------------------------------------
// 1. 註冊服務到 DI 容器 ≈ app/Config/Services.php
//    之後在這裡加入 Service、Repository（W04、W05）和 DbContext（D02）
// ---------------------------------------------------------------------
builder.Services.AddControllers();     // 啟用 專案內的Controller
builder.Services.AddOpenApi();         // API 文件：GET /openapi/v1.json

var app = builder.Build();

// ---------------------------------------------------------------------
// 2. Middleware 管線 ≈ app/Config/Filters.php
//    之後在這裡加入例外處理、Request Log 等 Middleware（W06、W08）
// ---------------------------------------------------------------------
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    //Minimal API寫法，與微服務不同
    app.MapGet("/api/debug/ping", () =>
    {
        return new {serverStatus="OK", apiStatus="OK"};
    });
}

// ---------------------------------------------------------------------
// 3. 路由：掃描所有 Controller 上的 [Route]、[HttpGet] 等標記 ≈ app/Config/Routes.php
// ---------------------------------------------------------------------
app.MapControllers();

app.Run();

# FlashSaleApi：電商訂單與限量搶購 API

以 ASP.NET Core（.NET 10）實作的電商後端，重點是**限量搶購不超賣**。這個專案也是我學 C# 和 .NET 的主要方式：先看功能需求，再查語法，然後自己寫出來並通過驗收。

> 這個資料夾是**獨立的 git repo**，和外層的 LearnDotNet 學習庫分開管理。

## 版本規劃

| 版本 | 時間點 | 內容 |
|---|---|---|
| 後台 | D04 完成後 | MVC 5 + Web API 2（.NET Framework 4.8）實作後台與報表 API，共用同一個資料庫 |
| v1 | 後台之後 | 商品、下單、扣庫存交易、`rowversion` 樂觀鎖、API 文件、xUnit、Docker、GitHub Actions CI、k6 壓測 |
| v2 | v1 之後 | JWT 角色權限（會員／管理員）、`BackgroundService` 取消逾時未付款訂單 |
| v3 | v2 之後 | Redis 快取熱門商品、模擬金流 Webhook |

## 亮點

- 用 k6 壓測「100 人同時搶 10 件商品」，證明不會超賣
- RESTful 設計：資源命名、冪等性、標準狀態碼（`201 Created` + `Location`、`409 Conflict`）

## 資料夾結構

```
FlashSaleApi/
├── FlashSaleApi.slnx            ← 方案檔
├── src/FlashSaleApi/            ← Web API 專案
│   ├── Controllers/             ← 接收 HTTP 請求（目前只有 HealthController）
│   ├── Services/                ← 商業邏輯（W05）
│   ├── Repositories/            ← 資料存取（W04、D02）
│   ├── Models/Entities/         ← 資料表對應的類別（04、D02）
│   ├── Models/Dtos/             ← API 的輸入輸出格式（W03）
│   ├── Middlewares/             ← 自訂 Middleware（W06）
│   ├── Exceptions/              ← 自訂例外（08、W08）
│   ├── Program.cs               ← 程式進入點、DI、Middleware 管線
│   └── FlashSaleApi.http        ← 用 REST Client 測試 API
├── tests/FlashSaleApi.Tests/    ← xUnit 測試
├── load-tests/                  ← k6 壓測（v1）
└── docs/ROADMAP.md              ← 學習與開發路線圖
```

分層方式和 `LearnDotNet/webapi/UserApi` 相同（Controller → Service → Repository），可以對照著看。

## 執行方式

在 Dev Container 的終端機（或已安裝 .NET 10 SDK 的電腦）執行：

```bash
cd FlashSaleApi
dotnet build                          # 建置
dotnet test                           # 執行測試
dotnet run --project src/FlashSaleApi # 啟動 API
```

啟動後開啟 <http://localhost:5100/api/health>，看到 `{"status":"ok", ...}` 就成功了。也可以打開 `src/FlashSaleApi/FlashSaleApi.http`，點「Send Request」測試。

- 連接埠用 **5100**，和 UserApi 的 5000 錯開，兩個 API 可以同時啟動。
- API 文件：<http://localhost:5100/openapi/v1.json>（第 1 週的 W09 會加上 Scalar 或 Swagger UI）。

## 學習路線

每個階段要學什麼、在專案做什麼、怎麼驗收，請看 [docs/ROADMAP.md](docs/ROADMAP.md)。

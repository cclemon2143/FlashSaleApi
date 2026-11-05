# 學習與開發路線圖

這份路線圖來自 Claude 與 Gemini 的三次討論，紀錄在 `AI-Discussions/projects/LearnDotNet/`：課綱（`2026-11-03_1601_curriculum.md`）、專案主導學習（`2026-11-04_0945_project-first-learning.md`）、避免照抄（`2026-11-04_1016_avoid-copying.md`）。

學習者的條件：

- PHP / CodeIgniter 4 開發者，目標是 .NET 與 .NET Framework 後端（Framework 以 MVC 為優先）
- 每天約 3～4 小時，邊學邊投履歷
- 在 GitHub 公開專案、在 LinkedIn 分享學習過程

狀態圖示：✅ 通過驗收　📝 進行中　（空白）尚未開始　📣 適合發 LinkedIn

---

## 學習方式：專案場景先行

每個功能都走這個流程，不用先把 `lessons/` 學完：

1. **看功能需求**：先知道這段語法在實戰中要解決什麼問題。
2. **查語法**：`lessons/` 範例 → 微軟官方文件 → 自己試寫 → 最後才看 `webapi/UserApi`，看完關檔再寫。
3. **寫進專案**：程式碼都由自己寫。卡住時 AI 只用提示引導，不動檔案也不給程式碼（規則見 `LearnDotNet/CLAUDE.md`）。
4. **驗收**：口頭問答加一道小改題。通過才標 ✅，沒過不打勾，記錄到 `lessons/REVIEW-LOG.md`，重寫後再驗收。

`lessons/` 的練習題改成補救用途：驗收發現觀念不清楚時，才回頭寫對應的題目。

---

## 和原本課綱的差異

| 調整 | 原因 |
|---|---|
| 不用先學完 C# 課程，直接從專案開始 | 先知道實戰用途，才清楚會用到哪些語法 |
| C# 主題拆進各功能，在需要時才學 | 語法立刻有地方用 |
| 練習題改成補救用途，每個功能加上驗收 | 作品出來得快，但容易照抄，驗收是抓盲點的主要關卡 |
| 06 泛型併進 07 LINQ | 泛型在 `List<T>`、LINQ 裡就會一直用到 |
| 10 現代語法移到 W03 | `record` 最適合用來寫 DTO |
| 05 完整學（介面、繼承、`abstract`、`virtual`/`override`、多型） | 不留到投履歷前才補 |
| 09 async 拆兩半 | 記憶體版只能學語法，接上資料庫才看得到等待 I/O 的用途 |
| 08 例外處理拆兩半 | 自訂例外配合下單；`IDisposable` 在進資料庫之前學 |
| W09 API 文件提前到第 1 週 | 之後每個 API 都能用文件頁面測試 |
| W07 排在 D02 前，W06 排在 P03 後 | 連線字串要放在設定檔裡 |
| D01、D02 連續學，D01 內容不簡化 | 先手寫 T-SQL，再對照 EF Core 產生的 SQL |
| D03 排在 A03 前，P03 排在 D02 後 | 容器裡的新資料庫要靠 Migrations 建表；有測試才能做 CI |
| W02、W08 加入 RESTful 規範 | 資源命名、冪等性、標準狀態碼是面試常考題 |
| W06 加入 CORS、`IHttpClientFactory` | 前後端分離與串接第三方 API 的實務必備 |
| D06 加入 Stored Procedure、索引、執行計畫 | 台灣職缺常要求 T-SQL 實務 |
| .NET Framework 提前到 D04 之後，集中學 | 目前 Framework 職缺常見；EDMX 要有資料表才能做，16GB 記憶體也不適合頻繁開關 VM |
| Framework 順序改成 F01 → F04 → F05 → F02 + F03 → F06 | 頁面要先有資料可用，MVC 優先 |
| Framework 成果改成「電商後台與報表 API」 | 新舊技術共用同一個資料庫，貼近實際維護情境 |
| A01、A02 維持兩課 | 架構和設計模式各自都需要扎實學 |

---

## 第一階段：Web API 與 C# 核心（記憶體版）⏱ 第 1～3 週

### 第 1 週：跑起專案、查詢商品

| 課 | 主題 | 在 FlashSaleApi 實作 | 驗收重點 | 狀態 |
|---|---|---|---|---|
| W01 | 專案結構與 `dotnet` CLI | 看懂 `.slnx`、`.csproj`、`Program.cs`，執行 `GET /api/health` | `Program.cs` 啟動時依序做了什麼？和 CI4 的 `index.php` 差在哪？ | ✅ |
| 04 | 類別與物件 | 在 `Models/Entities` 寫 `Product`（名稱、價格、庫存） | property 和欄位差在哪？建構子負責什麼？ | |
| 03 + 07 | 集合（查需要的部分）、泛型 + LINQ ⭐ | 用 `List<Product>` 存商品，用 LINQ 篩選、排序 | 集合邊走訪邊刪除會怎樣？`IEnumerable` 的延遲執行是什麼？ | |
| W02 | Controller、路由、RESTful 命名 | `GET /api/products`（基本篩選、排序）、`GET /api/products/{id}` | 查無資料該回什麼狀態碼？網址為什麼用名詞複數？ | |
| W09 | API 文件 | 加上 Scalar 或 Swagger UI | OpenAPI 文件是怎麼產生的？ | |

### 第 2 週：新增商品、依賴注入

| 課 | 主題 | 在 FlashSaleApi 實作 | 驗收重點 | 狀態 |
|---|---|---|---|---|
| W03 + 10 | Model Binding、驗證、`record` | `POST /api/products`，用 `record` 寫 `CreateProductRequest` 並加上驗證 | `record` 和 `class` 差在哪？驗證失敗回什麼？ | |
| 05 | 介面、繼承、`abstract`、`virtual`/`override`、多型 | 定義 `IProductRepository`；讓限量商品繼承 `Product` 並覆寫售價；用 `abstract` 類別寫折扣規則 | 介面和抽象類別怎麼選？PHP 不用 `virtual`，C# 為什麼要？ | |
| 09 前半 | async/await ⭐ | Repository 的方法都回傳 `Task` | `await` 時發生什麼事？`.Result` 在 ASP.NET Core 的主要風險是什麼？（執行緒集區耗盡） | |
| W04 | 相依性注入 ⭐ | 註冊 `InMemoryProductRepository`、`ProductService` | 三種生命週期差在哪？Singleton 注入 Scoped 會怎樣？ | |

05 內容比較多，這週可能會排得比較滿，延到下週也沒關係。

### 第 3 週：下單扣庫存

| 課 | 主題 | 在 FlashSaleApi 實作 | 驗收重點 | 狀態 |
|---|---|---|---|---|
| 08 前半 | 例外處理 | 建立 `OutOfStockException` | 什麼時候該丟例外，什麼時候該回傳結果？ | |
| W05 | 分層架構 | `POST /api/orders` 下單並扣庫存（先用記憶體） | 扣庫存的邏輯為什麼放 Service？ | |
| W08 | 錯誤處理、`ProblemDetails`、狀態碼 | 庫存不足回 `409 Conflict`，建立訂單回 `201 Created` 加 `Location` | `PUT` 和 `POST` 的冪等性差在哪？ | 📣 |

## 第二階段：進資料庫 ⏱ 第 4～5 週

### 第 4 週：設定與 T-SQL

| 課 | 主題 | 在 FlashSaleApi 實作 | 驗收重點 | 狀態 |
|---|---|---|---|---|
| W07 | 設定與 Logging | 用 `IOptions<T>` 讀「訂單逾時分鐘數」，加上 Serilog | 環境設定檔的覆蓋順序是什麼？ | |
| 08 後半 | `IDisposable`、`using` | 用範例理解資源釋放，為 `DbContext` 做準備 | `using` 結束時發生什麼事？ | |
| D01 | SQL Server 基礎（內容不簡化） | 啟動容器，手寫 T-SQL：建表、CRUD、`JOIN`、`GROUP BY`；整理和 MySQL 的差異（`TOP`、`IDENTITY`、資料型別） | 當場寫一段 `JOIN` 加 `GROUP BY` | |

### 第 5 週：EF Core、測試

| 課 | 主題 | 在 FlashSaleApi 實作 | 驗收重點 | 狀態 |
|---|---|---|---|---|
| D02 + 09 後半 | EF Core 入門、`CancellationToken` | 建立 `FlashSaleDbContext`，對應手寫的資料表，換掉記憶體版 Repository；比對 EF Core 產生的 SQL | `DbContext` 為什麼用 Scoped？`CancellationToken` 解決什麼問題？ | |
| P03 | 單元測試 ⭐ | 用 xUnit + Moq 測試下單扣庫存的 Service | 為什麼要 mock Repository？ | |
| W06 | Middleware、Filter、CORS、`IHttpClientFactory` | Request Log Middleware、CORS 設定 | Middleware 的順序為什麼重要？ | |

## 第三階段：自動化與資料關聯

| 課 | 主題 | 在 FlashSaleApi 實作 | 驗收重點 | 狀態 |
|---|---|---|---|---|
| D03 | Migrations | 刪掉手寫的資料庫，改用 Code First 重建，Seed 測試商品 | Migration 檔案記錄了什麼？ | |
| A03 | Docker 部署 | Dockerfile、docker compose 一起啟動 API 和資料庫 | 多階段建置的好處是什麼？ | |
| A04 | GitHub Actions | push 時自動建置與測試，README 加上 CI 徽章 | CI 失敗時要怎麼查？ | 📣 |
| D04 | 關聯 | `Order`、`OrderItem`、`Product` 的一對多關聯，`Include()` | 不用 `Include()` 會怎樣？ | |

## 第四階段：.NET Framework（集中學習，Windows VM）

D04 完成後開始。Windows VM 可以先利用空檔裝好。學這一階段時關掉 Dev Container 省記憶體，但保留 Mac 上的 `db` 容器，讓 VM 連到 Mac 的 1433 埠，和 FlashSaleApi 共用同一個資料庫。

| 課 | 主題 | 實作 | 驗收重點 | 狀態 |
|---|---|---|---|---|
| F01 | 環境與專案結構 | 建立後台專案，看懂 `Web.config`、`Global.asax` | 和 .NET 10 的 `Program.cs` 對照 | |
| F04 | Entity Framework 6 | 用 EF6（Database First / EDMX）連到同一個資料庫 | EF6 和 EF Core 差在哪？ | |
| F05 | ASP.NET MVC 5 | 後台頁面：商品管理、訂單查詢（Razor） | 一個請求如何走到 View？ | 📣 |
| F02 + F03 | Web API 2、DI 與 Filter | 報表 API：每日銷售額、熱銷商品；用 Autofac 注入 Repository | `.Result` 在 Framework 為什麼會造成死結？和 Core 差在哪？ | |
| F06 | 遷移到 .NET Core | 整理新舊兩邊的對照表 | 舊專案升級要注意什麼？ | |

**成果**：作品集同時有「.NET 10 前台 API」和「.NET Framework 後台」，共用同一個資料庫。

## 第五階段：防超賣 → **v1 里程碑**

| 課 | 主題 | 在 FlashSaleApi 實作 | 驗收重點 | 狀態 |
|---|---|---|---|---|
| D05 | 效能與陷阱 ⭐ | 檢查訂單列表的 N+1 問題，查詢加上 `AsNoTracking` | `IQueryable` 和 `IEnumerable` 差在哪？ | |
| D06 | Dapper、交易、T-SQL 實務 | 下單用 Transaction，庫存用 `rowversion` 樂觀鎖；用 Dapper 寫銷售報表；Stored Procedure、索引、執行計畫 | 樂觀鎖和悲觀鎖怎麼選？ | |
| 🎯 | **壓測** | 用 k6 模擬「100 人同時搶 10 件商品」，證明不會超賣（`load-tests/`） | 看懂壓測報告 | 📣 |

**v1 完成**：更新履歷，發一篇完整的 LinkedIn 文章（防超賣的實作與壓測結果）。

## 第六階段：實務必備 → **v2、v3**

| 課 | 主題 | 在 FlashSaleApi 實作 | 狀態 |
|---|---|---|---|
| P01 | 認證：JWT | 會員註冊、登入發 Token，密碼雜湊 | |
| P02 | 授權 | 會員與管理員兩種角色，只有管理員能新增商品 | |
| P06 | 背景工作 | 用 `BackgroundService` 定時取消逾時未付款的訂單 | 📣 v2 |
| P04 | 整合測試 | 用 `WebApplicationFactory` 測試整個下單流程 | |
| P05 | 快取 | 用 Redis 快取熱門商品 | |
| P07 | 物件轉換與分頁 | 商品列表的分頁與進階查詢（W02 只做基本篩選排序） | |
| — | 模擬金流 | 用 `IHttpClientFactory` 串接模擬金流，接收付款完成的 Webhook | 📣 v3 |

驗收方式相同，題目由 AI 依當時的功能出題。

## 第七階段：架構

| 課 | 主題 | 實作 | 狀態 |
|---|---|---|---|
| A01 | Clean Architecture | 把 FlashSaleApi 拆成 Api、Application、Domain、Infrastructure | |
| A02 | SOLID 與設計模式 | Unit of Work、Strategy（例如不同的折扣規則） | |

## 第八階段：面試準備

- 模擬面試：C#、ASP.NET Core、EF Core、架構、SQL
- 履歷：把 CI4 經驗與 FlashSaleApi 的成果轉換成 .NET 職缺的關鍵字

---

## 還沒決定的事

- 補上 Visual Studio 除錯技巧（可在第四階段一起學），這項只有 Claude 提出，暫時沒有採用。
- 提供 3～5 則目標職缺的技術要求後，可以再確認 Framework 的深度與開始時間，以及要不要加上 Azure、IIS 部署、前端（jQuery / Vue）等主題。

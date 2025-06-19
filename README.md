# AppChirp

## 專案簡介
AppChirp 是一個用於程式內訊息傳遞的 .NET 套件，支援事件型態的即時訊息發送與接收，適合應用於模組間解耦、訊息推播等場景。

## 主要功能
- 註冊與管理事件源（Event Source）
- 即時訊息傳送與接收
- 支援 DI（相依性注入）
- 易於擴充與整合

## 安裝方式
請透過 NuGet 安裝 AppChirp 套件：
dotnet add package AppChirp
或於 Visual Studio 的 NuGet 管理器搜尋 `AppChirp` 進行安裝。

## 用法說明

### 1. 註冊 AppChirp 與事件源
在 `Program.cs` 或 DI 註冊階段加入：

```csharp
services.AddAppChirp(config =>
    config.RegisterEventSource<string>("demo"));
```
### 2. 取得事件發送者並發送訊息
```csharp
var eventBus = serviceProvider.GetRequiredService<IEventBus>();
var publisher = eventBus.GetEventPublisher<string>("demo");
await publisher?.PublishAsync("Hello, AppChirp!");
```
### 3. 監聽事件訊息
```csharp
var observable = eventBus.GetEventObserable<string>("demo");
observable?.Subscribe(msg => Console.WriteLine($"Received: {msg}"));
```
### 4. 在 ASP.NET Core 專案中使用（範例）
```csharp
builder.Services.AddAppChirp(config => config.RegisterEventSource<string>("demo"));
builder.Services.AddKeyedSingleton(
    "demo",
    (sp, _) => sp.GetRequiredService<IEventBus>().GetEventPublisher<string>("demo")!);
```

## 貢獻方式
歡迎提出 issue 或 pull request，請遵循專案的貢獻指南。

## 授權
本專案採用 MIT 授權。
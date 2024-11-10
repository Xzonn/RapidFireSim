# 连发手柄模拟

用于模拟一个连发手柄。虽然很多手柄都支持连发功能，但是 Xbox 官方手柄并不支持连发，于是就有了这个项目。

## 使用方法

需要 [.NET 8.0](https://dotnet.microsoft.com/zh-cn/download/dotnet/8.0) 运行时以及 [ViGEm Bus Driver](https://github.com/nefarius/ViGEmBus/releases/latest)。

[下载最新版本](https://github.com/Xzonn/RapidFireSim/releases/latest)，解压后运行 `RapidFireSim.exe`。

## 配置

可以在命令行中添加参数来配置，也可在当前文件夹下新建名为 `command.txt` 的文件来配置，配置项格式类似于 `按键,持续时间,按键,持续时间`。

例如：

```
A,20,,20
```

表示按下 A 键 20 毫秒→松开 20 毫秒（这也是默认配置）。可以同时配置多个按键，例如：

```
A,20,B,20,AB,20,,20
```

表示按下 A 键 20 毫秒→松开 A 键并按下 B 键 20 毫秒→同时按下 A 和 B 键 20 毫秒→松开所有按键 20 毫秒。不同按键之间无需空格。无法识别的按键会被跳过。

可在 <https://www.onlinemictest.com/zh/controller-tester/> 或 <https://hardwaretester.com/gamepad> 测试效果。

## 按键列表

| 按键名 | 写法 |
| ------ | ---- |
| A | `A` |
| B | `B` |
| X | `X` |
| Y | `Y` |
| ↑（Up） | `U` |
| ↓（Down） | `D` |
| ←（Left） | `L` |
| →（Right） | `R` |
| LT | `LT` |
| RT | `RT` |
| LB | `LB` |
| RB | `RB` |
| 返回（Back） | `BC` |
| 开始（Start） | `S` |
| 导航（Guide） | `G` |

> [!NOTE]
>
> **注意**：为了避免冲突，B 键和 ← 键同时按下时请写作 `BL` 而不是 `LB`，B 键和 → 键同时按下时请写作 `BR` 而不是 `RB`。

## 授权协议

本项目使用 [GNU General Public License v3.0](https://www.gnu.org/licenses/gpl-3.0.html) 授权。请阅读 `LICENSE.txt` 获取更多信息。

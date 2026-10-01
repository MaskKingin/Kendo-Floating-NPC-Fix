# Kendo Floating NPC Fix

[![Download](https://img.shields.io/github/v/release/MaskKingin/Kendo-Floating-NPC-Fix?label=Download&style=for-the-badge)](https://github.com/MaskKingin/Kendo-Floating-NPC-Fix/releases/latest)

**Fix for floating NPCs in Kendo (Steam AppID 5182140).**

## Проблема

В игре часть NPC «висит» в воздухе — они не взаимодействуют с игроком
и выглядят сломанными. Баг не связан с сохранениями или системой.

## Решение

Мод для MelonLoader, который:
- Ставит всех NPC на землю
- Разносит их на равное расстояние (2.5 юнита)
- Запускает idle-анимацию
- Не даёт им телепортироваться или двигаться за игроком

## Установка

### 1. Скачать

Скачайте последний релиз: **[Releases](https://github.com/MaskKingin/Kendo-Floating-NPC-Fix/releases/latest)**

### 2. Установить MelonLoader

Запустите `MelonLoader.Installer.exe`, укажите путь к `Kendo.exe`: ...\Steam\steamapps\common\Kendo\Kendo.exe
Нажмите **Install**.

### 3. Установить мод

Скопируйте `NpcYFix.dll` в папку: ...\Steam\steamapps\common\Kendo\Mods\
(если папки нет — создайте)

### 4. Запустить

Запустите игру через Steam. NPC должны стоять на земле.

## Проверка

Откройте `...\Kendo\MelonLoader\Latest.log`, найдите строки:
[NPC Y Fix] PLACED Role101_sword_R(Clone) at (-2.50,-10.75)
[NPC Y Fix] PLACED Role232_L(Clone) at (2.50,-10.75)

## Удаление

Удалите файл `...\Kendo\Mods\NpcYFix.dll`.

## Требования

- Windows 10/11 x64
- .NET 6 Runtime (установщик MelonLoader поставит сам)
- Kendo (Steam)

## Известные ограничения

- Оригинальные координаты NPC в игре зашифрованы (Odin Serializer / Easy Save 3).
  Мод **не восстанавливает оригинальные позиции**, а расставляет NPC равномерно.
- Если у вас другая версия игры, `Y = -10.75` может не подойти — откройте
  `src/NpcYFix.cs`, поменяйте `FIXED_Y` и пересоберите.

## Сборка из исходников

```bash
git clone https://github.com/MaskKingin/Kendo-Floating-NPC-Fix.git
cd Kendo-Floating-NPC-Fix/src
dotnet build -c Release

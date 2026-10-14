# DocumentPublishingTest

`xml:lang` で多言語化した C# XML コメントと `docs/<lang>` を、DocFX で言語別に GitHub Pages へ公開する検証用リポジトリ。

## 構成

| パス | 内容 |
|---|---|
| `src/Sample.Core`, `src/Sample.Extensions` | 多言語 XML コメント付きサンプル (Extensions → Core を参照) |
| `src/Directory.Build.*` | ドキュメント生成 + ビルド後に `SeparateDocs.ps1` で en / ja に分割 |
| `scripts/SeparateDocs.ps1` | D3Fragments からコピー。`bin/.../<name>.xml` (en) と `bin/.../ja/<name>.xml` を出力 |
| `scripts/BuildDocs.ps1` | 言語ごとに dll + xml をステージングし DocFX を実行、`_site/<lang>` を生成 |
| `scripts/GenerateToc.ps1` | `docs/<lang>/<section>/toc.yml` をディレクトリ構成から生成 (ファイル名順、`index.md` 先頭)。`BuildDocs.ps1` から呼ばれる |
| `docfx/<lang>.json` | 言語別 DocFX 設定 |
| `docfx/index.html` | サイトルートの言語選択ページ |
| `docs/<lang>` | 手書きドキュメント。`<xref:UID>` / `@UID` で API へリンク |
| `.github/workflows/docs.yml` | Pages 公開 (`workflow_dispatch`) |

## 仕組み

DocFX は csproj を入力にするとソースからコメントを読む (両言語が混ざる) ため、**dll を入力**にしている。
dll と同名の xml を隣に置くと DocFX はそれを読むので、言語ごとに分割済み xml をステージングして別々にビルドする。
各言語のビルドは独立しているため、xref は常に同一言語のページへ解決される。

`docs/<lang>/toc.yml` (ナビバー) は手書き。各セクション (`guide/` など) のサイドバー用 `toc.yml` は自動生成。

生成物 (`_site/`, `docs/*/api/`, `docs/*/*/toc.yml`, `obj/`) はコミットしない。Actions ではランナー上で生成し、Pages にアーティファクトとしてデプロイする。

## ローカルでのビルド

```pwsh
dotnet build -c Release
dotnet tool restore
./scripts/BuildDocs.ps1
dotnet docfx serve _site   # http://localhost:8080
```

## GitHub Pages への公開

1. リポジトリの Settings → Pages → Build and deployment → Source を **GitHub Actions** にする
2. Actions → Docs → Run workflow
3. `https://<owner>.github.io/<repo>/` (言語選択) 、`.../en/`, `.../ja/` を確認

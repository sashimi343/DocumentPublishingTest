# Generate toc.yml for each section directory under docs/<lang> (except api).
#   - Item name: first "# " heading of the markdown. Directory: heading of its index.md, or the directory name.
#   - Order: index.md first, then by file name. A sub directory's index.md becomes the href of the directory item.
Param( $DocsRoot = (Join-Path (Split-Path $PSScriptRoot) 'docs') )

$ErrorActionPreference = 'Stop'

function Get-Title($md) {
    $h = Select-String -LiteralPath $md -Pattern '^#\s+(.+)$' -List
    $h ? $h.Matches[0].Groups[1].Value.Trim() : [IO.Path]::GetFileNameWithoutExtension($md)
}

function Get-TocLines($dir, $section, $indent, $isRoot) {
    $q = { param($s) "'" + $s.Replace("'", "''") + "'" }
    $href = { param($p) [IO.Path]::GetRelativePath($section, $p).Replace('\', '/') }

    $mds = Get-ChildItem -LiteralPath $dir -Filter *.md -File |
        Where-Object { $isRoot -or $_.Name -ne 'index.md' } |
        Sort-Object { $_.Name -ne 'index.md' }, Name
    foreach ($md in $mds) {
        "$indent- name: $(& $q (Get-Title $md.FullName))"
        "$indent  href: $(& $href $md.FullName)"
    }

    foreach ($sub in Get-ChildItem -LiteralPath $dir -Directory | Sort-Object Name) {
        $index = Join-Path $sub.FullName 'index.md'
        $hasIndex = Test-Path -LiteralPath $index
        "$indent- name: $(& $q ($hasIndex ? (Get-Title $index) : $sub.Name))"
        if ($hasIndex) { "$indent  href: $(& $href $index)" }
        $children = @(Get-TocLines $sub.FullName $section "$indent    " $false)
        if ($children) { "$indent  items:"; $children }
    }
}

foreach ($section in Get-ChildItem (Join-Path $DocsRoot '*/*') -Directory | Where-Object Name -ne 'api') {
    $toc = Join-Path $section.FullName 'toc.yml'
    Get-TocLines $section.FullName $section.FullName '' $true | Set-Content -LiteralPath $toc -Encoding utf8NoBOM
    Write-Host "--> $toc"
}

# Script para actualizar sintaxis de Bootstrap 4 a Bootstrap 5
# Ejecutar desde la raíz del proyecto

Write-Host "=====================================" -ForegroundColor Cyan
Write-Host "Bootstrap 4 to 5 Migration Script" -ForegroundColor Cyan
Write-Host "=====================================" -ForegroundColor Cyan
Write-Host ""

# Definir los archivos a actualizar (excluir carpetas bin, obj, packages)
$excludeFolders = @('bin', 'obj', 'packages', '.vs', 'Documentation')
$files = Get-ChildItem -Path . -Include *.aspx,*.ascx,*.html,*.cshtml -Recurse | 
    Where-Object { 
        $exclude = $false
        foreach($folder in $excludeFolders) {
            if($_.FullName -like "*\$folder\*") {
                $exclude = $true
                break
            }
        }
        -not $exclude
    }

Write-Host "Archivos encontrados: $($files.Count)" -ForegroundColor Yellow
Write-Host ""

# Contador de cambios
$totalChanges = 0

# Definir los reemplazos
$replacements = @(
    @{ Find = 'data-toggle="'; Replace = 'data-bs-toggle="' },
    @{ Find = 'data-dismiss="'; Replace = 'data-bs-dismiss="' },
    @{ Find = 'data-target="'; Replace = 'data-bs-target="' },
    @{ Find = 'class="close"'; Replace = 'class="btn-close"' },
    @{ Find = 'mr-auto'; Replace = 'me-auto' },
    @{ Find = 'ml-auto'; Replace = 'ms-auto' },
    @{ Find = 'mr-1'; Replace = 'me-1' },
    @{ Find = 'mr-2'; Replace = 'me-2' },
    @{ Find = 'mr-3'; Replace = 'me-3' },
    @{ Find = 'mr-4'; Replace = 'me-4' },
    @{ Find = 'mr-5'; Replace = 'me-5' },
    @{ Find = 'ml-1'; Replace = 'ms-1' },
    @{ Find = 'ml-2'; Replace = 'ms-2' },
    @{ Find = 'ml-3'; Replace = 'ms-3' },
    @{ Find = 'ml-4'; Replace = 'ms-4' },
    @{ Find = 'ml-5'; Replace = 'ms-5' },
    @{ Find = 'pr-1'; Replace = 'pe-1' },
    @{ Find = 'pr-2'; Replace = 'pe-2' },
    @{ Find = 'pr-3'; Replace = 'pe-3' },
    @{ Find = 'pr-4'; Replace = 'pe-4' },
    @{ Find = 'pr-5'; Replace = 'pe-5' },
    @{ Find = 'pl-1'; Replace = 'ps-1' },
    @{ Find = 'pl-2'; Replace = 'ps-2' },
    @{ Find = 'pl-3'; Replace = 'ps-3' },
    @{ Find = 'pl-4'; Replace = 'ps-4' },
    @{ Find = 'pl-5'; Replace = 'ps-5' },
    @{ Find = 'text-right'; Replace = 'text-end' },
    @{ Find = 'text-left'; Replace = 'text-start' },
    @{ Find = 'float-right'; Replace = 'float-end' },
    @{ Find = 'float-left'; Replace = 'float-start' },
    @{ Find = 'dropdown-menu-right'; Replace = 'dropdown-menu-end' },
    @{ Find = 'dropdown-menu-left'; Replace = 'dropdown-menu-start' }
)

foreach ($file in $files) {
    Write-Host "Procesando: $($file.Name)" -ForegroundColor Gray
    
    $content = Get-Content $file.FullName -Raw
    $originalContent = $content
    $fileChanges = 0
    
    foreach ($replacement in $replacements) {
        if ($content -match [regex]::Escape($replacement.Find)) {
            $content = $content -replace [regex]::Escape($replacement.Find), $replacement.Replace
            $fileChanges++
        }
    }
    
    if ($content -ne $originalContent) {
        Set-Content -Path $file.FullName -Value $content -NoNewline
        Write-Host "  ? $fileChanges cambios aplicados" -ForegroundColor Green
        $totalChanges += $fileChanges
    } else {
        Write-Host "  - Sin cambios" -ForegroundColor DarkGray
    }
}

Write-Host ""
Write-Host "=====================================" -ForegroundColor Cyan
Write-Host "Migración Completada" -ForegroundColor Cyan
Write-Host "=====================================" -ForegroundColor Cyan
Write-Host "Total de cambios aplicados: $totalChanges" -ForegroundColor Yellow
Write-Host ""
Write-Host "Siguiente paso:" -ForegroundColor Cyan
Write-Host "1. Revisa los cambios con 'git diff'" -ForegroundColor White
Write-Host "2. Compila el proyecto (Ctrl+Shift+B)" -ForegroundColor White
Write-Host "3. Prueba la aplicación (F5)" -ForegroundColor White
Write-Host "4. Verifica dropdowns, modals y alertas" -ForegroundColor White
Write-Host ""
Write-Host "Presiona cualquier tecla para salir..." -ForegroundColor Gray
$null = $Host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")
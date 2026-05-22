param(
    [string]$ModelName
)

Write-Host "Generating validator for $ModelName"

$validatorName = "${ModelName}Validator"
$folder = "Validators"

if (!(Test-Path $folder)) {
    New-Item -ItemType Directory -Path $folder | Out-Null
}

$filePath = "$folder/$validatorName.cs"

@"
using FluentValidation;

namespace ProjectName.Validators;

public class $validatorName : AbstractValidator<$ModelName>
{
    public $validatorName()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id is required.");
    }
}
"@ | Set-Content $filePath

Write-Host "Validator created at $filePath"